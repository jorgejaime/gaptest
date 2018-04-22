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
using Jorge.ClinicaApp.Web.Services.Security;
using Microsoft.AspNetCore.Identity;
using Jorge.ClinicaApp.Web.Services.Web.Security;
using Microsoft.AspNetCore.Http;

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

            services.AddScoped(typeof(IPatientRepository), typeof(PatientRepository));
            services.AddScoped(typeof(IPatientService), typeof(PatientService));

            services.AddIdentity<ApplicationUser, ApplicationRole>().AddDefaultTokenProviders();
            services.AddTransient<IUserStore<ApplicationUser>, ApplicationUserStore>();
            services.AddTransient<IUserRoleStore<ApplicationUser>, ApplicationUserStore>();
            services.AddTransient<IUserPasswordStore<ApplicationUser>, ApplicationUserStore>();
            services.AddTransient<IUserSecurityStampStore<ApplicationUser>, ApplicationUserStore>();
            services.AddTransient<IRoleStore<ApplicationRole>, ApplicationRoleStore>();


            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequiredLength = 8;
               
                // User settings
                options.User.RequireUniqueEmail = true;

            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                //options.Cookie.HttpOnly = true;             
                options.Cookie = new CookieBuilder
                {
                    HttpOnly = true,
                    Name = ".Jorge.Security.Cookie",
                    Path = "/",
                    SameSite = SameSiteMode.Lax,
                    SecurePolicy = CookieSecurePolicy.SameAsRequest,
                    Expiration = TimeSpan.FromDays(150),

                };
                //options.SlidingExpiration = true;
                options.LoginPath = "/Home/Login"; // If the LoginPath is not set here, ASP.NET Core will default to /Home/Login
                options.LogoutPath = "/Home/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to /Home/Logout
                options.AccessDeniedPath = "/Home/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Home/AccessDenied                
                options.ExpireTimeSpan = TimeSpan.FromDays(150);
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

                DataSeeder.Seed(context);
            }
        }
    }
}
