using Business;
using DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Models;

namespace TechnicalTest
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
            services.AddOptions();
            services.Configure<AppSettingModels>(Configuration.GetSection("DBconnection"));
            services.AddScoped(typeof(IPatientBLL), typeof(Business.Patient));
            services.AddScoped(typeof(IErrorBLL), typeof(Business.ErrorRecord));
            services.AddScoped(typeof(IPatientDAL), typeof(DAL.Patient));
            services.AddScoped(typeof(IErrorDAL), typeof(DAL.ErrorRecord));
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
