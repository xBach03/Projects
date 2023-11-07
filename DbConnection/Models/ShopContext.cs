using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF
{
	public class ShopContext : DbContext
	{
		public DbSet<Product> Products { set; get; }
		public DbSet<Category> Category { set; get; }

		private const string connectionString = "server = DESKTOP-KJRTV58\\SQLEXPRESS; database = ShopData; integrated security = true; TrustServerCertificate = True";
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			optionsBuilder.UseSqlServer(connectionString);
			
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			//var entity = modelBuilder.Entity<Product>();
			modelBuilder.Entity<Product>(entity =>
			{
				// entity -> fluent api
				// table mapping
				entity.ToTable("Products");
				entity.HasKey(p => p.ProductID);
				entity.HasIndex(p => p.Price);
			});
		}
	}
}