using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF
{
	//[Table("Products")]
	public class Product
	{
		//[Key]
		public int ProductID { set; get; }

		[Required]
		[StringLength(50)]
		[Column(TypeName = "ntext")]
		public string? ProductName { set; get; }

		[Column(TypeName = "money")]
		public decimal Price { set; get; }

		public int CategoryId { set; get; }
		[ForeignKey("CategoryId")]
		[Required]
		public Category? Category { set; get; }
		public void Print() => Console.WriteLine($"{ProductID} - {ProductName} - {Price} - {CategoryId}");

	}
}
