using CarDataRecognizer.Repositories.DataRepository;
using CarDataRecognizer.Services;
using CarDataRecognizer.Utils.Period;
using CarDataRecognizer.ConfigSections;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using CarDataRecognizer.Services.Dir;
using CarDataRecognizer.Services.ImageAnalyzer;

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
            services.AddHostedService<DataProcessorHostedService>();

            // Add framework services.
            services.AddDbContext<DatabaseContext>();
            // Transient lifetime services are created each time they are requested. This lifetime works best for lightweight, stateless services.
            services.AddTransient<IDataRepository, DataRepository>();
            services.AddTransient<IDirectoryService, DirectoryService>();
            services.AddTransient<IImageAnalyzerService, ImageAnalyzerService>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (IServiceScope serviceScope = app.ApplicationServices.CreateScope())
            {
                serviceScope.ServiceProvider.GetService<DatabaseContext>().Database.Migrate();
            }
        }
    }
}
