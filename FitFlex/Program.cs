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
using FitFlex.Middleware;
using Microsoft.OpenApi.Models;

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
// ✅ Swagger with JWT support
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
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

builder.Services.AddHttpContextAccessor();



builder.Services.AddScoped(typeof(IRepository<>), typeof(repository<>));
builder.Services.AddScoped<IAuth, AuthService>();
builder.Services.AddScoped<ITrainerservice, TrainerService>();
builder.Services.AddScoped<ISubscription, SubscriptionService>();


// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<UserIdMiddleware>();

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
