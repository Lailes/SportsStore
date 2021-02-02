using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportsStore.Models;

namespace SportsStore {
    public class Startup {
        private const string Config = "Data:SportStoreProducts:ConnectionString";
        private const string ConfigIdentity = "Data:SportsStoreIdentity:ConnectionString";

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration[Config]));
            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(Configuration[ConfigIdentity]));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();
            services.AddTransient<IProductRepository, EfProductRepository>();
            services.AddTransient<IOrderRepository, EfOrderRepository>();
            services.AddScoped(SessionCart.GetCart);
            services.AddMvc(service => service.EnableEndpointRouting = false);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app
                .UseStatusCodePages()
                .UseStaticFiles()
                .UseSession()
                .UseAuthentication()
                .UseMvc(routes => {
                    routes.MapRoute(
                        null,
                        "{category}/Page{productPage:int}",
                        new {controller = "Product", action = "List"}
                    );

                    routes.MapRoute(
                        null,
                        "Page{productPage:int}",
                        new {controller = "Product", action = "List", productPage = 1}
                    );

                    routes.MapRoute(
                        null,
                        "{category}",
                        new {controller = "Product", action = "List", productPage = 1}
                    );

                    routes.MapRoute(
                        null,
                        "",
                        new {controller = "Product", action = "List", productPage = 1}
                    );

                    routes.MapRoute(null, "{controller}/{action}/{id?}");
                });

            SeedData.EnsurePopulated(app);
            IdentitySeedData.EnsurePopulated(app);
        }
    }
}