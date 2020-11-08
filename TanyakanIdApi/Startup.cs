using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSwag.Generation.Processors.Security;
using TanyakanIdApi.Common.Authentication;
using TanyakanIdApi.Common.Config;
using TanyakanIdApi.Common.Mapper;
using TanyakanIdApi.Entities.Entities;
using TanyakanIdApi.Infrastructure.AlphanumericTokenGenerator;
using TanyakanIdApi.Infrastructure.DataAccess;
using TanyakanIdApi.Infrastructure.EmailSender;
using TanyakanIdApi.Infrastructure.PasswordHasher;
using TanyakanIdApi.Infrastructure.SecurePasswordSaltGenerator;

namespace TanyakanIdApi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
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
                        document.Info.Title = "tanyakan.id API";
                        document.Info.Description = "The backend API for tanyakan.id, a group project for DTETI FT UGM's enterpreunership course.";
                        document.Info.Contact = new NSwag.OpenApiContact()
                        {
                            Name = "Firdaus Bisma Suryakusuma",
                            Email = "firdausbismasuryakusuma@mail.ugm.ac.id"
                        };
                    };
                });

            #region init db
            var dbContext = new AppDbContext();

            dbContext.Users.Add(
                new User()
                {
                    Id = Guid.NewGuid(),
                    Username = "admin",
                    Email = new UserEmail()
                    {
                        EmailAddress = "goldenpanzer17@gmail.com",
                        IsVerified = true
                    },
                    BanLiftedDate = DateTime.MinValue,
                    Privileges = new List<UserPrivilege>() { new UserPrivilege() { PrivilegeName = "Admin" } },
                    PasswordCredential = new PasswordCredential()
                    {
                        HashedPassword = "7aUjVyYk+LCwYsqFc+QJt2psYGPvU0+upOMc5gN1INWpU2W6Cy1TYBC8GqLz2Ivvewbil4in2ZYM47sial3E2aSqddQB2oKiPYSg9SKLypk3OclNUvOCvEWyeRKxwPTwwMQoaJbyVVh0a5wxmUSGWK/UJQORGCYiFNBq7Sh2mYt5/f3rIYfTYx23jhAYwPNhDKSHx6LsPQQY31I6XXvlIIv3KIR7Fae/5v9D4SLZTfMkeYpFAW+cTCH4iagHETRfaOS938x1tkXmO0LAEopVOy+WhqWsHwaZg96J9U9i71aZa4yk5Lja51dmEhlfYEJtXxpc0bKDWU2PmQ4rekHxWw==",
                        PasswordSalt = "dcU9mccExbfd189crlqTTvWFhG76Z5vfItdnF/tGUy4H1FKGM+y4OgGrFK36BT4OkSspri4poshIj9v3fiDFvw=="
                    }
                });

            dbContext.SaveChanges();
            #endregion
            services.AddSingleton(typeof(AppDbContext), dbContext);

            services.AddSingleton(
                typeof(IMapper), 
                new Mapper(new MapperConfig().GetConfiguration()));

            services.AddAuthentication(
                (config) =>
                {
                    config.DefaultScheme = "RandomTokenScheme";
                })
                .AddScheme<RandomTokenAuthenticationSchemeOptions, ValidateRandomTokenAuthenticationHandler>("RandomTokenScheme", (options) => { });

            services.AddSingleton(
                typeof(IEmailSender),
                new SmtpEmailSender(
                    Environment.GetEnvironmentVariable("EMAIL_CREDENTIAL_ADDRESS"),
                    Environment.GetEnvironmentVariable("EMAIL_CREDENTIAL_PASSWORD")));

            services.AddSingleton(
                typeof(IPasswordHasher),
                new PasswordHasher());

            services.AddSingleton(
                typeof(ISecurePasswordSaltGenerator),
                new SecurePasswordSaltGenerator());

            services.AddSingleton(
                typeof(IAlphanumericTokenGenerator),
                new AlphanumericTokenGenerator());

            services.AddSingleton(
                new ApiConfig()
                {
                    AuthTokenLength = 32,
                    PasswordResetTokenLength = 32,
                    EmailVerificationTokenLength = 32,
                    EmailVerificationTokenLifetime = new TimeSpan(30, 0, 0, 0),
                    PasswordResetTokenLifetime = new TimeSpan(2, 0, 0),
                    ApiBaseAddress = Environment.GetEnvironmentVariable("API_BASE_ADDRESS")
                });

            services.AddAuthorization(
                (config) =>
                {
                    config.AddPolicy(AuthorizationPolicyConstants.EmailVerifiedPolicy, policy => policy.RequireClaim("EmailVerified", "True"));
                    config.AddPolicy(AuthorizationPolicyConstants.IsNotBannedPolicy, policy => policy.RequireClaim("IsBanned", "False"));
                    config.AddPolicy(AuthorizationPolicyConstants.ModeratorOnlyPolicy, policy => policy.RequireClaim("IsModerator", "True"));
                    config.AddPolicy(AuthorizationPolicyConstants.AdminOnlyPolicy, policy => policy.RequireClaim("IsAdmin", "True"));
                    config.AddPolicy(AuthorizationPolicyConstants.AuthenticatedUsersOnlyPolicy, policy => policy.RequireClaim("UserId"));
                });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
