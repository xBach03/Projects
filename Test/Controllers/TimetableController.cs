using ExcelDataReader;
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
								table.DateCount = int.TryParse(reader.GetValue(9).ToString(), out number) ? number : 0;
								table.Weekday = int.TryParse(reader.GetValue(10).ToString(), out number) ? number : 0;
								table.Time = reader.GetValue(11).ToString();
								table.Start = int.TryParse(reader.GetValue(12).ToString(), out number) ? number : 0;
								table.End = int.TryParse(reader.GetValue(13).ToString(), out number) ? number : 0;
								table.Shift = reader.GetValue(14) == null? null : reader.GetValue(14).ToString();
								table.Week = reader.GetValue(15).ToString();
								table.ClassRoom = reader.GetValue(16).ToString();
								table.Experiment = reader.GetValue(17).ToString();
								table.Enrolled = int.TryParse(reader.GetValue(18).ToString(), out number)? number:0;
								table.MaxNumber = int.TryParse(reader.GetValue(19).ToString(), out number) ? number : 0;
								table.Status = reader.GetValue(20).ToString();
								table.ClassType = reader.GetValue(21).ToString();
								table.OpenStage = reader.GetValue(22).ToString();
								table.EduProgram = reader.GetValue(23).ToString();
								_context.TempTable.Add(table);
								
									await _context.SaveChangesAsync();
								

							}
						} while (reader.NextResult());
						
					}
				}
			}
			return View();
		}
		[HttpGet]
		public IActionResult SubjectSearch(string subjectId)
		{
			var result = _context.TempTable.FirstOrDefault(t => t.SubjectId.ToLower() == subjectId.ToLower());
			if (result != null)
			{
				return Json(result);
			}
			return Json("No subject found");
		}
		private List<string> GetSearchHistory()
		{
			// chuoi json dc tra ve SearchHistoryJson 
			var SearchHistoryJson = HttpContext.Session.GetString("SearchHistory");
			// DeserializeObject chuyen chuoi json thanh 1 doi tuong List<List<TempTimetable>>
			var searchHistory = SearchHistoryJson != null ? JsonConvert.DeserializeObject<List<string>>(SearchHistoryJson) : new List<string>();
			return searchHistory;
		}
		private void AddToSearchHistory(List<string> SearchResult)
		{
			var searchHistory = GetSearchHistory();
			searchHistory.Clear();
			foreach (var Subject in SearchResult)
			{
				searchHistory.Add(Subject);
			}
			var searchHistoryJson = JsonConvert.SerializeObject(searchHistory);
			HttpContext.Session.SetString("SearchHistory", searchHistoryJson);
		}

		[HttpPost]
		public IActionResult SubjectList([FromBody] List<string> SelectedSubjects)
		{
			_logger.LogInformation("SubjectList action");
			AddToSearchHistory(SelectedSubjects);
			return Json(Url.Action("Arrange"));
		}
		public IActionResult Arrange()
		{
			_logger.LogInformation("Arrange action");
			var SelectedSubjects = GetSearchHistory();
			//foreach(var subject in SelectedSubjects)
			//{
			//	_logger.LogInformation(subject);
			//}
			var subjectList = new List<TempTimetable>();
			foreach (var Subject in SelectedSubjects)
			{
				var result = _context.TempTable.FirstOrDefault(s => s.SubjectId.ToLower() == Subject.ToLower());
				if (result != null)
				{
					subjectList.Add(result);
				}
			}
			ViewBag.Subjects = subjectList;
			return View();
		}
		[HttpGet]
		public IActionResult GetClassesBySubjectId(string subjectId)
		{
			var result = new List<TempTimetable>();
			foreach(var Class in _context.TempTable)
			{
				if(Class.SubjectId.ToLower() == subjectId.ToLower())
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
				UserId = currentUser.Id,
				Term = SelectedClasses.First().Term
			};
			_context.Timetable.Add(PersonalTable);
			await _context.SaveChangesAsync();
			// Add PTimetable truoc thi moi add duoc Class vi Id cua PTimetable la foreign key tham chieu den Class
			foreach (var SelectedClass in SelectedClasses)
			{
				ClassTb classTb = new ClassTb()
				{
					ClassId = SelectedClass.ClassId,
					TimetableId = PersonalTable.TimetableId
				};
				_context.ClassTb.Add(classTb);
				ClassUser classUser = new ClassUser()
				{
					ClassId = SelectedClass.ClassId,
					UserId = currentUser.Id
				};
				_context.ClassUser.Add(classUser);
				if (!_context.Subject.Any(s => s.SubjectId == SelectedClass.SubjectId))
				{
					var Subject = new Subject()
					{
						SubjectName = SelectedClass.SubjectName,
						SubjectId = SelectedClass.SubjectId,
						ESubjectName = SelectedClass.ESubjectName,
						School = SelectedClass.School,
						Difficulty = SelectedClass.Difficulty,
						EduProgram = SelectedClass.EduProgram,
					};
					_context.Subject.Add(Subject);
				}
				if(!_context.ClassTime.Any(c => c.ClassId == SelectedClass.ClassId && c.DateCount == SelectedClass.DateCount))
				{
					var ClassTime = new ClassTime()
					{
						ClassId = SelectedClass.ClassId,
						DateCount = SelectedClass.DateCount,
						Time = SelectedClass.Time,
						Start = SelectedClass.Start,
						End = SelectedClass.End,
						Shift = SelectedClass.Shift,
						Week = SelectedClass.Week,
						Weekday = SelectedClass.Weekday,
						ClassRoom = SelectedClass.ClassRoom
					};
					_context.ClassTime.Add(ClassTime);
				}
				if(!_context.Class.Any(c => c.ClassId == SelectedClass.ClassId))
				{
					var Class = new Class()
					{
						ClassId = SelectedClass.ClassId,
						AClassId = SelectedClass.AClassId,
						SubjectId = SelectedClass.SubjectId,
						Enrolled = SelectedClass.Enrolled,
						ClassType = SelectedClass.ClassType,
						Status = SelectedClass.Status,
						MaxNumber = SelectedClass.MaxNumber,
						Note = SelectedClass.Note,
						Experiment = SelectedClass.Experiment,
						OpenStage = SelectedClass.OpenStage,
                        Term = SelectedClass.Term
                    };
					
					_context.Class.Add(Class);
				}
				await _context.SaveChangesAsync();
			}
			
			return Json(new { success = true, message = "Success." });
		}
	}
}
