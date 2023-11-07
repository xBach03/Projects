// See https://aka.ms/new-console-template for more information
using System;
using Delegate;
using Event;
namespace FirstCSharp
{
	class MainFunc
	{
		public delegate int SaySth(string s);
		public static int Talk(string s)
		{
			Console.WriteLine(s);
			return 10;
		}
		static void Main(string[] args)
		{
			//string name = "Xbach";
			//int age = 20;
			//Console.WriteLine(name + " is not " + age);
			//double pi = 3.14;
			//double pi2 = 2 * pi;
			//Console.WriteLine(pi + " " + pi2);
			//string myname=;
			//myname = Console.ReadLine();
			//Console.WriteLine("Xin chao " + myname);
			//Console.WriteLine(3 + 3);
			//int a, b;
			//a = Convert.ToInt32(Console.ReadLine());
			//b = Convert.ToInt32(Console.ReadLine());
			//Console.WriteLine($"a = {a}, b = {b}", a, b);
			//var c = "XBach";
			//int a, b;
			//a = Convert.ToInt32(Console.ReadLine());
			//b = Convert.ToInt32(Console.ReadLine());
			//Console.WriteLine("a + b = {0}", a + b);
			//Console.WriteLine("a - b = {0}", a - b);
			//int a, b;
			//a = Convert.ToInt32(Console.ReadLine());
			//b = Convert.ToInt32(Console.ReadLine());
			//Console.WriteLine((a==b));
			//int c;
			//c = Convert.ToInt32(Console.ReadLine());
			//if (c % 2 == 0)
			//{
			//    Console.WriteLine("This is an even number");
			//}
			//else
			//{
			//    Console.WriteLine("This is an odd number");
			//}
			//Console.WriteLine(9 > 8 ? 9 : 8);
			//Console.WriteLine("Nhap a: ");
			//int a = Convert.ToInt32(Console.ReadLine());
			//Console.WriteLine("Nhap b: ");
			//int b = Convert.ToInt32(Console.ReadLine());
			//Console.WriteLine("Chon cac phuong thuc: ");
			//Console.Write("1. Tinh tong \n2. Tinh hieu \n3. Tinh tich \n4. Tinh thuong");
			//Console.WriteLine();
			//Console.Write("Chon c: ");
			//char c = Convert.ToChar(Console.ReadLine());
			//switch(c)
			//{
			//    case '1':
			//        Console.WriteLine("Tong a b la: {0}", a + b);
			//        break;
			//    case '2':
			//        Console.WriteLine("Hieu a b la : {0}", a - b);
			//        break;
			//    case '3':
			//        Console.WriteLine("Tich a b la: {0}", a * b);
			//        break;
			//    case '4':
			//        Console.WriteLine("Thuong a va b la: {0}", a / b);
			//        break;
			//    default:
			//        Console.WriteLine("Phuong thuc ko ton tai");
			//        break;
			//}
			//int x = 5;
			//for (int i = 0; i < 10; i++)
			//{
			//    Console.WriteLine($"{i+1} Bach yeu My");
			//}
			//string[] lover = new string[2];
			//lover[0] = "Dao Ngoc Tra My";
			//lover[1] = "MeoMeo";
			//for (int i = 0; i < 2; i++)
			//{
			//    Console.WriteLine($"Bach yeu {lover[i]}");
			//} 
			//int[] a = { 5, 7, 1, 8, 9, 3, 2 };
			//Console.WriteLine($"Max cua mang: {a.Max()}");
			//Console.WriteLine($"Min cua mang: {a.Min()}");
			//Console.WriteLine($"Tong cua mang: {a.Sum()}");
			//FirstCSharp.Function.Hellow("Bach", "Dang");
			//string input = Console.ReadLine();
			//string[] inputs = input.Split(' ');
			//int input1 = Convert.ToInt32(inputs[0]);
			//int input2 = Convert.ToInt32(inputs[1]);
			//Console.WriteLine($"sum: {input1 + input2}");
			//Console.WriteLine($"Multiple: {FirstCSharp.Function.Tich(input1, input2)}");
			//int a = Convert.ToInt32(Console.ReadLine());
			//FirstCSharp.Function.up(ref a);
			//Console.WriteLine(a);
			//Weapon item = new Weapon("Gun", 150, 30);
			//Console.WriteLine($"{item.name} {item.damage} {item.defense}");
			//item.attack();
			//item.DMG = 180;
			//Console.WriteLine(item.DMG);
			//Weapon[] item1 = new Weapon[100000];
			//for (int i = 0; i < 100000; i++)
			//{
			//	Console.WriteLine(i);
			//	item1[i] = new Weapon("Lazer", 200, 10);
			//	item1[i] = null;
			//}
			//int n = Convert.ToInt32(Console.ReadLine());
			//Console.WriteLine($"{Recursion.SqrtSum(n)}");
			//int n = 0;
			//int[] a = new int[5] { 81, 58, 42, 33, 61 };
			//int target = 242;
			//BestSum.Sort(a);
			//Console.WriteLine(BestSum.Calc(ref n, ref target, a));
			//Mage Leb = new Mage();
			//Leb.name = "Leblanc";
			//Console.WriteLine(Leb.abilityPower);
			//string ?b = null;
			//Console.WriteLine(BestSum.findMax(7, 5));
			//iPhone myPhone = new iPhone();
			//myPhone.ProductInfo();
			//ShowLog show = Program.Say;
			//if (show != null)
			//{
			//    show("Hellow");
			//}
			//Func<int, string, string> Communicate = Program.Talk;
			//Console.WriteLine($"{Communicate(20213813, "Dang Xuan Bach")}");
			//Console.WriteLine();

			//UserInput input = new UserInput();
			//Calc calculator = new Calc();
			//calculator.Sub(input);
			//input.Input();

			string s = "Hellow Tra My";
			//s.Print(ConsoleColor.Yellow);
			//Console.ResetColor();
			//double x = 26;
			//Console.WriteLine($"{x.Square()}");
			//Extend.Info();
			SaySth Communicate;
			Communicate = Talk;
			Console.WriteLine(Communicate(s));

		}
	}
}
