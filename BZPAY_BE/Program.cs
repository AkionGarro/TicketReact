using AutoMapper;
using BZPAY_BE.BussinessLogic.auth.ServiceImplementation;
using BZPAY_BE.BussinessLogic.auth.ServiceInterface;
using BZPAY_BE.Common.Profiles;
using BZPAY_BE.Models;
using BZPAY_BE.Repositories.Implementations;
using BZPAY_BE.Repositories.Interfaces;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
var connectionString = builder.Configuration.GetConnectionString("MembershipContext");
builder.Services.AddDbContext<MembershipContext>(x => x.UseSqlServer(connectionString));

// Add services
builder.Services.AddScoped<IAspnetUserService, AspnetUserService>();

// Add repositories
builder.Services.AddScoped<IAspnetUserRepository, AspnetUserRepository>();

// Auto Mapper Configurations
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new AspnetUserProfile());
    mc.AddProfile(new AspnetMembershipProfile());
});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


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
