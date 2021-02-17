using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotnet_core_api.Config;
using dotnet_core_api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace dotnet_core_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            var key = "pamys2021xD$123/rpi@192.168.1.200@zapateria-pamys.ml";
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddSingleton<IJwtAuthManager>(new JwtAuthManager(key));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "dotnet_core_api", Version = "v1" });
            });
            services.AddDbContextPool<DB_PAMYSContext>(
            dbContextOptions => dbContextOptions
            .UseMySql(
            // Replace with your connection string.
            Configuration.GetConnectionString("DB_PAMYS"),
            // Replace with your server version and type.
            // For common usages, see pull request #1233.
            new MySqlServerVersion(new Version(8, 0, 21)), // use MariaDbServerVersion for MariaDB
            mySqlOptions => mySqlOptions
            .CharSetBehavior(CharSetBehavior.NeverAppend))
            // Everything from this point on is optional but helps with debugging.
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors()
            );
            services.AddCors(options =>
            {
                options.AddPolicy("default",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200");
                        builder.AllowAnyMethod();
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "dotnet_core_api v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("default");
            app.UseEndpoints(endpoints =>
            { endpoints.MapControllers(); });
        }
    }
}
