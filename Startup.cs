using System.IO;
using Microsoft.AspNetCore.DataProtection;
using System.Text;
using AutoMapper;
using ProyectoSalud.API.Data;
using ProyectoSalud.API.Helpers;
using ProyectoSalud.API.Repository;
using ProyectoSalud.API.Repository.Interfaces;
using ProyectoSalud.API.Security;
using ProyectoSalud.API.Services;
using ProyectoSalud.API.Smtp;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using DinkToPdf.Contracts;
using DinkToPdf;

namespace ProyectoSalud.API
{
    public class Startup
    {

        LoggerFactory loggerFactory = new LoggerFactory();
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public ILoggerFactory _loggerFactory;
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MainRepository).Assembly);
            services.AddControllers().AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            LoadRepositories(services);
            // services.AddScoped<LogUserActivity>();
            services.AddCors(options =>
            {
                options.AddPolicy(
                    name: MyAllowSpecificOrigins,
                    builder =>
                    {
                        // builder.SetIsOriginAllowed(origin => true);
                        builder.WithOrigins("http://localhost:4200", "https://localhost:4200", "https://ProyectoSalud.net/", "https://ProyectoSalud.org/", "https://ProyectoSalud.com/");
                        builder.AllowAnyHeader();
                        builder.AllowCredentials();
                        builder.AllowAnyMethod();
                    }
                );
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings"));
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddTransient<IMailService, Services.MailService>();
            services.AddTransient<Seed>();
            services.AddDataProtection().DisableAutomaticKeyGeneration();
            // Conexion with Sql Server 
            // services.AddDbContext<DataContext>(x =>
            // {
            //     x.UseSqlServer(Configuration.GetConnectionString("SqlServerConnection"));
            // });
            services.AddDbContext<DataContext>(x =>
            {
                // x.UseLazyLoadingProxies();
                x.UseNpgsql(Configuration.GetConnectionString("PgAdminConnection"));
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Seed seeder, ILoggerFactory loggerFactory)
        {
            var path = Directory.GetCurrentDirectory();
            loggerFactory.AddFile($"{path}\\Logs\\GeneralLog.txt");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            seeder.SeedAll();
            app.UseCors(MyAllowSpecificOrigins);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{Id?}"
                );
                endpoints.MapFallbackToController("Index", "Home");
            });
        }
        void LoadRepositories(IServiceCollection services)
        {
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ICloudinaryRepository, CloudinaryRepository>();
            services.AddScoped<IConsultationRepository, ConsultationRepository>();
            services.AddScoped<IConsultingRoomRepository, ConsultingRoomRepository>();
            services.AddScoped<IInsureRepository, InsureRepository>();
            services.AddScoped<IMainRepository, MainRepository>();
            services.AddScoped<IMedicalHistoryRepository, MedicalHistoryRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<ISeedRepository, Seed>();
            services.AddScoped<ITelephoneRepository, TelephoneRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserValidation, UserValidation>();
        }
    }
}
