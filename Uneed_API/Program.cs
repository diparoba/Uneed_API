using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Uneed_API.FirstData;
using Uneed_API.Models;
using Uneed_API.Services;
using Uneed_API.Utilities;
using IServiceProvider = Uneed_API.Services.IServiceProvider;
using ServiceProvider = Uneed_API.Services.ServiceProvider;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(options =>
                                            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configuraci�n de Token
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWT:Dominio"],
            ValidAudience = builder.Configuration["JWT:appApi"],
            LifetimeValidator = TokenLifetimeValidator.Validate,
            IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:Password"])
            )

        };
    });
//Servicios Activos
builder.Services.AddScoped<IServiceUser, ServiceUser>();
builder.Services.AddScoped<IServiceLogin, ServiceLogin>();
builder.Services.AddScoped<IServiceCategory, ServiceCategory>();
builder.Services.AddScoped<IServiceProvider, ServiceProvider>();
builder.Services.AddScoped<IServiceAddress, ServiceAddress>();
builder.Services.AddScoped<IServiceContrat, ServiceContrat>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("PolicyLocal",
        policy =>
        {
            policy.WithOrigins("http://localhost:5001",
                                "http://uneed.com").AllowAnyHeader().AllowAnyMethod();
        });

    options.AddPolicy("PauloVi",
        policy =>
        {
            policy.WithOrigins("http://localhost:8081")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
    options.AddPolicy("AnotherPolicy",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
    options.AddPolicy("All",
        policy =>
        {
            policy.WithOrigins("*")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });

});




var app = builder.Build();

//enable cors
app.UseCors();

// Create Database if not exist

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<DataContext>();
        DbInitial.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error ocurred creating DB.");
    }


}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
