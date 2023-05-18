using AutoMapper;
using BZPAY_BE.BussinessLogic.auth.ServiceImplementation;
using BZPAY_BE.BussinessLogic.auth.ServiceInterface;
using BZPAY_BE.BussinessLogic.Services.Implementations;
using BZPAY_BE.BussinessLogic.Services.Interfaces;
using BZPAY_BE.Models;
using BZPAY_BE.Repositories.Implementations;
using BZPAY_BE.Repositories.Interfaces;
using BZPAY_UI.Repositories.Implementations;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<project_ticketContext>(x => 
x.UseMySql(connectionString, Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.27-mariadb")));

// Add services
builder.Services.AddScoped<IAspnetUserService, AspnetUserService>();
builder.Services.AddScoped<ITipoEventoService, TipoEventoService>();
builder.Services.AddScoped<IEventoService, EventoService>();
builder.Services.AddScoped<IEscenarioService, EscenarioService>();
builder.Services.AddScoped<IEntradaService, EntradaService>();

// Add repositories
builder.Services.AddScoped<IAspnetUserRepository, AspnetUserRepository>();
builder.Services.AddScoped<ITipoEventoRepository, TipoEventoRepository>();
builder.Services.AddScoped<IEventoRepository, EventoRepository>();
builder.Services.AddScoped<IEscenarioRepository, EscenarioRepository>();
builder.Services.AddScoped<IEntradaRepository, EntradaRepository>();

// Add services to the container.
builder.Services.AddControllers();

//Add Localization Service
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
            .AddDataAnnotationsLocalization();
builder.Services.AddControllersWithViews();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
                    {
                        options.AddPolicy(name: "AllowAllHeaders",
                            builder =>
                            {
                                builder.AllowAnyHeader()
                                       .AllowAnyMethod()
                                       .WithOrigins("https://localhost:3000");
                            });                                                  
                    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllHeaders");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var supportedCultures = new[] { "es", "en" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

localizationOptions.RequestCultureProviders.Clear();
localizationOptions.RequestCultureProviders.Add(new QueryStringRequestCultureProvider() { QueryStringKey = "lang" });

app.UseRequestLocalization(localizationOptions);

app.Run();
