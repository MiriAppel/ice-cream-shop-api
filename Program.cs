using MyWebApi.Services;
using MyWebApi.Interfaces;
using MyWebApi;
using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
        .AddAuthentication(options =>
        {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(cfg =>
        {
            cfg.RequireHttpsMetadata = false;
            cfg.TokenValidationParameters = IceTokenService.GetTokenValidationParameters();
        });

builder.Services.AddAuthorization(cfg =>
   {
       cfg.AddPolicy("Admin", policy => policy.RequireClaim("type", "Admin"));
       cfg.AddPolicy("User", policy => policy.RequireClaim("type", "User", "Admin"));
   }
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ice cream", Version = "v1" });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter JWT with Bearer into field",
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement {
            { 
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer"}
                },
                new string[] {}
            }
        });
    }
);

builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IIceCreamService, IceCreamService>();
var app = builder.Build();

// app.UseMy1stMiddleware();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMyLogMiddleware();

//  app.UseDefaultFiles();
//  app.UseStaticFiles();

app.UseDefaultFiles();

app.UseStaticFiles();

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
