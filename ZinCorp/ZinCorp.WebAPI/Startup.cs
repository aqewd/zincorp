using System;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using ZinCorp.BE;
using ZinCorp.BL.Customers;
using ZinCorp.BL.Products;
using ZinCorp.DAL;
using ZinCorp.WebAPI.Auth;
using ZinCorp.WebAPI.Configuration;
using ZinCorp.WebAPI.Helpers;
using ZinCorp.WebAPI.Models;

namespace ZinCorp.WebAPI
{
    public class Startup
    {
        private const string SecretKey = "qGbNivDRj1iqsfhPVkH23sMRmHLpUA2d";
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDataProtection();
            services.AddDistributedMemoryCache();
            services.AddSession();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });

            services.AddDbContext<DBContext>(item => item.UseSqlServer(Configuration.GetConnectionString("ZinCorp")));
            services.AddControllers();
            services.AddScoped<DBContext>();

            services.AddScoped<ICustomersBL, CustomersBL>();
            services.AddScoped<IProductsBL, ProductsBL>();
            services.AddScoped<IJwtFactory, JwtFactory>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCookiePolicy();
            app.UseSession();

            app.Use(async (context, next) =>
            {
                if (context.Session.Get<Customer>("customer") == null && context.Request.Cookies.ContainsKey("login") && context.Request.Cookies.ContainsKey("password"))
                {
                    try
                    {
                        var userBL = app.ApplicationServices.GetService<ICustomersBL>();
                        var dataProtectionProvider = app.ApplicationServices.GetService<IDataProtectionProvider>();
                        var dataProtector = dataProtectionProvider.CreateProtector(Constants.DataProtectorPurpose);

                        var login = dataProtector.Unprotect(context.Request.Cookies["login"]);
                        var password = dataProtector.Unprotect(context.Request.Cookies["password"]);

                        var userToVerify = userBL.Auth(login, password);
                        if (userToVerify != null)
                        {
                            context.Session.Set("user", userToVerify);
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }
                await next.Invoke();
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
