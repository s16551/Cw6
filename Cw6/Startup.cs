using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cw6.Middleware;
using Cw6.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Cw6
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
            services.AddScoped<IStudentDbService, SqlServerStudentDbService>();
            services.AddControllers();
            // Dodawanie dokumentacji
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo { Title = "Students App API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IStudentDbService service)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(config => 
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "Students App API"); 
            });

            //Middlwere - Index
            app.UseMiddleware<LoggingMiddleware>();
            app.UseWhen(context => context.Request.Path.ToString().Contains("secret"), app =>
           {
               app.Use(async (context, next) =>
               {
                   if (!context.Request.Headers.ContainsKey("Index"))
                   {
                       context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                       await context.Response.WriteAsync("Musisz podaæ numer indeksu");
                       return;
                   }

                   string index = context.Request.Headers["Index"].ToString();
                   var stud = service.GetStudent(index);
                   if (stud == null)
                   {
                       context.Response.StatusCode = StatusCodes.Status404NotFound;
                       await context.Response.WriteAsync("Student not found");
                       return;
                   }

                   await next();
               });
           });
            
            app.UseHttpsRedirection();

            app.UseMiddleware<LoggingMiddleware>();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
