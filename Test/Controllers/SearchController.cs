using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Test.Models;

namespace Test.Controllers
{
	public class SearchController : Controller
	{
		private readonly ILogger<TimetableController> _logger;
		private readonly AppDbContext _context;
		public SearchController(ILogger<TimetableController> logger, AppDbContext context)
		{
			_logger = logger;
			_context = context;
		}
		public IActionResult Index()
		{
			return View();
		}
		[HttpGet]
		public IActionResult UserSearch(string searchData)
		{
			_logger.LogInformation("UserSearch action");
			User? User = _context.Users.FromSqlInterpolated($"SELECT * FROM [Test].[dbo].[Users] WHERE [UserName] = {searchData}").FirstOrDefault();
			if(User != null)
			{
				string url = Url.Action("DisplayUser") + $"?userId={User.Id}";
				return Json(url);
			}
			else
			{
				return Json("No user found");
			}
		}
		public IActionResult DisplayUser(string userId)
		{
			_logger.LogInformation("DisplayUser action");
			List<PTimetable> Timetables = _context.Timetable.FromSqlInterpolated
				(
				$@"SELECT * 
					FROM [Test].[dbo].[Timetable] AS t
					WHERE t.[UserId] = (
						SELECT u.[Id] 
						FROM [Test].[dbo].[Users] AS u
						WHERE u.[Id] = {userId}
						)
					"
				).ToList();
			User? User = _context.Users.FromSqlInterpolated($@"
				SELECT *
				FROM [Test].[dbo].[Users]
				WHERE [Id] = {userId}
				").FirstOrDefault();
			ViewBag.Timetables = Timetables;
			ViewBag.User = User?.UserName;
			return View();
		}
		public class QueriedClass
		{
			public int ClassId { set; get; }
			public string? SubjectId { set; get; }
			public string? SubjectName { set; get; }
			public string? ClassType { set; get; }
			public string? ClassRoom { set; get; }
			public int Weekday { set; get; }
			public string? Time { set; get; }

		}
		public IActionResult DisplayTable(int timetableId)
		{
			List<ClassTb> ClassTb = _context.ClassTb.FromSqlInterpolated
				(
				$@"SELECT *
					FROM [Test].[dbo].[ClassTb] AS ct
					WHERE ct.[TimetableId] = {timetableId}"
				).ToList();
			List<QueriedClass> ResultClass = new List<QueriedClass>();
			foreach (var ClassCell in ClassTb)
			{
				Class? QueryClass = _context.Class.FromSqlInterpolated
					(
					$@"SELECT * 
						FROM [Test].[dbo].[Class]
						WHERE [ClassId] = {ClassCell.ClassId}"
					).FirstOrDefault();
				Subject? QuerySubject = _context.Subject.FromSqlInterpolated
					(
					$@"SELECT *
						FROM [Test].[dbo].[Subject]
						WHERE [SubjectId] = {QueryClass.SubjectId}"
					).FirstOrDefault();
				List<ClassTime> QueryClassTime = _context.ClassTime.FromSqlInterpolated
					(
					$@"SELECT *
						FROM [Test].[dbo].[ClassTime]
						WHERE [ClassId] = {ClassCell.ClassId}"
					).ToList();
				foreach (var QueriedClassTime in QueryClassTime)
				{
					var Class = new QueriedClass()
					{
						ClassId = ClassCell.ClassId,
						SubjectId = QuerySubject.SubjectId,
						SubjectName = QuerySubject.SubjectName,
						ClassType = QueryClass.ClassType,
						ClassRoom = QueriedClassTime.ClassRoom,
						Weekday = QueriedClassTime.Weekday,
						Time = QueriedClassTime.Time
					};
					ResultClass.Add(Class);
				}
			}
			var JsonResult = JsonConvert.SerializeObject(ResultClass);
			ViewBag.ResultClass = JsonResult;
			return View();
		}

		//[HttpGet]
		//public IActionResult TermSearch(int Term)
		//{
		//	List<Class> QueryClass = _context.Class.FromSqlInterpolated
		//		(
		//		$@"SELECT * 
		//			FROM [Test].[dbo].[Class]
		//			WHERE [Term] = {Term}"
		//		).ToList();

		//	return Json();
		//}

		public IActionResult TermSearch(int searchData)
		{
			var Timetables = _context.Timetable.FromSqlInterpolated(
				$@"SELECT *
					FROM [Test].[dbo].[Timetable]
					WHERE [Term] = {searchData}"
				).FirstOrDefault();
			
			if(Timetables != null)
			{
				string url = Url.Action("DisplayTerm") + $"?Term={searchData}";
				return Json(url);
			}
			return Json("No timetable found");
		}
		public IActionResult DisplayTerm(int Term)
		{
			var Timetables = _context.Timetable.FromSqlInterpolated(
				$@"SELECT *
					FROM [Test].[dbo].[Timetable]
					WHERE [Term] = {Term}"
				).ToList();
			var UserNames = _context.Users.FromSqlInterpolated(
				$@"SELECT *
					FROM [Test].[dbo].[Users]
					WHERE [Id] IN 
						(
							SELECT DISTINCT [UserId]
							FROM [Test].[dbo].[Timetable]
							WHERE [Term] = {Term}
						)"
				).ToList();
			ViewBag.Timetables = Timetables;
			ViewBag.UserNames = UserNames;
			return View();
			//return Json(new {timetables= Timetables, userNames = UserNames});
		}

	}
}
