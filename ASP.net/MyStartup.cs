using Microsoft.AspNetCore.Builder;

namespace ASP.net
{
	public class MyStartup
	{
		// Dang ky cac dich vu cua ung dung (dependency injection)
		public void ConfigureServices(IServiceCollection services)
		{
			//services.AddSingleton
		}
		// Xay dung pipeline (chuoi middleware)
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			// wwwroot
			app.UseStaticFiles();
			// request
			// endpoints middleware

			app.UseRouting();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapGet("/", async (context) =>
				{
					await context.Response.WriteAsync("Main page");
				});
				endpoints.MapGet("/about", async (context) =>
				{
					await context.Response.WriteAsync("About page");
				});
				endpoints.MapGet("/contact", async (context) =>
				{
					await context.Response.WriteAsync("Contact page");
				});
			});
			app.Map("/abc", (app1) => {
				app1.Run(async (HttpContext context) =>
				{
					await context.Response.WriteAsJsonAsync("This is responses from abc");
				});
			});
			app.UseStatusCodePages();
			//app.Run(async (HttpContext context) => {
			//	await context.Response.WriteAsync("This is my startup");
			//});


		}
	}
}
