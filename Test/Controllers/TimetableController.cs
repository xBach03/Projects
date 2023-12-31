﻿using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Test.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;

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
		public IActionResult subjectSearch(string subjectId)
		{
			var result = _context.TempTables.FirstOrDefault(t => t.SubjectId == subjectId);
			if(result != null)
			{
				return Json(result);
			}
			return Json(null);
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
		public IActionResult Arrange()
		{
			_logger.LogInformation("Arrange post action");
			ViewBag.Subjects = GetSearchHistory();
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
		[HttpPost]
		public async Task<IActionResult> SaveTimetable([FromBody] List<TempTimetable> SelectedClasses)
		{
			// Lay thong tin ID cua User 
			var userManager = HttpContext.RequestServices.GetRequiredService<UserManager<User>>();
			User currentUser = await userManager.GetUserAsync(HttpContext.User);
			PTimetable PersonalTable = new PTimetable()
			{
				CreatedDate = DateTime.Now,
				UserId = currentUser.Id
			};
			// Add PTimetable truoc thi moi add duoc Class vi Id cua PTimetable la foreign key tham chieu den Class
			_context.Add(PersonalTable);
			await _context.SaveChangesAsync();
			foreach (var SelectedClass in SelectedClasses)
			{
				var Subject = new Subject();
				var Class = new Class();
				if (!_context.Subjects.Any(s => s.SubjectId == SelectedClass.SubjectId))
				{

					Subject.SubjectName = SelectedClass.SubjectName;
					Subject.SubjectId = SelectedClass.SubjectId;
					Subject.ESubjectName = SelectedClass.ESubjectName;
					Subject.School = SelectedClass.School;
					Subject.Difficulty = SelectedClass.Difficulty;
					Subject.EduProgram = SelectedClass.EduProgram;
					Subject.Experiment = SelectedClass.Experiment;
					_context.Subjects.Add(Subject);
				}
				Class.TimetableId = PersonalTable.TimetableId;
				Class.ClassId = SelectedClass.ClassId;
				Class.AClassId = SelectedClass.AClassId;
				Class.Term = SelectedClass.Term;
				Class.Time = SelectedClass.Time;
				Class.Week = SelectedClass.Week;
				Class.Weekday = SelectedClass.Weekday;
				Class.Enrolled = SelectedClass.Enrolled;
				Class.ClassRoom = SelectedClass.ClassRoom;
				Class.ClassType = SelectedClass.ClassType;
				Class.Status = SelectedClass.Status;
				Class.MaxNumber = SelectedClass.MaxNumber;
				Class.Note = SelectedClass.Note;
				Class.SubjectId = SelectedClass.SubjectId;
				_context.Classes.Add(Class);
				await _context.SaveChangesAsync();
			}

			return Json(new { success = true, message = "Success." });
		}
	}
}
