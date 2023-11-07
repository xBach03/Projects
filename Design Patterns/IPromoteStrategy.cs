using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Patterns
{
	public interface IPromoteStrategy
	{
		double DoDiscount(double price);
	}
}
