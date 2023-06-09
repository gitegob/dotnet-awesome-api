global using System;
global using System.Text;
global using Dotnet_API.Data;
global using Dotnet_API.Utils;
using System.Security.Claims;
using System.Text.Json.Serialization;
using Dotnet_API.Extensions;
using Dotnet_API.Middleware;
using Dotnet_API.Services;
using Dotnet_API.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerUI;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<AppSettings>(builder.Configuration);
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Authorization header with bearer scheme",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
    options.OrderActionsBy((apiDesc) => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}");
});

// Register Authentication middleware
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = async context =>
            {
                var email = context.Principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
                var userService = context.HttpContext.RequestServices.GetRequiredService<UserService>();
                var existingUser = await userService.GetByEmail(email);
                if (existingUser == null) context.Fail("Invalid token");
            }
        };
    });

builder.Services.AddHttpContextAccessor();

// Register all service classes with the [ScopedService], [TransientService] or [SingletonService] attributes
builder.Services.RegisterServices(builder.Configuration);

var app = builder.Build();

// Initialize the Database
DatabaseInitializer.Initialize(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DocumentTitle = "Dotnet API";
        options.EnablePersistAuthorization();
        options.DocExpansion(DocExpansion.None);
    });
}

app.UseAuthentication();
app.UseAuthorization();
app.RegisterMiddleware(); // Register all custom middleware with the [Middleware] attribute

app.MapControllers();

app.Run();