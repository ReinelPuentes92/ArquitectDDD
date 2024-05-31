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

//Jwt Authenticate Endpoints
var appSettings = configs.Get<AppSettings>();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

//Authenticate Endpoints
var key = Encoding.ASCII.GetBytes(appSettings.Secret);
var issuer = appSettings.Issuer;
var audience = appSettings.Audience;

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

})
    .AddJwtBearer(x =>
    {
        x.Events = new JwtBearerEvents
        {
            OnTokenValidated = context =>
            {
                var userId = int.Parse(context.Principal.Identity.Name);
                return Task.CompletedTask;
            },

            OnAuthenticationFailed = context =>
            {
                if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                {
                    context.Response.Headers.Add("Token-Expired", "true");
                }
                return Task.CompletedTask;
            },
        };
        x.RequireHttpsMetadata = true;
        x.SaveToken = false;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = true,
            ValidAudience = audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

//Documentation Swagger
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "Iktan Services S.A.S",
        Description = "A Eccomerce Services",
        //TermsOfService = "None",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Reinel Puentes",
            Email = "reinel.puentes@iktanservices.com",
        },
        License = new Microsoft.OpenApi.Models.OpenApiLicense
        {
            Name = "Use under LICX",
            //Url = "https://example.com/license",
        }

    });

    //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    //s.IncludeXmlComments(xmlPath);

    s.AddSecurityDefinition("Authorization", new OpenApiSecurityScheme
    {
        Description = "Authorization by API Key",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"        
    });

    s.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
    
app.UseAuthorization();

app.UseCors(policyEcommerce);
app.UseAuthentication();

app.MapControllers();

app.Run();
