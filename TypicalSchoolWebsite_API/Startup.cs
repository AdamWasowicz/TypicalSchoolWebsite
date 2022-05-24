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


        public void AddJwtSupport(IServiceCollection services)
        {
            var authSettings = new AuthenticationSettings();

            authSettings.JwtKey = Environment.GetEnvironmentVariable("JwtKey") != null
                ? Environment.GetEnvironmentVariable("JwtKey")
                : Configuration.GetValue<string>("Authentication:Default:JwtKey");

            authSettings.JwtExpireTimeHours = Environment.GetEnvironmentVariable("JwtExpireTimeHours") != null
                ? Environment.GetEnvironmentVariable("JwtExpireTimeHours")
                : Configuration.GetValue<string>("Authentication:Default:JwtExpireTimeHours");

            authSettings.JwtIssuer = Environment.GetEnvironmentVariable("JwtIssuer") != null
                ? Environment.GetEnvironmentVariable("JwtIssuer")
                : Configuration.GetValue<string>("Authentication:Default:JwtIssuer");

            services.AddSingleton(authSettings);


            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = "Bearer";
                option.DefaultScheme = "Bearer";
                option.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authSettings.JwtIssuer,
                    ValidAudience = authSettings.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.JwtKey)),
                };
            });
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Database
            ConfigureDataBaseConections(services);

            //JwtAuthentication
            AddJwtSupport(services);

            //Middleware
            services.AddScoped<ErrorHandlingMiddleware>();




            //Automapper
            services.AddAutoMapper(this.GetType().Assembly);

            //Validators
            services.AddFluentValidation();
            services.AddScoped<IValidator<RegisterUserDTO>, RegiserUserDTO_Validator>();

            //Services
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();

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


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Seeder seeder)
        {
            //Use Seeder
            seeder.SeedRoles();

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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
