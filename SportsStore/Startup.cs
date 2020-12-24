using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportsStore.Models;

namespace SportsStore {
    public class Startup {
        private const string Config = "Data:SportStoreProducts:ConnectionString";

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }
        
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration[Config])
            );

            services.AddTransient<IProductRepository, EfProductRepository>();
            services.AddMvc(service => service.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseStatusCodePages();
            app.UseStaticFiles();
            
            app.UseMvc(routes => {
                routes.MapRoute(
                    name: null,
                    template: "{category}/Page{productPage:int}",
                    defaults: new {controller = "Product", action = "List"}
                );

                routes.MapRoute(
                    name: null,
                    template: "Page{productPage:int}",
                    defaults: new {controller = "Product", action = "List", productPage = 1}
                );
                
                routes.MapRoute(
                    name: null,
                    template: "{category}",
                    defaults: new {controller = "Product", action = "List", productPage = 1}
                );

                routes.MapRoute(
                    name: null,
                    template: "",
                    defaults: new {controller = "Product", action = "List", productPage = 1}
                );

                routes.MapRoute(name: null, template: "{controller}/{action}/{id?}");
            });
            
            SeedData.EnsurePopulated(app);
        }
    }
}