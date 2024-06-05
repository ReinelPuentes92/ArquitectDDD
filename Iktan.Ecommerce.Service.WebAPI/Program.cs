using Iktan.Ecommerce.Transversal.Mapper;
using Iktan.Ecommerce.Transversal.Common;
using Iktan.Ecommerce.Infraestructure.Data;
using Iktan.Ecommerce.Infraestructure.Interface;
using Iktan.Ecommerce.Infraestructure.Repository;
using Iktan.Ecommerce.Domain.Core;
using Iktan.Ecommerce.Domain.Interface;
using Iktan.Ecommerce.App.Interface;
using Iktan.Ecommerce.App.Main;
using Iktan.Ecommerce.Service.WebAPI.Helpers;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using Microsoft.OpenApi.Models;

var policyEcommerce = "_policyRouteEcommerce";

//Key Configuration
var builder = WebApplication.CreateBuilder(args);
var configCORS = builder.Configuration.GetValue<string>("Configs:OriginCors");

//CORS
builder.Services.AddCors(options => options.AddPolicy(policyEcommerce, builder => builder.WithOrigins(configCORS)
                                                                                         .AllowAnyHeader()
                                                                                         .AllowAnyMethod()));

//Get Values AppSettings
var configs = builder.Configuration.GetSection("Configs");
builder.Services.Configure<AppSettings>(configs);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Ecommerce Iktan", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Authorization Bearer Sheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Configs:Issuer"],
            ValidAudience = builder.Configuration["Configs:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Configs:Secret"])),
            ClockSkew = TimeSpan.Zero
        };
    });

//Enable to Authorize Controllers
builder.Services.AddAuthorization();

// Automapping
builder.Services.AddAutoMapper(typeof(MappingsProfile));

//dependency injection
//AddSingleton => Se instancia una sola vez y se reutiliza en los demas llamados
//builder.Services.AddSingleton<IConfiguration>(Configuration);
builder.Services.AddSingleton<IConnectionFactory, ConnectionFactory>();

//AddScoped => Se instancia una sola vez por solicitud
builder.Services.AddScoped<ICustomerApplication, CustomerApplication>();
builder.Services.AddScoped<ICustomerDomain, CustomerDomain>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IUserApplication, UserApplication>();
builder.Services.AddScoped<IUserDomain, UserDomain>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseCors(policyEcommerce);
app.MapControllers();
app.Run();
