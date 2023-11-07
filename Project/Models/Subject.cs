using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Models
{
	[Table("Subject")]
	public class Subject
	{
		[Key]
		public int SubjectId { set; get; }
		public string SubjectName { set; get; }
		public string? School { set; get; }
		public string EduProgram { set; get; }
	}
}
