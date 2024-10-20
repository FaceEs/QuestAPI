using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using QuestAPI.Web.Data;
using QuestAPI.Web.Services.PlayerQuest;
using QuestAPI.Web.Services.PlayerService;
using QuestAPI.Web.Services.Quest;
using Swashbuckle.AspNetCore.Swagger;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateSlimBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<QuestDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddScoped<IQuestService, QuestService>();
builder.Services.AddScoped<IPlayerQuestService, PlayerQuestService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IDbInit, DbInit>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.UseAllOfToExtendReferenceSchemas();
});
var app = builder.Build();
app.UseStaticFiles();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c =>
    {
        c.RouteTemplate = "api/swagger/{documentname}/swagger.json";
    });
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/api/swagger/v1/swagger.json", "QuestAPI V1");
        c.RoutePrefix = "api/swagger";
    });
    app.UseDeveloperExceptionPage();
}
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
}
);
app.SeedData();
app.Run();
