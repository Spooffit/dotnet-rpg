namespace dotnet_rpg.Web.Middleware.ExceptionHandler;

public static class ExceptionHandlerMiddlewareExtension
{
    public static IApplicationBuilder UseExceptionHandlerExtension(this IApplicationBuilder app)
    {
        if (app == null)
        {
            throw new ArgumentNullException(nameof(app));
        }
        
        return app.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}