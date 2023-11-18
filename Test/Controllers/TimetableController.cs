using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Test.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;


namespace Test.Controllers
{
	public class TimetableController : Controller
	{
		private readonly ILogger<TimetableController> _logger;
		private readonly AppDbContext _context;
		public TimetableController(ILogger<TimetableController> logger, AppDbContext context)
		{
			_logger = logger;
			_context = context;
		}
		public IActionResult Index()
		{
			_logger.LogInformation("Timetable Index");
			//_logger.LogInformation(excelData.Count.ToString());
			return View();
		}
		public IActionResult ExcelReader()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> ExcelReader(IFormFile file)
		{
			_logger.LogInformation("ExcelReader");
			System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
			// Upload file
			
			if (file != null && file.Length > 0)
			{
				var uploadDirectory = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Uploads";
				if (!Directory.Exists(uploadDirectory))
				{
					Directory.CreateDirectory(uploadDirectory);
				}

				var filePath = Path.Combine(uploadDirectory, file.FileName);
				using (var stream = new FileStream(filePath, FileMode.Create))
				{
					await file.CopyToAsync(stream);
				}
				// Read file
				using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
				{
					using (var reader = ExcelReaderFactory.CreateReader(stream))
					{
						do
						{
							int isHeaderSkipped = 1;
							while (reader.Read())
							{
								if (isHeaderSkipped != 4)
								{
									isHeaderSkipped++;
									continue;
								}
								int number;
								TempTimetable table = new TempTimetable();
								table.Term = int.TryParse(reader.GetValue(0).ToString(), out number) ? number : 0;
								table.School = reader.GetValue(1).ToString();
								table.ClassId = int.TryParse(reader.GetValue(2).ToString(), out number) ? number : 0;
								table.AClassId = int.TryParse(reader.GetValue(3).ToString(), out number) ? number : 0;
								table.SubjectId = reader.GetValue(4).ToString();
								table.SubjectName = reader.GetValue(5).ToString();
								table.ESubjectName = reader.GetValue(6).ToString();
								table.Difficulty = reader.GetValue(7).ToString();
								table.Note = reader.GetValue(8).ToString();
								table.Weekday = int.TryParse(reader.GetValue(10).ToString(), out number) ? number : 0;
								table.Time = reader.GetValue(11).ToString();
								table.Week = reader.GetValue(15).ToString();
								table.ClassRoom = reader.GetValue(16).ToString();
								table.Experiment = reader.GetValue(17).ToString();
								table.Enrolled = int.TryParse(reader.GetValue(18).ToString(), out number)? number:0;
								table.MaxNumber = int.TryParse(reader.GetValue(19).ToString(), out number) ? number : 0;
								table.Status = reader.GetValue(20).ToString();
								table.ClassType = reader.GetValue(21).ToString();
								table.EduProgram = reader.GetValue(23).ToString();
								_context.TempTables.Add(table);
								await _context.SaveChangesAsync();
								
							}
						} while (reader.NextResult());
						
					}
				}

			}
			//foreach (var row in excelData)
			//{
			//	foreach (var cell in row)
			//	{
			//		_logger.LogInformation(cell);
			//	}
			//}
			return View();
		}
		private List<TempTimetable> GetSearchHistory()
		{
			// chuoi json dc tra ve SearchHistoryJson 
			var SearchHistoryJson = HttpContext.Session.GetString("SearchHistory");
			// DeserializeObject chuyen chuoi json thanh 1 doi tuong List<List<TempTimetable>>
			var searchHistory = SearchHistoryJson != null ? JsonConvert.DeserializeObject<List<TempTimetable>>(SearchHistoryJson) : new List<TempTimetable>();
			return searchHistory;
		}

		private void AddToSearchHistory(TempTimetable currentResult)
		{
			var searchHistory = GetSearchHistory();
			if (searchHistory.Any(s => s.SubjectId == currentResult.SubjectId))
			{
				return;
			} else
			{
				searchHistory.Add(currentResult);
			}
			var searchHistoryJson = JsonConvert.SerializeObject(searchHistory);
			HttpContext.Session.SetString("SearchHistory", searchHistoryJson);
		}
		public IActionResult SubjectSearch()
		{
			var searchHistory = GetSearchHistory();
			ViewBag.SearchHistory = searchHistory;
			return View();
		}
		[HttpPost]
		public IActionResult SubjectSearch(string Subject)
		{

			_logger.LogInformation("SubjectSearch");
			if(_context.Subjects == null)
			{
				return Problem("Context is null");
			}
			var result = _context.TempTables.FirstOrDefault(row => row.SubjectId == Subject);
			if(result != null) AddToSearchHistory(result);
			ViewBag.SearchHistory = GetSearchHistory();
			return View();
		}
		private List<TempTimetable> GetClasses()
		{
			var classSearchJson = HttpContext.Session.GetString("Class");
			var classSearch = classSearchJson != null ? JsonConvert.DeserializeObject<List<TempTimetable>>(classSearchJson) : new List<TempTimetable>();
			return classSearch;
		}
		private void AddClass(List<TempTimetable> newClasses)
		{
			var classes = GetClasses();
			if (classes.Any(s => s.ClassId == newClasses.First().ClassId))
			{
				return;
			} else
			{
				foreach(var newClass in newClasses)
				{
					classes.Add(newClass);
				}
			}
			var classesJson = JsonConvert.SerializeObject(classes);
			HttpContext.Session.SetString("Class", classesJson);
		}
		//public IActionResult Arrange()
		//{
		//	_logger.LogInformation("Arrange action");
		//	ViewBag.Subjects = GetSearchHistory();
		//	ViewBag.Class = GetClasses();
		//	return View();
		//}
		//[HttpPost]
		public IActionResult Arrange()
		{
			_logger.LogInformation("Arrange post action");
			ViewBag.Subjects = GetSearchHistory();
			foreach(var subject in GetSearchHistory())
			{
				var search = _context.TempTables.Where(row => row.SubjectId == subject.SubjectId).ToList();
				if (search != null) AddClass(search);
			}
			ViewBag.Class = GetClasses();
			return View();
		}
		[HttpGet]
		public IActionResult GetClassesBySubjectId(string subjectId)
		{
			var result = new List<TempTimetable>();
			foreach(var Class in _context.TempTables)
			{
				if(Class.SubjectId == subjectId)
				{
					result.Add(Class);
				}
			}
			return Json(result);
		}
		//public IActionResult ClassSearch(string subjectId)
		//{
		//	var search = _context.TempTables.Where(row => row.SubjectId == subjectId).ToList();
		//	if (search != null) AddClass(search);
		//	return Json(search);
		//}
	}
}
