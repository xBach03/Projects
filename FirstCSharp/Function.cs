using System;

namespace FirstCSharp
{
	internal class Function
	{
		public static void Hellow(string first_name, string last_name)
		{
			Console.WriteLine($"Hellow {first_name} {last_name}");
		}
		public static int Tich(int a, int b)
		{
			return a * b;
		}
		public static int sqr(ref int a)
		{
			a = a * a;
			return a;
		}
		public static void up(ref int a)
		{
			a++;
		}
		public static void swap<T>(ref T a, ref T b)
		{
			T c = a;
			a = b;
			b = c;
		}
	}
	public class Weapon
	{
		// Du lieu
		public string name;
		public int damage;
		public int defense;

		// Thuoc tinh
		public int DMG
		{
			set
			{
				damage = value;
			}
			get
			{
				return damage;
			}
		}
		// Phuong thuc
		public Weapon()
		{
			name = null;
			damage = defense = 0;
		}
		public Weapon(string n, int dmg, int def)
		{
			name = n;
			damage = dmg;
			defense = def;
			Console.WriteLine("Weapon created");
		}
		public void setDmg(int dmg)
		{
			damage = dmg;
		}
		public void attack()
		{
			Console.WriteLine($"{name} attacked with {damage} damage");
		}
		~Weapon()
		{
			Console.WriteLine($"Weapon {name} destroyed");
			name = null;
			damage = defense = 0;
		}

	}
	internal class Recursion
	{
		public static double SqrtSum(int n)
		{
			if (n == 0) return 0;
			return Math.Sqrt(1 + SqrtSum(n - 1));
		}
	}
	class Champion
	{
		public string name;
		public int age;
		public int damage, abilityPower, defense, movespeed;
		public Champion()
		{
			name = " ";
			age = damage = defense = movespeed = abilityPower = 0;
		}
	}
	class Mage : Champion
	{
		string item;
		public Mage()
		{
			abilityPower = 30;
			defense = 30;
			movespeed = 300;
		}
	}
	class Product
	{
		protected double Price
		{
			set; get;
		}
		public virtual void ProductInfo()
		{
			Console.WriteLine($"The price of this product is {Price}");
		}
	}
	class iPhone : Product
	{
		//public override void ProductInfo()
		//{
		//	Console.WriteLine("This product is an iPhone");
		//}
		public iPhone() => Price = 500;

	}
	public static class Extend
	{
		public static void Print(this string s, ConsoleColor color)
		{
			Console.ForegroundColor = color;
			Console.WriteLine(s);
		}
		public static double Square(this double x) => x * x;
		public static void Info()
		{
			Console.WriteLine("Number of entries: ");
		}
	}
}
namespace Delegate
{
	public delegate void ShowLog(string message);
	public class Program
	{
		public static void Say(string s)
		{
			Console.WriteLine(s);
		}
		public static string Talk(int i, string s)
		{
			string x = s + " " + i.ToString();
			return x;
		}
	}

}
namespace Event
{
	public delegate void InputEvent(int x);
	public class UserInput
	{
		public InputEvent Event1 { set; get; }
		public void Input()
		{
			do
			{
				Console.Write("Enter an interger: ");
				string s = Console.ReadLine();
				int i = Convert.ToInt32(s);
				Event1?.Invoke(i);
			} while (true);

		}
	}
	public class Calc
	{
		public void Square(int i)
		{
			Console.WriteLine($"The square multiply of {i} is {i * i}");
		}
		public void Sub(UserInput input)
		{
			input.Event1 = Square;
		}
	}
}
