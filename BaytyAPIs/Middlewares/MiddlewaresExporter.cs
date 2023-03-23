namespace BaytyAPIs.Middlewares
{
    public static class MiddleWareExporter
    {
        public static IApplicationBuilder UseRefreshTokenHandler(this IApplicationBuilder app) => app.UseMiddleware<RefreshTokenMWHandler>();
    }
}
