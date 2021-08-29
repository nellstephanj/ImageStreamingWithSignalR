using System;
using ImageStreamingWithSignalR.Hubs;
using ImageStreamingWithSignalR.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;

namespace ImageStreamingWithSignalR
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // To add SignalR to a Dotnet 5 project, simply add the services.AddSignalR() no need to install any Nuget packages
            // To send Binary files we add () and install Microsoft.AspNetCore.SignalR.Protocols.MessagePack Nuget package
            services.AddSignalR().AddMessagePackProtocol();
            
            services.AddControllers();
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "ImageStreamingWithSignalR", Version = "v1"});
            });
            services.AddSingleton<IImageStreamingService, ImageStreamingService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Image Streaming with Signal R");
                    c.DisplayRequestDuration();
                });
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ImageStreamHub>("/imageStreamHub");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
                spa.Options.DefaultPageStaticFileOptions = new StaticFileOptions
                {
                    OnPrepareResponse = context =>
                    {
                        if (context.File.Name == "index.html")
                        {
                            context.Context.Response.Headers.Add(HeaderNames.CacheControl, "no-cache");
                        }
                    }
                };
                
                if (!env.IsDevelopment())
                {
                    return;
                }

                if (Environment.GetEnvironmentVariables().Contains("ApiOnly"))
                {
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                }
                else
                {
                    spa.UseAngularCliServer("start");
                }
            });
        }
    }
}