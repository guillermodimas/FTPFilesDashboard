using Blazorise;
using FTPFilesDashboard.DataAccess.Internal;
using FTPFilesDashboard.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using FTPFilesDashboard.Services;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace FTPFilesDashboard
{
    public class Startup
    {
        private IWebHostEnvironment _hostingEnvironment;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            _hostingEnvironment = hostingEnvironment;

            var builder = new ConfigurationBuilder()
              .SetBasePath(hostingEnvironment.ContentRootPath)
              .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
              .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true)
              .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            //Blazorise Services (UI)
            services
              .AddBlazorise(options =>
              {
                  options.ChangeTextOnKeyPress = false; // optional
              })
              .AddBootstrapProviders()
              .AddFontAwesomeIcons();

            services.AddServerSideBlazor();

            

            services.AddScoped<AzureStorageHelper>();

            //SQL Service
            services.AddTransient<ISQLDataAccess, SQLDataAccess>();

            //Data Access Service
            services.AddTransient<IRecordingsData, RecordingsData>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();



            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "/")),
                RequestPath = "/Files"
            });

            


            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

  
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
