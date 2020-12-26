using Application.Common.Config;
using Autofac;
using DomainModel.Entities;
using DomainModel.Services;
using DomainModel.ValueObjects;
using InMemoryDatabase;
using MediatR;
using MediatR.Extensions.Autofac.DependencyInjection;
using PostgresDatabase;
using SimpleDomainServiceImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class Bootstrapper
    {
        public IMediator Mediator { get; }

        private IContainer _container;
        private ILifetimeScope _scope;

        private readonly ApplicationConfig _config;

        public Bootstrapper(ApplicationConfig config)
        {
            _config = config;

            RegisterDependencies();

            _scope = _container.BeginLifetimeScope();

            Mediator = _scope.Resolve<IMediator>();
        }

        private void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            //register configuration
            builder.RegisterInstance(_config).As<ApplicationConfig>().SingleInstance();

            //register mediator components
            builder.RegisterMediatR(Assembly.GetExecutingAssembly());

            //register domain service implementations
            builder.RegisterInstance(new AlphanumericTokenGenerator()).As<IAlphanumericTokenGenerator>().SingleInstance();

            builder.RegisterInstance(new DateTimeService()).As<IDateTimeService>().SingleInstance();

            switch (_config.TypeOfEnvironment)
            {
                case TypeOfEnvironment.Development:
                    builder.RegisterType<DebugEmailSender>().As<IEmailSender>().SingleInstance();
                    break;

                case TypeOfEnvironment.Production:
                    builder.RegisterInstance(new SmtpEmailSender(
                        Environment.GetEnvironmentVariable("EMAIL_CREDENTIAL_ADDRESS"),
                        Environment.GetEnvironmentVariable("EMAIL_CREDENTIAL_PASSWORD"))).As<IEmailSender>().SingleInstance();
                    break;
            }

            builder.RegisterInstance(new PasswordHashingService()).As<IPasswordHashingService>().SingleInstance();

            builder.RegisterInstance(new SecureRngService()).As<ISecureRngService>().SingleInstance();

            //initialize and register database
            AppDbContext dbContext = null; 
            switch (_config.TypeOfEnvironment)
            {
                case TypeOfEnvironment.Development:
                    dbContext = new InMemoryAppDbContext();
                    break;

                case TypeOfEnvironment.Production:
                    dbContext = new AppDbContext();
                    break;
            }

            var user = new User()
            {
                Id = Guid.NewGuid(),
                Username = "admin",
                Email = new Email()
                {
                    EmailAddress = "admin@mail.com",
                    IsVerified = true
                },
                PasswordCredential = new PasswordCredential(
                    "password",
                    new PasswordHashingService(),
                    new SecureRngService()),
                Privileges = new List<UserPrivilege>() { new UserPrivilege() { PrivilegeName = "ADMIN" } }
            };

            dbContext.Users.AddAsync(user);

            dbContext.SaveChanges();

            switch (_config.TypeOfEnvironment)
            {
                case TypeOfEnvironment.Development:
                    builder.RegisterType<InMemoryAppDbContext>().As<AppDbContext>().InstancePerDependency();
                    break;

                case TypeOfEnvironment.Production:
                    builder.RegisterType<AppDbContext>().As<AppDbContext>().InstancePerDependency();
                    break;
            }

            _container = builder.Build();
        }
    }
}
