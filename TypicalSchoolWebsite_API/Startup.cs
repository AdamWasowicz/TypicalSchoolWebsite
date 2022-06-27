using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypicalSchoolWebsite_API.Entities;
using TypicalSchoolWebsite_API.Interfaces;
using TypicalSchoolWebsite_API.Models.Account;
using TypicalSchoolWebsite_API.Other;
using TypicalSchoolWebsite_API.Services;
using TypicalSchoolWebsite_API.Validation.Account;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TypicalSchoolWebsite_API.Middleware;
using TypicalSchoolWebsite_API.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;
using TypicalSchoolWebsite_API.Authorization.Handlers;
using TypicalSchoolWebsite_API.Validation.Post;
using TypicalSchoolWebsite_API.Models.Post;

namespace TypicalSchoolWebsite_API
{
    public class Startup
    {
        private readonly IWebHostEnvironment environment;


        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            this.environment = environment;
        }


        public IConfiguration Configuration { get; }


        public void ConfigureDataBaseConections(IServiceCollection services)
        {
            //Release
            var localDbConectionString = Configuration["DB_CONNECTION_STRING"];
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? localDbConectionString;

            services.AddDbContext<TSW_DbContext>(options =>
                options.UseNpgsql(connectionString)
            );

            //Dev
            //TODO
        }


        public void AddJwtAuthentication(IServiceCollection services)
        {
            var authenticationSettings = new AuthenticationSettings();
            Configuration.GetSection("Authentication:Default").Bind(authenticationSettings);


            authenticationSettings.JwtKey = Environment.GetEnvironmentVariable("JwtKey") != "" && Environment.GetEnvironmentVariable("JwtKey") != null
                ? Environment.GetEnvironmentVariable("JwtKey")
                : Configuration.GetValue<string>("Authentication:Default:JwtKey");

            authenticationSettings.JwtExpireTimeHours = Environment.GetEnvironmentVariable("JwtExpireTimeHours") != "" && Environment.GetEnvironmentVariable("JwtExpireTimeHours") != null
                ? Environment.GetEnvironmentVariable("JwtExpireTimeHours")
                : Configuration.GetValue<string>("Authentication:Default:JwtExpireTimeHours");

            authenticationSettings.JwtIssuer = Environment.GetEnvironmentVariable("JwtIssuer") != "" && Environment.GetEnvironmentVariable("JwtIssuer") != null
                ? Environment.GetEnvironmentVariable("JwtIssuer")
                : Configuration.GetValue<string>("Authentication:Default:JwtIssuer");


            services.AddSingleton(authenticationSettings);


            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = authenticationSettings.JwtIssuer,
                    ValidAudience = authenticationSettings.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
                };
            });
        }


        public void AddAuthorizationPolicies(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                //HasAccessLevelAtLeast
                options.AddPolicy("IsWriter", builder => builder.AddRequirements(new HasAccessLevelAtLeastRequirement(4)));
                options.AddPolicy("IsModerator", builder => builder.AddRequirements(new HasAccessLevelAtLeastRequirement(8)));
                options.AddPolicy("IsAdmin", builder => builder.AddRequirements(new HasAccessLevelAtLeastRequirement(12)));
            });
        }


        public void ConfigureServices(IServiceCollection services)
        {
            //Use Database
            ConfigureDataBaseConections(services);

            //Use JWT Authentication
            AddJwtAuthentication(services);

            //Use Authorization
            AddAuthorizationPolicies(services);

            //Use HTTP Client
            services.AddHttpClient();

            //AuthorizationHandlers
            services.AddScoped<IAuthorizationHandler, HasAccessLevelAtLeastRequirementHandler>();

            //Middleware
            services.AddScoped<ErrorHandlingMiddleware>();

            //Automapper
            services.AddAutoMapper(this.GetType().Assembly);

            //Validators
            services.AddFluentValidation();
            // --> Account
            services.AddScoped<IValidator<RegisterUserDTO>, RegiserUserDTO_Validator>();
            // --> Post
            services.AddScoped<IValidator<EditPostDTO>, EditPostDTO_Validator>();
            services.AddScoped<IValidator<CreatePostDTO>, CreatePostDTO_Validator>();

            //Services
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPostService, PostService>();

            //Other
            services.AddScoped<Seeder>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            //Controllers
            services.AddControllers();

            //Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TypicalSchoolWebsite_API", Version = "v1" });
            });
        }


        public void Configure(
            IApplicationBuilder app, 
            IWebHostEnvironment env, 
            Seeder seeder,
            TSW_DbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TypicalSchoolWebsite_API v1"));
            }

            //Use Middleware
            app.UseMiddleware<ErrorHandlingMiddleware>();

            //Use Jwt Tokens
            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseRouting();

            //Use Authorization
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //Use Seeder
            seeder.Seed();
        }
    }
}
