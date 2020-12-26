using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSwag.Generation.Processors.Security;
using GimanaIdApi.Common.Authentication;
using GimanaIdApi.Common.Config;
using GimanaIdApi.Common.Mapper;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Application;
using Application.Common.Config;
using MediatR;

namespace GimanaIdApi
{
    public class Startup
    {
        private IWebHostEnvironment _env;

        public Startup(
            IWebHostEnvironment env)
        {
            _env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var config =
                new ApiConfig()
                {
                    AuthTokenLength = 32,
                    PasswordResetTokenLength = 32,
                    EmailVerificationTokenLength = 32,
                    EmailVerificationTokenLifetime = new TimeSpan(30, 0, 0, 0),
                    PasswordResetTokenLifetime = new TimeSpan(2, 0, 0),
                    ApiBaseAddress = Environment.GetEnvironmentVariable("API_BASE_ADDRESS")
                };

            services.AddSingleton(typeof(ApiConfig), config);

            services.AddSwaggerDocument(
                (config) =>
                {
                    config.DocumentProcessors.Add(
                    new SecurityDefinitionAppender("AuthToken",
                    new NSwag.OpenApiSecurityScheme
                    {
                        Type = NSwag.OpenApiSecuritySchemeType.ApiKey,
                        Name = "Auth-Token",
                        In = NSwag.OpenApiSecurityApiKeyLocation.Header,
                    }));
                    config.OperationProcessors.Add(new OperationSecurityScopeProcessor("AuthToken"));

                    config.PostProcess =
                    (document) =>
                    {
                        document.Info.Version = "v1";
                        document.Info.Title = "gimana.id API";
                        document.Info.Description = "The backend API for gimana.id, a group project for DTETI FT UGM's enterpreunership course.";
                        document.Info.Contact = new NSwag.OpenApiContact()
                        {
                            Name = "Firdaus Bisma Suryakusuma",
                            Email = "firdausbismasuryakusuma@mail.ugm.ac.id"
                        };
                    };
                });

            services.AddSingleton(
                typeof(IMapper), 
                new Mapper(new MapperConfig().GetConfiguration()));

            services.AddAuthentication(
                (config) =>
                {
                    config.DefaultScheme = "RandomTokenScheme";
                })
                .AddScheme<RandomTokenAuthenticationSchemeOptions, ValidateRandomTokenAuthenticationHandler>("RandomTokenScheme", (options) => { });

            var applicationConfig = new ApplicationConfig()
            {
                ApiBaseAddress = Environment.GetEnvironmentVariable("API_BASE_ADDRESS")
            };

            if (_env.IsDevelopment()) 
            {
                applicationConfig.TypeOfEnvironment = TypeOfEnvironment.Development;
            }
            else if (_env.IsProduction()) 
            {
                applicationConfig.TypeOfEnvironment = TypeOfEnvironment.Production;
            }
            else
            {
                throw new Exception("Invalid environment type.");
            }

            services.AddSingleton(typeof(IMediator), new Bootstrapper(applicationConfig).Mediator);

            services.AddAuthorization(config =>
            {
                config.AddPolicy(AuthorizationPolicyConstants.EmailVerifiedPolicy, policy => policy.RequireClaim("EmailVerified", "True"));
                config.AddPolicy(AuthorizationPolicyConstants.IsNotBannedPolicy, policy => policy.RequireClaim("IsBanned", "False"));
                config.AddPolicy(AuthorizationPolicyConstants.ModeratorOnlyPolicy, policy => policy.RequireClaim("IsModerator", "True"));
                config.AddPolicy(AuthorizationPolicyConstants.AdminOnlyPolicy, policy => policy.RequireClaim("IsAdmin", "True"));
                config.AddPolicy(AuthorizationPolicyConstants.AuthenticatedUsersOnlyPolicy, policy => policy.RequireClaim("UserId"));
            });

            services.AddSpaStaticFiles(config => config.RootPath = "ClientApp/build");

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddMvc().AddControllersAsServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            if (_env.IsDevelopment()) 
            {
                app.UseOpenApi();
                app.UseSwaggerUi3();
            }

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (_env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer("start");
                }
            });
        }
    }
}
