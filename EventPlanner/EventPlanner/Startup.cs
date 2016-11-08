using System;
using AutoMapper;
using EventPlanner.Configuration;
using EventPlanner.Repositories;
using EventPlanner.Services.Configuration;
using EventPlanner.Services.Event;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace EventPlanner
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        { 
            // Configure model mappings
            var mapper = new MapperConfiguration(cfg =>
            {
                ViewModelsMapperConfiguration.InitialializeMappings(cfg);
                ServicesMapperConfiguration.InitialializeMappings(cfg);

            }).CreateMapper();

            // Do not allow application to start with broken configuration. Fail fast.
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
            services.AddSingleton(mapper);

            // Singleton - There will be at most one instance of the registered service type and the container will hold on to that instance until the container is disposed or goes out of scope. Clients will always receive that same instance from the container.
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
              .AddSingleton<IActionContextAccessor, ActionContextAccessor>(); ;
            
            // Scoped - For every request within an implicitly or explicitly defined scope.
            services
                .Configure<ConnectionOptions>(options => options.ConnectionString = Configuration.GetConnectionString("EventPlannerConnection"))
                .AddScoped<IEventService, EventService>();

            // Transient - A new instance of the service type will be created each time the service is requested from the container. If multiple consumers depend on the service within the same graph, each consumer will get its own new instance of the given service.
            services
                .AddTransient<IEventsRepository, EventsRepository>();
            
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);
            
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseApplicationInsightsExceptionTelemetry();
            app.UseStaticFiles();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
