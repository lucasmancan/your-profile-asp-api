﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aspApi.Models;
using aspApi.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
namespace aspApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        protected  void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                 .UseLazyLoadingProxies();



            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseLoggerFactory(new LoggerFactory().AddConsole((category, level) =>
                 level == LogLevel.Information &&
                    category == DbLoggerCategory.Database.Command.Name, true));
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<UserDbContext>(options =>
            options.UseLazyLoadingProxies()
           .UseSqlServer(Configuration.GetConnectionString("AzureConnection")));
         

    

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var key = Encoding.ASCII.GetBytes(Configuration["SecurityKey"]);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            //services.AddTransient<IUserRepository, UserRepository>();
            //services.AddTransient<IPhoneRepository, PhoneRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPhoneRepository, PhoneRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }


            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
