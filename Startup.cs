using CarDataRecognizer.Repositories.AdatRepository;
using CarDataRecognizer.Services;
using CarDataRecognizer.Utils.Period;
using Kamera.ConfigSections;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CarDataRecognizer
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
            services.AddTransient<IPeriodProvider, PeriodProvider>();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.Configure<Config>(Configuration.GetSection("Config"));

            services.AddHostedService<CleanerHostedService>();

            // Add framework services.
            services.AddDbContext<DatabaseContext>();
            // Transient lifetime services are created each time they are requested. This lifetime works best for lightweight, stateless services.
            services.AddTransient<IAdatRepository, AdatRepository>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
