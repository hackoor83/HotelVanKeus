using HotelVanKeus.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HotelVanKeus
{
    public class Startup
    {

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
       
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //Other services to be added:

            services.AddDbContext<HotelVanKeusContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("Default"));
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //Enable routing
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                //To add the mapcontrollerroute:
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Room}/{action=List}/{id?}"
                    );
            });
        }
    }
}
