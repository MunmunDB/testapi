using Core.API;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api
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
            services.AddTransient<IExternalService, ExternalService>();
            services.AddTransient<IFunds, Funds>();            
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseMvc(routes =>
            {
                // 1. Map the root URL (“”) to Funds/get-funds with default id=aaa
                routes.MapRoute(
                    name: "s",
                    template: "",
                    defaults: new { controller = "Funds", action = "get-funds"}
                );

                // 2. Your normal API routes
                routes.MapRoute(
                    name: "api",
                    template: "api/{controller}/{action}/{id?}"
                );
            });
        }
    }
}
