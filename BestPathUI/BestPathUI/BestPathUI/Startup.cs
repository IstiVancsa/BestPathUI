using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BestPathUI.Data;
using System;
using Services;
using Interfaces;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using BestPathUI.services;
using System.Net.Http;
using Microsoft.AspNetCore.Components;

namespace BestPathUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddRazorPages();
            services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });
            services.AddSingleton<WeatherForecastService>();

            services.AddBlazoredLocalStorage();
            services.AddAuthorizationCore();
            
            services.AddScoped<ApiAuthenticationStateProvider>();
            services.AddScoped<IAuthService, AuthService>();


            services.AddHttpClient<IAuthService, AuthService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44344/");
            });
            services.AddHttpClient<AuthenticationStateProvider,ApiAuthenticationStateProvider>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44344/");
            }); 
            services.AddHttpClient<ICitiesDataService, CitiesDataService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44344/");
            });
            services.AddHttpClient<IReviewDataService, ReviewDataService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44344/");
            });
            services.AddHttpClient<IGoogleDataService, GoogleDataService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44344/");
            });

            services.AddScoped<HttpClient>(client =>
            {
                var uriHelper = client.GetRequiredService<NavigationManager>();
                return new HttpClient
                {
                    BaseAddress = new Uri("https://localhost:44344/")
                };
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
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

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
