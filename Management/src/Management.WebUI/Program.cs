using FClub.Backend.Common.Middleware;
using FClub.Backend.Common.Swagger;
using Management.Application;
using Management.Infrastructure;
using Management.Infrastructure.Data;
using Management.WebUI.Policies;

var builder = WebApplication.CreateBuilder(args);

//------------------------// Custom //------------------------//
builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.Services.AddApplicationLayer();
builder.Services.AddPolicies(builder.Configuration);
builder.Services.AddCustomErrorHandling();
builder.Services.AddCustomSwagger(options =>
{
    builder.Configuration.GetSection("Swagger").Bind(options);
});
//------------------------------------------------------------//

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//------------------------// Custom //------------------------//
app.UseCustomErrorHandling();
if (app.Environment.IsProduction())
    await PrepDb.ApplyMigrationsAsync<AppDbContext>(app.Services);
//------------------------------------------------------------//

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
