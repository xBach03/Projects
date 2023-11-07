using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Test.Models;

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
			builder.Entity<TempTimetable>().Property(e => e.ClassRoom).IsRequired(false);
			builder.Entity<TempTimetable>().Property(e => e.Note).IsRequired(false);
			builder.Entity<TempTimetable>().Property(e => e.ClassType).IsRequired(false);
			builder.Entity<TempTimetable>().Property(e => e.EduProgram).IsRequired(false);
			builder.Entity<TempTimetable>().Property(e => e.Week).IsRequired(false);
			builder.Entity<TempTimetable>().Property(e => e.Experiment).IsRequired(false);
			builder.Entity<TempTimetable>().Property(e => e.Status).IsRequired(false);
			builder.Entity<TempTimetable>().Property(e => e.SubjectName).IsRequired(false);
			builder.Entity<TempTimetable>().Property(e => e.ESubjectName).IsRequired(false);
			builder.Entity<TempTimetable>().Property(e => e.School).IsRequired(false);
			builder.Entity<TempTimetable>().Property(e => e.SubjectId).IsRequired(false);
			builder.Entity<TempTimetable>().Property(e => e.Time).IsRequired(false);
			builder.Entity<TempTimetable>().Property(e => e.Difficulty).IsRequired(false);

		}
		public DbSet<PTimetable> Timetables { set; get; }
		public DbSet<Class> Classes { set; get; }
		public DbSet<Subject> Subjects { set; get; }
		public DbSet<TempTimetable> TempTables { set; get; }
	}
}
