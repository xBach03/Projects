namespace ASP.netRazor
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddRazorPages().AddRazorPagesOptions(options =>
			{
				options.RootDirectory = "/Pages";
				options.Conventions.AddPageRoute("/FirstPage", "/1stPg");
				options.Conventions.AddPageRoute("/SecondPage", "/2ndPg");
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoint =>
			{
				endpoint.MapRazorPages();
				endpoint.MapGet("/", async context =>
				{
					await context.Response.WriteAsync("Hello ASP.net RAZOR");
				});

			});

			app.Run();
		}
	}
}