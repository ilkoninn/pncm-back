using Microsoft.OpenApi.Models;
using App.Business;
using App.DAL;
using App.API;
using App.DAL.Presistence;
using App.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "App.API", Version = "v1" });
});


builder.Services
    .AddShared(builder.Configuration)
    .AddDataAccess(builder.Configuration)
    .AddBusiness(builder.Configuration);

builder.Services.AddSwagger();
builder.Services.AddJwt(builder.Configuration);

var app = builder.Build();

// Add new user
using var scope = app.Services.CreateScope();
await AutomatedMigration.MigrateAsync(scope.ServiceProvider);

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();


app.UseCors("AllowReactApp");

app.UseStaticFiles();
app.UseHttpsRedirection();

// Add Middlewares Here
app.AddMiddlewares();

app.UseAuthorization();

app.MapControllers();

app.Run();
