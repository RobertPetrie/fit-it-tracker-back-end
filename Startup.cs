using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using fix_it_tracker_back_end.Model;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Newtonsoft.Json;
using Microsoft.OpenApi.Models;
using fix_it_tracker_back_end.Data.Repositories;
using System.Reflection;
using System.IO;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using fix_it_tracker_back_end.Helpers;

namespace fix_it_tracker_back_end
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
            string connectionString =
                Configuration["ConnectionStrings:SQLiteConnection"];

            services.AddDbContext<DataContext>(options =>
                options.UseSqlite(connectionString));

            //  Use this for SQL Server

            //string connectionString =
            //    Configuration["ConnectionStrings:SQLServerConnection"];

            //services.AddDbContext<DataContext>(options =>
            //    options.UseSqlServer(connectionString));

            services.AddControllersWithViews()
            .AddJsonOptions(opts =>
            {
                opts.JsonSerializerOptions.IgnoreNullValues = true;
            }).AddNewtonsoftJson();
            services.AddAutoMapper(typeof(DataContext).Assembly);
            services.AddControllers();
            services.AddScoped<IFixItTrackerRepository, FixItTrackerRepository>();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new OpenApiInfo { Title = "Fix IT Tracker API", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseExceptionHandler(builder =>
                {
                    builder.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        var error = context.Features.Get<IExceptionHandlerFeature>();

                        if (error != null)
                        {
                            context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });
            }

            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                    var error = context.Features.Get<IExceptionHandlerFeature>();

                    if (error != null)
                    {
                        context.Response.AddApplicationError(error.Error.Message);
                        await context.Response.WriteAsync(error.Error.Message);
                    }
                });
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Fix IT Tracker API");
            });
        }
    }
}
