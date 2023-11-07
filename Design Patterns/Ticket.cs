using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Patterns
{
	internal class Ticket
	{
		private double price;
		IPromoteStrategy promoteStrategy;
		private string? name;

		public double Pricer
		{
			set { price = value; }
			get { return price; }
		}
		public IPromoteStrategy PromoteStrateger
		{
			set { promoteStrategy = value; }
			get { return promoteStrategy; }
		}
		public string? Namer
		{
			set { name = value; }
			get { return name; }
		}
		//public double getPrice()
		//{
		//	return price;
		//}
		//public void setPrice(double _price)
		//{
		//	price = _price;
		//}
		//public IPromoteStrategy getPromoteStrategy()
		//{
		//	return promoteStrategy;
		//}
		//public void setPromoteStrategy(IPromoteStrategy strategy)
		//{
		//	promoteStrategy = strategy;
		//}

		public Ticket(IPromoteStrategy Strategy)
		{
			promoteStrategy = Strategy;
		}

		

		public double getActualPrice()
		{
			return promoteStrategy.DoDiscount(price);
		}
	}
}
