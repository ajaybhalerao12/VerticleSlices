namespace Newsletter.Api.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder RegisterNSwagMiddleware(this IApplicationBuilder app)
        {
            // Add OpenAPI 3.0 document serving middleware
            // Available at: http://localhost:<port>/swagger/v1/swagger.json
            app.UseOpenApi();
            // Add web UIs to interact with the document
            // Available at: http://localhost:<port>/swagger
            app.UseSwaggerUi();
            return app;
        }
    }
}
