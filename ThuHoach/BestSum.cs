// See https://aka.ms/new-console-template for more information
using System;
namespace BestSum
{
    class Solve
    {
        public static int sum = 0;
        public static void Sort(int[] a)
        {
            for (int i = 1; i < a.Length; i++)
            {
                int key = a[i];
                int j = i - 1;
                while (j >= 0 && a[j] < key)
                {
                    a[j + 1] = a[j];
                    j--;
                }
                a[j + 1] = key;
            }
        }
        // 5 259
        // 81 61 58 42 33
        //public static int findMax(int a, int b) => (a > b) ? a : b;

        public static int findMax(int a, int b)
        {
            return (a > b) ? a : b;
        }
        public static int Calc(ref int mark, ref int n, int[] a)
        {
            if (mark == a.Length - 1) return sum;
            int i;
            for (i = mark; i < a.Length - 1; i++)
            {
                if (n - a[i] >= 0)
                {
                    n -= a[i];
                    sum += a[i];
                }
                else
                {
                    i += 1;
                    break;
                }
            }
            return Calc(ref i, ref n, a);

        }
    }
	
	class Program
    {
          
         

        public class Solution1
		{
			public static bool IsValid(string s)
			{
                if (s == "(]" || s == "(}" || s == "[}" || s == "[)" || s == "{)" || s == "{]") return false;
                Stack<char> strs = new Stack<char>();
				string ans = new string("");
				for (int i = 0; i < s.Length; i++)
                {
                    if (s[i] == '(' || s[i] == '[' || s[i] == '{')
                    {
                        strs.Push(s[i]);
                    } else
                    {
                        if(strs.Count == 0 || s[i] == ')' && strs.Peek() != '(' || s[i] == ']' && strs.Peek() != '[' || s[i] == '}' && strs.Peek() != '{')
                        {
                            return false;
                        }
                        strs.Pop();
					}
                    
                }
                if (strs.Count() == 0) return true;
                return false;
			}
		}

		public delegate int Sum(int a, int b);
        public static int Solve(int[] a, int target)
        {
            int left = 0, right = a.Length - 1;
            if (a[a.Length - 1] < target) return a.Length;
            while(left <= right)
            {
                int mid = (left + right) / 2;
                if (a[mid] == target) return mid;
                else if (a[mid] > target) right = mid - 1;
                else left = mid + 1;
            }
            return left;
        }
        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;
            public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
            {
                this.val = val;
                this.left = left;
                this.right = right;
            }
        }
        public class Solution
        {
            public IList<int> list = new List<int>();
            public IList<int> InorderTraversal(TreeNode root)
            {
                if (root == null) return new List<int>();
                if (root.left == null && root.right == null) {
                    return new List<int>(root.val);
                }
                Traverse(root);
                return list;

            }
            public void Traverse(TreeNode node)
            {
                if (node == null) return;
                Traverse(node.left);
                list.Add(node.val);
                Traverse(node.right);
            }
        }
        static void Main(string[] args)
        {
            //string input1 = Console.ReadLine();
            //string[] inputs1 = input1.Split(' ');
            //int n = Convert.ToInt32(inputs1[0]);
            //int target = Convert.ToInt32(inputs1[1]);
            //int[] a = new int[n];
            //string input2 = Console.ReadLine();
            //string[] inputs2 = input2.Split(' ');
            //for (int j = 0; j < n; j++)
            //{
            //    a[j] = Convert.ToInt32(inputs2[j]);
            //}
            //Solve.Sort(a);
            //int i = 0;
            //Console.WriteLine(Solve.Calc(ref i, ref target, a));

            //int[] a = new int[] { 1, 3, 5, 6 };
            //int target = 2;
            //Console.WriteLine(Solve(a, target));

            //Sum s1 = (int a, int b) => { return a + b; };
            //int c = s1(5, 2);
            //Console.Write(c);


        }
    }
}
