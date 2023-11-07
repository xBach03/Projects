using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Patterns
{
	public class QuarterDiscount : IPromoteStrategy
	{
		public double DoDiscount(double price)
		{
			return price * 0.75;
		}
	}
}
