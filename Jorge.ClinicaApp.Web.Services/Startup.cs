using Jorge.ClinicaApp.Infrastructure.Model;
using Jorge.ClinicaApp.Infrastructure.UnitOfWork;
using Jorge.ClinicaApp.Model.DomainModels;
using Jorge.ClinicaApp.Repository;
using Jorge.ClinicaApp.Model.Repositories;
using Jorge.ClinicaApp.Repository.Repositories;
using Jorge.ClinicaApp.Services;
using Jorge.ClinicaApp.Services.Implementatios;
using Jorge.ClinicaApp.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Design;
using Jorge.ClinicaApp.Model.Context;
using System;

namespace Jorge.ClinicaApp.Web.Services
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            AutoMapperBootStrapper.ConfigureAutoMapper();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddDbContext<ClinicaContext>(options => options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            services.AddScoped(typeof(IAppointmentRepository), typeof(AppointmentRepository));
            services.AddScoped(typeof(IAppointmentService), typeof(AppointmentService));

            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            Initialize(app.ApplicationServices);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();


        }

        public static void Initialize(IServiceProvider service)
        {
            using (var serviceScope = service.CreateScope())
            {
                var scopeServiceProvider = serviceScope.ServiceProvider;
                var context = scopeServiceProvider.GetService<ClinicaContext>();
                context.Database.Migrate();

                DataSeeder.SeedCountries(context);
            }
        }
    }
}
