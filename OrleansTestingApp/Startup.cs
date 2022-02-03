
using Microsoft.OpenApi.Models;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans;
namespace OrleansTestingApp
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "templateproject", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "templateproject v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


        public static Action<ISiloBuilder> ConfigureOrleans(IConfiguration config)
        {
            return  x =>
            {
                x.Configure((Action<ClusterOptions>)(o =>
                {
                    o.ClusterId = "dev";
                    o.ServiceId = "dev";
                }));
                x.UseAdoNetClustering(options =>
                {
                    options.Invariant = "Npgsql";
                    options.ConnectionString = config.GetConnectionString("PostgresqlConnectionString");
                });
                x.AddAdoNetGrainStorage("teststorage", options =>
                {
                    options.Invariant = "Npgsql";
                    options.ConnectionString = config.GetConnectionString("PostgresqlConnectionString");
                    options.UseJsonFormat = true;
                });
                x.ConfigureApplicationParts(options =>
                {
                    options.AddApplicationPart(typeof(HelloGrain).Assembly).WithReferences();
                });
                x.ConfigureEndpoints
                (
                    siloPort: 11111,
                    gatewayPort: 30000,
                    listenOnAnyHostAddress: true
                );
            };
        }
    }
}
