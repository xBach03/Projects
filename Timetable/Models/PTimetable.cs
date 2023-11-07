using Project.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
	[Table("Timetable")]
	public class PTimetable
	{
		[Key]
		public int TimetableId { set; get; }
		public DateTime CreatedDate { set; get; }
		public string? UserId { set; get; }
		[ForeignKey("UserId")]
		public User? User { set; get; }

	}
}
