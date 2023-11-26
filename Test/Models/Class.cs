using Test.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Models
{
	[Table("Class")]
	public class Class
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int ClassId { set; get; }
		public int TimetableId { set; get; }
		[ForeignKey("TimetableId")]
		public PTimetable pTimetable { set; get; }
		public string SubjectId { set; get; }
		[ForeignKey("SubjectId")]
		public Subject subject { set; get; }
		public int? AClassId { set; get; }
		public int Weekday { set; get; }
		public string Time { set; get; }
		public string ClassRoom { set; get; } 
		public int? Enrolled { set; get; }
		public int? MaxNumber { set; get; }
		public string ClassType { set; get; }
		public int Term { set; get; }
		public string Week { set; get; }
		public string Status { set; get; }
		public string Note { set; get; }
	}
}
