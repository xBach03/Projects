using Microsoft.AspNetCore.Identity;
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
		}
		public List<string> SubjectRecommendation(int UserYear, int UserTerm)
		{
			List<string> results = new List<string>();
			int Term = UserTerm % 10;
			if(UserYear == 1)
			{
				results = new List<string>() { "SSH1131" };
			} else if(UserYear == 2)
			{
				if(Term == 1)
				{
					results = new List<string>() { "ET2021", "ET2031", "MI2020", "PH1122", "PH3330", "SSH1141"};
				} else if(Term == 2)
				{
                    results = new List<string>() { "ED3280", "ET2040", "ET2050", "ET2060", "ET2100", "ET3210", "MI1131" };
                } else
				{
					results = new List<string>() { "SSH1151" };
				}
			} else if(UserYear == 3)
			{
                if (Term == 1)
                {
                    results = new List<string>() { "ET2022", "ET2072", "ET3220", "ET3230", "ET3260", "ET3280", "ME3123", "MI2010" };
                }
                else if (Term == 2)
                {
                    results = new List<string>() { "ET2080", "ET3241", "ET3250", "ET3290", "ET3300", "ET4020" };
                }
                else
                {
                    results = new List<string>() { "ET3270" };
                }
            } else
			{
                if (Term == 1)
                {
                    results = new List<string>() { "ET4010", "ET3310", "ET4070", "ET4230", "ET4250", "ET4291" };
                }
                else if (Term == 2)
                {
                    results = new List<string>() { "ET4920" };
                }
            }
			return results;
		}
		public async Task<IActionResult> Recommender()
		{
			int UserYear = new int();
			int[] Year = new int[] { 1, 2, 3, 4, 5, 6 };
			string[] UserCourse = new string[] { "K68", "K67", "K66", "K65", "K64", "K63" };
            var userManager = HttpContext.RequestServices.GetRequiredService<UserManager<User>>();
            User currentUser = await userManager.GetUserAsync(HttpContext.User);
			for (int i = 0; i < Year.Length; i++)
			{
				if(currentUser.Course == UserCourse[i])
				{
					UserYear = Year[i];
				}
			}
			int UserTerm = _context.TempTable.First().Term;
			var RecommendedSubjects = SubjectRecommendation(UserYear, UserTerm);
            return Json(RecommendedSubjects);
		}
	}
}
