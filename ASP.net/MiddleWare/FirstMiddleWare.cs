namespace ASP.net.MiddleWare
{
	public class FirstMiddleWare
	{
		private readonly RequestDelegate _next;
		// RequestDelegate ~ async (HttpContext) => {}
		public FirstMiddleWare(RequestDelegate next)
		{
			_next = next;
		}
		public async Task InvokeAsync(HttpContext context)
		{
			Console.WriteLine($"URL: {context.Request.Path}");
			context.Items.Add("DataFirstMiddleWare", $"<p>URL: {context.Request.Path}<p>");
			//await context.Response.WriteAsync();
			await _next(context);
		}
	}
}
