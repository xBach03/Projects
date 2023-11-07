namespace Design_Patterns
{
	public class Program
	{
		static void Main(string[] args)
		{
			for(int i = 1; i < 6; i++)
			{
				Ticket soldTicket = new Ticket(new NoDiscount());
				var random = new Random();
				var pmt = random.Next(0, 2);
				soldTicket.Pricer = 50 * i;
				switch (pmt)
				{
					case 0:
						soldTicket.PromoteStrateger = new NoDiscount();
						break;
					case 1:
						soldTicket.PromoteStrateger = new QuarterDiscount();
						break;
					default:
						soldTicket.PromoteStrateger = new HalfDiscount();
						break;
				}
				Console.WriteLine($"The orginal price of this ticket is {soldTicket.Pricer} and after discount is {soldTicket.getActualPrice()}");
			}
		}
	}
}