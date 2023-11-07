namespace ASP.net03
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddRazorPages();

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

			app.UseEndpoints((endpoints) =>
			{
				endpoints.MapGet("/", async (context) =>
				{				
					var menu = HtmlHelper.MenuTop(HtmlHelper .DefaultMenuTopItems(),context.Request);

					var html = HtmlHelper.HtmlDocument("Welcome", menu + "Hellow".HtmlTag("h1, text-danger"));

					await context.Response.WriteAsync(html);
				});
				endpoints.MapGet("/RequestInfo", async (context) =>
				{
					await context.Response.WriteAsync("Request Info");
				});
				endpoints.MapGet("/Encoding", async (context) =>
				{
					await context.Response.WriteAsync("Encoding");
				});
				endpoints.MapGet("/Cookies", async (context) =>
				{
					await context.Response.WriteAsync("Cookies");
				});
				endpoints.MapGet("/Json", async (context) =>
				{
					await context.Response.WriteAsync("Json");
				});
				endpoints.MapGet("/Form", async (context) =>
				{
					await context.Response.WriteAsync("Form");
				});
			});

			app.MapRazorPages();

			app.Run();
		}
	}
}