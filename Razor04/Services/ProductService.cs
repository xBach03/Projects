using Razor04.Models;
using System.Linq;
namespace Razor04.Services
{
	public class ProductService
	{
		private List<Product> products = new List<Product>();
		public ProductService()
		{
			LoadProducts();
		}
		public void LoadProducts()
		{
			products.Clear();
			products.Add(new Product() { Id = 1, Name = "Iphone X", Description = "Apple's phone", Price = 1000 });
			products.Add(new Product() { Id = 1, Name = "Samsung Note 9", Description = "Samsung's phone", Price = 980 });
			products.Add(new Product() { Id = 1, Name = "Nokia 1280", Description = "Nokia's phone", Price = 450 });
		}
		public Product Find(int id)
		{
			var qr = from p in products
					 where p.Id == id
					 select p;
			return qr.FirstOrDefault();
		}
		public List<Product> AllProducts()
		{
			return products;
		}
	}
}
