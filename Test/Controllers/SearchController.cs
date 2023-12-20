using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
		public IActionResult UserSearch(string userName)
		{
			List<User> Users = _context.Users.FromSqlInterpolated($"SELECT * FROM [Test].[dbo].[Users]").ToList();
            foreach(var User in Users)
			{

                _logger.LogInformation(User.UserName);
            }
            return Json(Users);
		}
	}
}
