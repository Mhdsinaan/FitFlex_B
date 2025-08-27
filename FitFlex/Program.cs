using FitFlex.Application.Interfaces;
using FitFlex.Infrastructure.Db_context;
using FitFlex.Infrastructure.Repository_service;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FitFlex.Application.DTO_s.Dto_validation;
using FluentValidation;
using FluentValidation.AspNetCore;
using FitFlex.Application.DTOs.Dto_validation;
using FitFlex.Application.DTO_s.Trainers_dto;
using FitFlex.Infrastructure.Interfaces;
using FitFlex.Application.services;

var builder = WebApplication.CreateBuilder(args);

var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

// Add Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddAuthorization();
builder.Services.AddControllers();

// Add DB context
builder.Services.AddDbContext<MyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("connection"))
);

//validation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterdtoValidation>();
builder.Services.AddValidatorsFromAssemblyContaining<TrainersValidation>();
builder.Services.AddValidatorsFromAssemblyContaining<TrainerRegisterDtoValidation>();
builder.Services.AddValidatorsFromAssemblyContaining<TrainerLoginDtoValidation>();




builder.Services.AddScoped(typeof(IRepository<>), typeof(repository<>));
builder.Services.AddScoped<IAuth, AuthService>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ✅ Authentication must come before Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
