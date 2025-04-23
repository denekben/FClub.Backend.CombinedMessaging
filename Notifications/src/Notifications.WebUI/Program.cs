using FClub.Backend.Common.Middleware;
using FClub.Backend.Common.Swagger;
using Notifications.Application;
using Notifications.Infrastructure;
using Notifications.Infrastructure.Data;
using Notifications.WebUI.Policies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//------------------------// Custom //------------------------//
builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.Services.AddApplicationLayer(builder.Configuration);
builder.Services.AddPolicies(builder.Configuration);
builder.Services.AddCustomErrorHandling();
builder.Services.AddCustomSwagger(options =>
{
    builder.Configuration.GetSection("Swagger").Bind(options);
});
//------------------------------------------------------------//

var app = builder.Build();

//------------------------// Custom //------------------------//
app.UseCustomErrorHandling();
if (app.Environment.IsProduction())
{
    await PrepDb.ApplyMigrationsAsync<AppDbContext>(app.Services);
    await PrepDb.ApplyMigrationsAsync<AppLogDbContext>(app.Services);
}
//------------------------------------------------------------//

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
