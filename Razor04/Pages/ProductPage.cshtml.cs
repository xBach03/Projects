using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor04.Services;
using Razor04.Models;
namespace Razor04.Pages
{
	public class ProductPageModel : PageModel
	{
		public readonly ProductService _productService;
		private readonly ILogger<ProductPageModel> _logger;
		public ProductPageModel(ILogger<ProductPageModel> logger, ProductService productService)
		{
			_logger = logger;
			_productService = productService;
		}
		public Product product { set; get; }
		public readonly ProductService productService;
		public void OnGet(int? id)
		{
			if(id != null)
			{
				ViewData["Title"] = $"Product (ID = {id.Value})";
				product = productService.Find(id.Value);
			}
			else
			{
				ViewData["Title"] = $"Product list";
			}
		}
	}
}

