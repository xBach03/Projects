namespace ASP.net.MiddleWare
{
	public class SecondMiddleWare : IMiddleware
	{
		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			if(context.Request.Path == "/xxx.html")
			{
				context.Response.Headers.Add("SecondMiddleWare", "You are not allowed to conenct to this path");
				var datafromFirstMiddleWare = context.Items["DataFirstMiddleWare"];
				if(datafromFirstMiddleWare != null)
				{
					await context.Response.WriteAsync((string)datafromFirstMiddleWare);
				}
				await context.Response.WriteAsync("You are not allowed to conenct to this path");
			} 
			else
			{
				context.Response.Headers.Add("SecondMiddleWare", "You are allowed to connect to this path");
				var datafromFirstMiddleWare = context.Items["DataFirstMiddleWare"];
				if (datafromFirstMiddleWare != null)
				{
					await context.Response.WriteAsync((string)datafromFirstMiddleWare);
				}
				await next(context);
			}
		}
	}
}
