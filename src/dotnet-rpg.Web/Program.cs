using dotnet_rpg.Application.Common;
using dotnet_rpg.Application.Interfaces.Repositories;
using dotnet_rpg.Application.Mappings;
using dotnet_rpg.Application.Services;
using dotnet_rpg.Infrastructure.Persistence;
using dotnet_rpg.Infrastructure.Persistence.Repositories;
using dotnet_rpg.Infrastructure.Services;
using dotnet_rpg.Web.Middleware.ExceptionHandler;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMvc();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<IAssemblyMarker>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(cfg =>
{
    cfg.SwaggerDoc("v1", new OpenApiInfo{Title = "dotnet-rpg API", Version = "v1"});
    var xmlFile = $"{typeof(Program).Assembly.GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    cfg.IncludeXmlComments(xmlPath);
});

builder.Services.AddHttpLogging(httpLogging =>
{
    httpLogging.LoggingFields = HttpLoggingFields.All;
});

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite(builder.Configuration["DefaultConnection"]);
});

using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }
}

builder.Services.AddScoped(typeof(ICharacterRepository), typeof(CharacterRepository));
builder.Services.AddScoped<ICharacterService, CharacterService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "dotnet-rpg API v1");
    });
}

app.UseExceptionHandlerExtension();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();