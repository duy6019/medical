using System;
using System.IO;
using System.Reflection;
using Bravure.Entities;
using Bravure.Infrastructure;
using Bravure.Infrastructure.Auth;
using Bravure.Infrastructure.Emails;
using Bravure.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Bravure.Services;
namespace Bravure
{
    public class Startup
    {
        private const string _apiVersion = "v1";
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            Env = webHostEnvironment;
        }

        public IWebHostEnvironment Env { get; set; }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting(opt => { opt.LowercaseUrls = true; opt.LowercaseQueryStrings = true; });

            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
            });
            services.AddResponseCaching();
            services.AddDbContext<BravureDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(60);
                options.Password.RequiredLength = 8;
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<BravureDbContext>()
                .AddClaimsPrincipalFactory<ClaimsPrincipalFactory>()
                .AddSignInManager<ApplicationSignInManager<ApplicationUser>>()
                .AddDefaultTokenProviders();

            services.AddScoped<ClaimsPrincipalFactory>();
            services.AddOptions<BravureOptions>().Bind(Configuration.GetSection(nameof(BravureOptions)));
            AuthConfigurer.Configure(services, Configuration);
            AuthConfigurer.ConfigureTokenAuth(services, Configuration);

            var bravureOptions = Configuration.GetSection(nameof(BravureOptions)).Get<BravureOptions>();
            bool useDummyServices = bravureOptions.UseDummyServices;

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IEmailHtmlParser, EmailHtmlParser>();
            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IUserAppService, UserAppService>();
            services.AddTransient<IPatientService, PatientService>();
            services.AddTransient<IMedicineService, MedicineService>();
            services.AddTransient<IMedicalExaminationService, MedicalExaminationService>();
            services.AddTransient<IMedicalAssistanceService, MedicalAssistanceService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<ICilinicExaminationService, CilinicExaminationService>();

            if (useDummyServices)
            {
                services.AddTransient<IEmailSender, DummyEmailSender>();
                services.AddTransient<ITemplateEmailSender, DummyEmailSender>();
            }
            else
            {
                services.AddTransient<IEmailSender, EmailSender>();
                services.AddTransient<ITemplateEmailSender, EmailSender>();
            }

            services.AddAutoMapper(typeof(Startup));
            ConfigureSwagger(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
                app.UseMiniProfiler();
            }
            else
            {
                app.UseExceptionHandler("/error/500");
                app.UseStatusCodePagesWithReExecute("/error/{0}");

                HstsBuilderExtensions.UseHsts(app);
            }
            app.UseStaticFiles();

            app.UseRouting();
            app.UseResponseCaching();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("defaultWithArea", "{area}/{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger();
            app.UseSwaggerUI();
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(_apiVersion, new OpenApiInfo
                {
                    Version = _apiVersion,
                    Title = "Project API",
                    Description = "Project",
                    // uncomment if needed TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Project",
                        Email = string.Empty,
                        Url = new Uri("https://twitter.com/aspboilerplate"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT License",
                        Url = new Uri("https://github.com/aspnetboilerplate/aspnetboilerplate/blob/dev/LICENSE"),
                    }
                });
                options.DocInclusionPredicate((docName, description) => true);

                // Define the BearerAuth scheme that's in use
                options.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme()
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                //add summaries to swagger
                bool canShowSummaries = Configuration.GetValue<bool>("Swagger:ShowSummaries");
                if (canShowSummaries)
                {
                    var hostXmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var hostXmlPath = Path.Combine(AppContext.BaseDirectory, hostXmlFile);
                    options.IncludeXmlComments(hostXmlPath);

                    var applicationXml = $"Project.Application.xml";
                    var applicationXmlPath = Path.Combine(AppContext.BaseDirectory, applicationXml);
                    options.IncludeXmlComments(applicationXmlPath);

                    var webCoreXmlFile = $"Project.Web.Core.xml";
                    var webCoreXmlPath = Path.Combine(AppContext.BaseDirectory, webCoreXmlFile);
                    options.IncludeXmlComments(webCoreXmlPath);
                }
            });
        }


    }
}
