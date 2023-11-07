namespace MyProject.Models
{
	public class TempTimetable
	{
		public string? School { set; get; }
		public int ClassCode { set; get; }
		public int? AClassCode { set; get; }
		public string SubjectCode { set; get; }
		public string SubjectName { set; get; }
		public int Term { set; get; }
		public DateTime Start { set; get; }
		public DateTime End { set; get; }
		public string ClassRoom { set; get; }
		public int? Enrolled { set; get; }
		public int? MaxEnrolling { set; get; }
		public string ClassType { set; get; }
		public string EProgram { set; get; }
	}
}
