using applicationApi.Formatters;
using applicationApi.Models;
using applicationApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace applicationApi
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
            // Adding service responsible for consuming messages that are posted in used message queue
            // TODO: uncomment below line when API will be done
            // services.AddHostedService<ConsumeRabbitMQHostedService>();
            
            // TODO: change "ConnectionString" value in "appsettings.json" to connect to local MongoDB available on deployment cluster
            // Acquiring configuration data defined in "appsettings.json"
            services.Configure<SensorsDatabaseSettings>(
                Configuration.GetSection(nameof(SensorsDatabaseSettings)));
            // Defining Singleton containing configuration data acquired above
            services.AddSingleton<ISensorsDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<SensorsDatabaseSettings>>().Value);

            // Defining Singletons that are services sharing CRUD methods for each entity class used
            services.AddSingleton<HumiditySensorService>();
            services.AddSingleton<PressureSensorService>();
            services.AddSingleton<TemperatureSensorService>();
            services.AddSingleton<WindSensorService>();

            services.AddControllers(options =>
            {
                options.OutputFormatters.Add(new CsvOutputFormatter());
            });
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "applicationApi", Version = "v1" });
            });
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "applicationApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
