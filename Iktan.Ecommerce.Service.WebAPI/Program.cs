using Iktan.Ecommerce.Transversal.Mapper;
using Iktan.Ecommerce.Transversal.Common;
using Iktan.Ecommerce.Infraestructure.Data;
using Iktan.Ecommerce.Infraestructure.Interface;
using Iktan.Ecommerce.Infraestructure.Repository;
using Iktan.Ecommerce.Domain.Core;
using Iktan.Ecommerce.Domain.Interface;
using Iktan.Ecommerce.App.Interface;
using Iktan.Ecommerce.App.Main;

var policyEcommerce = "_policyRouteEcommerce";

//Key Configuration
var builder = WebApplication.CreateBuilder(args);
var configCORS = builder.Configuration.GetValue<string>("Configs:OriginCors");

//CORS
builder.Services.AddCors(options => options.AddPolicy(policyEcommerce, builder => builder.WithOrigins(configCORS)
                                                                                         .AllowAnyHeader()
                                                                                         .AllowAnyMethod()));

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

app.MapControllers();

app.Run();
