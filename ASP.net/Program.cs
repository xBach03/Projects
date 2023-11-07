using ASP.net.MiddleWare;
namespace ASP.net
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddRazorPages();

			builder.Services.AddSingleton<SecondMiddleWare>();

			var app = builder.Build();

			// wwwroot
			app.UseStaticFiles();

			// request
			// endpoints middleware


			app.UseFirstMiddleWare();

			app.UseSecondMiddleWare();

			app.UseRouting();

			app.UseEndpoints((endpoint) =>
			{
				endpoint.MapGet("/about.html", async (context) =>
				{
					await context.Response.WriteAsync("About this page");
				});

				endpoint.MapGet("/products.html", async (context) =>
				{
					await context.Response.WriteAsync("Products page");
				});
			});

			app.Run(async (context) => { await context.Response.WriteAsync("Xin chao ASP.net Core"); });

			
			//app.UseEndpoints(endpoints =>
			//{
			//	endpoints.MapGet("/", async (context) =>
			//	{
			//		await context.Response.WriteAsync("Main page");
			//	});
			//	endpoints.MapGet("/about", async (context) =>
			//	{
			//		await context.Response.WriteAsync("About page");
			//	});
			//	endpoints.MapGet("/contact", async (context) =>
			//	{
			//		await context.Response.WriteAsync("Contact page");
			//	});
			//});
			//app.Map("/abc", (app1) =>
			//{
			//	app1.Run(async (HttpContext context) =>
			//	{
			//		await context.Response.WriteAsJsonAsync("This is responses from abc");
			//	});
			//});		
			app.UseStatusCodePages();

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


			
			app.MapRazorPages();

			app.Run();
			
		}
		//public static void Main(string[] args)
		//{
		//	Console.WriteLine("Starting app");
		//	IHostBuilder builder = Host.CreateDefaultBuilder();
		//	builder.ConfigureWebHostDefaults((IWebHostBuilder webBuilder) =>
		//	{
		//		webBuilder.UseWebRoot("public");
		//		// Tuy bien them ve host
		//		webBuilder.UseStartup<MyStartup>();
		//	});
		//	IHost host = builder.Build();
		//	host.Run();
		//}

	}
}