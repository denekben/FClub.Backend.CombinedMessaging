using FClub.Backend.Common.Middleware;
using FClub.Backend.Common.Swagger;
using Management.Infrastructure;
using Management.Infrastructure.Data;
using Management.WebUI.Policies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//------------------------// Custom //------------------------//
builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.Services.AddPolicies();
builder.Services.AddCustomErrorHandling();
builder.Services.AddCustomSwagger(options =>
{
    builder.Configuration.GetSection("Swagger").Bind(options);
});
//------------------------------------------------------------//

var app = builder.Build();

//------------------------// Custom //------------------------//
app.UseCustomErrorHandling();
//------------------------------------------------------------//

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsProduction())
{
    await PrepDb.PrepPopulation(app);
}

app.Run();
