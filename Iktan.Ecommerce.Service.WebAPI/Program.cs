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
using Iktan.Ecommerce.Service.WebAPI.Modules.Injection;
using Iktan.Ecommerce.Service.WebAPI.Modules.Authentication;
using Iktan.Ecommerce.Service.WebAPI.Modules.Swagger;
using Iktan.Ecommerce.Service.WebAPI.Modules.Feature;
using Iktan.Ecommerce.Service.WebAPI.Modules.Validator;
using Iktan.Ecommerce.Transversal.Logging;

using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

//Key Configuration
var builder = WebApplication.CreateBuilder(args);

//Get Values and set AppSettings
var configs = builder.Configuration.GetSection("Configs");
builder.Services.Configure<AppSettings>(configs);

//CORS Extensions
var policyEcommerce = "_policyRouteEcommerce";
builder.Services.AddFeature(builder.Configuration, policyEcommerce);

// Add services to the container.
builder.Services.AddControllers();

//Extension Validatiors
builder.Services.AddValidator();

//Extensions AddAuthentication
builder.Services.AddAuthentication(builder.Configuration);

//Enable to Authorize Controllers
builder.Services.AddAuthorization();

// Automapping
builder.Services.AddAutoMapper(typeof(MappingsProfile));

//Extensions Dependency Injection
builder.Services.AddInjection(builder.Configuration);

//Swagger Extensions
builder.Services.AddSwagger();

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
