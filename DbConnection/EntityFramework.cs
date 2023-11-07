using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EF
{
	class EntityFramework
	{
		static void CreateDb()
		{
			using var dbcontext = new ShopContext();
			string dbname = dbcontext.Database.GetDbConnection().Database;

			var result = dbcontext.Database.EnsureCreated();
			if (result)
			{
				Console.WriteLine($"Create database {dbname} successfully");
			} else
			{
				Console.WriteLine($"Cannot create {dbname}");
			}
		}
		static void DropDb()
		{
			using var dbcontext = new ShopContext();
			string dbname = dbcontext.Database.GetDbConnection().Database;

			var result = dbcontext.Database.EnsureDeleted();
			if (result)
			{
				Console.WriteLine($"Delete database {dbname} successfully");
			}
			else
			{
				Console.WriteLine($"Cannot delete {dbname}");
			}
		}
		static void InsertData()
		{
			using var dbcontext = new ShopContext();

			//Category c1 = new Category() { Name = "Phones", Description = "Mobile phones" };
			//Category c2 = new Category() { Name = "Beverages", Description = "Beverages" };

			//dbcontext.Category.Add(c1);
			//dbcontext.Category.Add(c2);
			var c1 = (from c in dbcontext.Category where c.CategoryId == 1 select c).FirstOrDefault();
			var c2 = (from c in dbcontext.Category where c.CategoryId == 2 select c).FirstOrDefault();

			dbcontext.Add(new Product() { ProductName = "iPhone X", Price = 1000, CategoryId = 1 });
			dbcontext.Add(new Product() { ProductName = "Samsung S20", Price = 1050, Category = c1 });
			dbcontext.Add(new Product() { ProductName = "Monster Energy", Price = 20, Category = c2 });
			dbcontext.Add(new Product() { ProductName = "Huawei Nova 3", Price = 500, Category = c1 });
			dbcontext.Add(new Product() { ProductName = "Iced Tea", Price = 10, Category = c2 });
			int newRows = dbcontext.SaveChanges();
			Console.WriteLine($"Added {newRows} row(s)");
		}
		static void DeleteData()
		{
			using var dbcontext = new ShopContext();
			Category c1 = new Category() { Name = "iPhone", Description = "Phones"};
			Category c2 = new Category() { Name = "Iced tea", Description = "Beverages" };

			dbcontext.Remove(c1);
			dbcontext.Remove(c2);
			int newRows = dbcontext.SaveChanges();
			Console.WriteLine($"Deleted {newRows} row(s)");
		}
		static void InsertProduct()
		{
			using var dbcontext = new ShopContext();
			//var p2 = new Product()
			//{
			//	ProductName = "Product 2",
			//	Provider = "Provider 2"
			//};
			//dbcontext.Add(p2);
			var products = new Product[]
			{
				//new Product() {ProductName = "Product 3", Provider = "Provider A"},
				//new Product() {ProductName = "Product 4", Provider = "Provider B"},
				//new Product() {ProductName = "Product 5", Provider = "Provider C"},
			};
			dbcontext.AddRange(products);
			int newRows = dbcontext.SaveChanges();
			Console.WriteLine($"Inserted {newRows} row(s)");

		}
		static void ReadProducts()
		{
			using var dbcontext = new ShopContext();
			//Linq
			var products = dbcontext.Products.ToList();
			products.ForEach(product => product.Print());
		}
		static void Rename(int id, string newName)
		{
			using var dbcontext = new ShopContext();
			Product? product = (from p in dbcontext.Products
							   where p.ProductID == id
							   select p).FirstOrDefault();
			if(product!= null)
			{
				product.ProductName = newName;
				int rowNumber = dbcontext.SaveChanges();
				Console.WriteLine($"{rowNumber} row(s) updated");
			}
		}
		public delegate void ptr(string msg);
		static void Main(string[] args)
		{
			// Entity ->  Database, Table
			// Database - SQL Server: data01 -> DbContext

			// -- product

			//InsertProduct();
			//Rename(4, "Smartphone"); // Delegate
			//ReadProducts();
			//DropDb();
			//CreateDb();
			//DeleteData();

			//using var dbcontext = new ShopContext();
			//var product = (from p in dbcontext.Products where p.ProductID == 3 select p).FirstOrDefault();
			//var e = dbcontext.Entry(product);
			//e.Reference(p => p.Category).Load();
			//product?.Print();
			//if (product?.Category != null)
			//{
			//	Console.WriteLine($"{product.Category.Name} - {product.Category.Description}");
			//}
			//else
			//{
			//	Console.WriteLine("Category = null");
			//}
			//InsertData();
			//using var dbcontext = new ShopContext();
			//var category = (from c in dbcontext.Category where c.CategoryId == 2 select c).FirstOrDefault();
			//Console.WriteLine($"{category.CategoryId} - {category.Name}");

			//var e = dbcontext.Entry(category);
			//e.Collection(c => c.Products).Load();
			//if (category.Products != null)
			//{
			//	category.Products.ForEach(p => p.Print());
			//} else
			//{
			//	Console.WriteLine("Products == null");
			//}
			DropDb();
			CreateDb();
			

		}
	}
}