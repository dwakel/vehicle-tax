using VehicleTax.Data;
using VehicleTax.Domain;
using VehicleTax.Services;
using VehicleTax.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Npgsql;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace VehicleTax
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

            services
               .AddOptions()
               .Configure<ConnectionStringSettings>(Configuration.GetSection("ConnectionStrings"));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "VehicleTax", Version = "v1" });
            });

            //services.AddScoped<IAuthenticationSessionProvider, AuthenticationSessionProvider>();
            services.AddSingleton<IConnectionResolver<NpgsqlConnection>, SqlConnectionResolver>();
            services.AddTransient<IVehicleRepository, VehicleRepository>();
            //services.AddScoped(typeof(IUserRepository), typeof(UserRepository));

            services.AddScoped<IVehicleTaxHandler, VehicleTaxHandler>();
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "VehicleTax v1"));
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
