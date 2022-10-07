using System.Text;
using AutoMapper;
using SpareParts.API.Helpers;
using SpareParts.Domain.Profiles;
using SpareParts.API.Services.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SpareParts.Data.DbContext;
using SpareParts.Data.Models;
using SpareParts.Domain.Repos;
using SpareParts.InfraStructure.Interfaces;

namespace SpareParts.API.Configuration;

public static class ApplicationService
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<Jwt>(config.GetSection("JWT"));
        
        services.AddIdentity<User, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        
        services.AddScoped<IAuthService, AuthService>();
        
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.RequireHttpsMetadata = false;
            o.SaveToken = false;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = config["JWT:Issuer"],
                ValidAudience = config["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]))
            };
        });

        services.AddSingleton(new MapperConfiguration(mc =>
        {
            mc.AddProfile(new ApplicationProfile());
            mc.AllowNullCollections = true;
        }).CreateMapper());

        services.AddScoped<UserRepo>();
        services.AddScoped<IDomainRepository < Product > ,ProductRepo >();
        services.AddScoped<IDomainRepository<OrderItem>, OrderItemRepo>();
        services.AddScoped<IDomainRepository<Order>, OrderRepo>();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new() {Title = "Spare Part API", Version = "v1"});
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                Scheme = "bearer",
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {jwtSecurityScheme, Array.Empty<string>()}
            });

        });


        services.AddDbContext<ApplicationDbContext>((provider, optionsBuilder) =>
            ApplicationDbDesigner.SetConfiguration<ApplicationDbContext>(provider, optionsBuilder, 
                config.GetConnectionString("ApplicationConnection")));
       
        return services;
    }
}