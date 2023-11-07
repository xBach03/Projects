using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorEF.Models;

namespace RazorEF.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly MyBlogContext _blogContext;

        public IndexModel(ILogger<IndexModel> logger, MyBlogContext blogContext)
        {
            _logger = logger;
            _blogContext = blogContext;
        }

        public void OnGet()
        {
            var posts = (from b in _blogContext.articles
                        orderby b.Created descending
                        select b).ToList();
            ViewData["posts"] = posts;
        }
    }
}