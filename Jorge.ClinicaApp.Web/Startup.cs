using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jorge.ClinicaApp.Infrastructure.Model;
using Jorge.ClinicaApp.Infrastructure.UnitOfWork;
using Jorge.ClinicaApp.Model.Context;
using Jorge.ClinicaApp.Model.Repositories;
using Jorge.ClinicaApp.Repository;
using Jorge.ClinicaApp.Repository.Repositories;
using Jorge.ClinicaApp.Services;
using Jorge.ClinicaApp.Services.Implementatios;
using Jorge.ClinicaApp.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Jorge.ClinicaApp.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            AutoMapperBootStrapper.ConfigureAutoMapper();
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

            services.AddScoped(typeof(IPatientRepository), typeof(PatientRepository));
            services.AddScoped(typeof(IPatientService), typeof(PatientService));


            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped(typeof(ISecurityService), typeof(SecurityService));


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, jwtBearerOptions =>
            {
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Configuration.GetValue<string>("SecurityKey"))
                    ),
                    ValidIssuer = "elbarcotechnology.com",
                    ValidAudience = "Jorge",
                };
            });



            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            //Initialize(app.ApplicationServices);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }

        public static void Initialize(IServiceProvider service)
        {
            using (var serviceScope = service.CreateScope())
            {
                var scopeServiceProvider = serviceScope.ServiceProvider;
                var context = scopeServiceProvider.GetService<ClinicaContext>();
                context.Database.Migrate();

                DataSeeder.Seed(context);
            }
        }
    }
}
