using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Test.Models
{
	public class AppDbContext : IdentityDbContext<User>
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
		}
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			foreach (var entityType in builder.Model.GetEntityTypes())
			{
				var tableName = entityType.GetTableName();
				if (tableName.StartsWith("AspNet"))
				{
					entityType.SetTableName(tableName.Substring(6));
				}
			}
			//builder.Entity<Subject>().Property(s => s.SubjectId).ValueGeneratedOnAdd();
			//builder.Entity<Class>().Property(c => c.ClassId).ValueGeneratedOnAdd();
			builder.Entity<TempTimetable>().Property(t => t.ClassRoom ).IsRequired(false);
			builder.Entity<TempTimetable>().Property(t => t.Note).IsRequired(false);
			builder.Entity<TempTimetable>().Property(t => t.ClassType).IsRequired(false);
			builder.Entity<TempTimetable>().Property(t => t.EduProgram).IsRequired(false);
			builder.Entity<TempTimetable>().Property(t => t.Week).IsRequired(false);
			builder.Entity<TempTimetable>().Property(t => t.Experiment).IsRequired(false);
			builder.Entity<TempTimetable>().Property(t => t.Status).IsRequired(false);
			builder.Entity<TempTimetable>().Property(t => t.SubjectName).IsRequired(false);
			builder.Entity<TempTimetable>().Property(t => t.ESubjectName).IsRequired(false);
			builder.Entity<TempTimetable>().Property(t => t.School).IsRequired(false);
			builder.Entity<TempTimetable>().Property(t => t.SubjectId).IsRequired(false);
			builder.Entity<TempTimetable>().Property(t => t.Time).IsRequired(false);
			builder.Entity<TempTimetable>().Property(t => t.Difficulty).IsRequired(false);
			builder.Entity<Class>().HasKey(c => new { c.ClassId, c.TimetableId });
		}
		public DbSet<PTimetable> Timetables { set; get; }
		public DbSet<Class> Classes { set; get; }
		public DbSet<Subject> Subjects { set; get; }
		public DbSet<TempTimetable> TempTables { set; get; }
	}
}
