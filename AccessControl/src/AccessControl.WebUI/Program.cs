using AccessControl.Infrastructure;
using AccessControl.Infrastructure.Data;
using AccessControl.WebUI.Policies;
using FClub.Backend.Common.Middleware;
using FClub.Backend.Common.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//------------------------// Custom //------------------------//
builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.Services.AddPolicies(builder.Configuration);
builder.Services.AddCustomErrorHandling();
builder.Services.AddCustomSwagger(options =>
{
    builder.Configuration.GetSection("Swagger").Bind(options);
});
//------------------------------------------------------------//

var app = builder.Build();

//------------------------// Custom //------------------------//
app.UseInfrastructure(builder.Configuration);
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
