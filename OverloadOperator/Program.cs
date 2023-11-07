// See https://aka.ms/new-console-template for more information
using LINQ;
using Newtonsoft.Json;
using System;
using System.Text;
namespace Program
{
	class Vector
	{
		double x;
		double y;
		public Vector(double _x, double _y)
		{
			x = _x;
			y = _y;
		}
		public static Vector operator +(Vector v1, Vector v2)
		{
			return new Vector(v1.x + v2.x, v1.y + v2.y);
		}
		public static Vector operator ++(Vector v1)
		{
			return new Vector(v1.x + 1, v1.y + 1);
		}
		public void Print()
		{
			Console.WriteLine($"x = {x}, y = {y}");
		}
	}
	class Champion
	{
		public string Name { set; get; }
		public string Category { set; get; }
		public int Damage { set; get; }
		public int Defense { set; get; }
	} 
	class Run
	{
		public delegate int Ptr(int a, int b);
		static void Main1(string[] args)
		{
			//Vector v1 = new Vector(2, 3);
			//Vector v2 = new Vector(5, 7);
			//Vector v3 = v1 + v2;
			//v3.Print();
			//v2++;
			//v2.Print();
			//int a = 5;
			//int b = 0;
			//try
			//{
			//    Console.WriteLine(a / b);
			//}
			//catch
			//{
			//    Console.WriteLine("Cannot calculate");
			//}
			//int[] array = new int[5] { 4, 6, 3, 2, 5 };
			//foreach(var n in array)
			//{
			//    Console.Write(n + " ");
			//}
			List<Champion> ChampList = new List<Champion>()
			{
				new Champion()
				{
					Name = "Leblanc", Category = "Mage", Damage = 59, Defense =29
				},
				new Champion()
				{
					Name = "Sylas", Category = "Mage", Damage = 62, Defense =34
				},
				new Champion()
				{
					Name = "Caitlyn", Category = "AD", Damage = 75, Defense =25
				},
				new Champion()
				{
					Name = "Khazix", Category = "Assassin", Damage = 68, Defense =30
				}
			};
			ChampList.Sort(
				(Champion a, Champion b) =>
				{
					if (a.Damage < b.Damage) return 1;
					if (a.Damage > b.Damage) return -1;
					return 0;
				}
			);
			foreach(Champion c in ChampList)
			{
				Console.WriteLine($"Name: {c.Name} - Category: {c.Category} - Damage: {c.Damage} - Defense: {c.Defense}");
			}
			Ptr dptr = (int a, int b) =>
			{
				return a + b;
			};
			Console.WriteLine(dptr(7, 3));
			
		}
		static void Main2(string[] args)
		{
			List<Champion> ChampList = new List<Champion>()
			{
				new Champion()
				{
					Name = "Leblanc", Category = "Mage", Damage = 59, Defense = 29
				},
				new Champion()
				{
					Name = "Sylas", Category = "Mage", Damage = 62, Defense = 34
				},
				new Champion()
				{
					Name = "Caitlyn", Category = "AD", Damage = 75, Defense = 25
				},
				new Champion()
				{
					Name = "Khazix", Category = "Assassin", Damage = 68, Defense = 30
				}
			};
			using FileStream file = new FileStream(path: "D:\\VS\\xBach's Projects\\FirstCSharp\\OverloadOperator\\File1.txt", FileMode.Create);
			string[] input = new string[5];
			foreach(Champion champ in ChampList)
			{
				string x = Convert.ToString(champ.Name + " " + champ.Category + " " + champ.Damage + " " + champ.Defense + '\n');
				byte[] s = Encoding.UTF8.GetBytes(x);
				file.Write(s);
			}
		}
		
	}
}
namespace LINQ
{
	class Product
	{
		public int Id { set; get; }
		public string Name { set; get; }
		public double Price { set; get; }
		public string[] Colors { set; get; }
		public int BrandId { set; get; }
		public Product(int id, string name, double price, string[] colors, int brandid)
		{
			Id = id; Name = name; Price = price; Colors = colors; BrandId = brandid;
		}
		public override string ToString()
		{
			string s = " ";
			foreach(var x in Colors)
			{
				s = s + x + " ";
			}
			return Convert.ToString(Id + " " + Name + " " + Price + " " + s + " " + BrandId);
		}
	}
	class Brand
	{
		public string Name { set; get; }
		public int ID { set; get; }
	}
	class Use
	{
		public delegate int Ptr(int x);
		static void Main1(string[] args)
		{
			var Brands = new List<Brand>()
			{
				new Brand()
				{
					ID = 1, Name = "Pfizer"
				},
				new Brand()
				{
					ID = 2, Name = "Moderna"
				},
				new Brand()
				{
					ID = 4, Name = "AstraZen"
				}
			};
			var Products = new List<Product>()
			{
				new Product(1, "Vaccine", 400, new string[] {"Blue", "Yellow"}, 2),
				new Product(2, "Kit Test", 250, new string[] {"White", "Red"}, 1),
				new Product(3, "Lungs Medicine", 560, new string[] {"Blue", "Red"}, 3),
				new Product(4, "Face Mask", 170, new string[] {"White", "Black", "Pink"}, 1),
				new Product(5, "Urgo", 90, new string[] {"White", "Yellow"}, 2),
				new Product(6, "Painkiller", 560, new string[] {"White", "Yellow"}, 2),
				new Product(7, "Drugs", 400, new string[] {"Green", "Red", "White"}, 3)
			};
			//var Query = Products.Where(
			//	(Product p) =>
			//	{
			//		return p.Price > 260;
			//	}
			//);
			//foreach (var p in Query)
			//{
			//	Console.WriteLine(p);
			//}
			//var Query1 = Products.Join(Brands, Products => Products.BrandId, Brands => Brands.ID, (Products, Brands) =>
			//{
			//	return new
			//	{
			//		Name = Products.Name,
			//		BrandName = Brands.Name
			//	};
			//});
			//foreach (var p in Query1)
			//{
			//	Console.WriteLine(p);
			//}
			//var x = Products.OrderBy(x => { return x.Price; });
			//foreach(var c in x){
			//	Console.WriteLine(c);
			//}
			var Query2 = from p in Products
						 from c in p.Colors
						 where(p.Price < 500 && c == "White")
						select new {
								Name = p.Name,
								Price = p.Price
							};
			foreach(var p in Query2)
			{
				Console.WriteLine(p);
			}
		}
	}
}
namespace Package
{
	class user
	{
		class Product
		{
			public string Name { set; get; }
			public DateTime Expiry { set; get; }
			public string[] Sizes { set; get; }

		}
		static void Main(string[] args)
		{
			Product product = new Product();
			product.Name = "Apple";
			product.Expiry = new DateTime(2008, 12, 28);
			product.Sizes = new string[] { "Small" };

			string json = JsonConvert.SerializeObject(product);
		}
	}
}