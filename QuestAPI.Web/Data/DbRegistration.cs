namespace QuestAPI.Web.Data
{
    public static class DbRegistration
    {
        public static IApplicationBuilder SeedData(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var initializer = scope.ServiceProvider.GetRequiredService<IDbInit>();
            initializer.Initialize();

            return app;
        }
    }
}
