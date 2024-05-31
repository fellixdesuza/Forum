using Forum.Contracts;
using Forum.Data;
using Forum.Models.Identity;
using Forum.Repositories;
using Forum.Services.Implements;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Forum.API
{
    public static class MiddlewareExtensions
    {
        public static void AddDatabaseContext(this WebApplicationBuilder builder) => builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer")));

        public static void ConfigureJwtOptions(this WebApplicationBuilder builder) => builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));
        public static void AddIdentity(this WebApplicationBuilder builder) => builder.Services
            .AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 5;

                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();


        public static WebApplicationBuilder AddAuthentication(this WebApplicationBuilder builder)
        {
            JwtOptions jwtOptions = new();

            var secret = builder.Configuration.GetValue<string>("ApiSettings:JwtOptions:Secret");
            var issuer = builder.Configuration.GetValue<string>("ApiSettings:JwtOptions:Issuer");
            var audience = builder.Configuration.GetValue<string>("ApiSettings:JwtOptions:Audience");
            var key = Encoding.ASCII.GetBytes(secret);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true, // Check for token expiration
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidIssuer = issuer,
                    ValidAudience = audience
                };
            });

            return builder;
        }
        public static void AddHttpContextAccessor(this WebApplicationBuilder builder) => builder.Services.AddHttpContextAccessor();
        public static void AddScopedServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IPostRepository, PostRepository>();
            builder.Services.AddScoped<ICommentRepository, CommentRepository>();
            builder.Services.AddScoped<IPostService, PostService>();
            builder.Services.AddScoped<ICommentService, CommentService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        }
        public static void AddControllers(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
        }
        public static void AddEndpointsApiExplorer(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
        }

        public static void AddSwaggerGen(this WebApplicationBuilder builder)
        {
            //builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter the Bearer Authorization string example: `Bearer Generated-JWT-Token`",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });

                options.AddSecurityRequirement(
                new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme,Id = JwtBearerDefaults.AuthenticationScheme}
                        },
                        new string [] {}
                    }
                });

            });
        }
        
    }
}
