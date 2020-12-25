using Application.Common.Config;
using Autofac;
using DomainModel.Services;
using InMemoryDatabase;
using MediatR;
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
            RegisterDependencies();

            _scope = _container.BeginLifetimeScope();

            Mediator = _scope.Resolve<IMediator>();
            _config = config;
        }

        private void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            //register configuration
            builder.RegisterInstance(_config).As<ApplicationConfig>().SingleInstance();

            //register mediator components
            builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();

            // request & notification handlers
            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            // finally register our custom code (individually, or via assembly scanning)
            // - requests & handlers as transient, i.e. InstancePerDependency()
            // - pre/post-processors as scoped/per-request, i.e. InstancePerLifetimeScope()
            // - behaviors as transient, i.e. InstancePerDependency()
            //builder.RegisterAssemblyTypes(typeof(IRequestHandler<>).GetTypeInfo().Assembly).AsImplementedInterfaces().InstancePerDependency();
            //builder.RegisterAssemblyTypes(typeof(IRequestHandler<,>).GetTypeInfo().Assembly).AsImplementedInterfaces().InstancePerDependency();

            //register request handlers
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.IsAssignableFrom(typeof(IRequestHandler<>)))
                .AsSelf();

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
            AppDbContext context = null; 
            switch (_config.TypeOfEnvironment)
            {
                case TypeOfEnvironment.Development:
                    context = new InMemoryAppDbContext();
                    break;

                case TypeOfEnvironment.Production:
                    context = new AppDbContext();
                    break;
            }

            context.Users.Add(
                new DomainModel.Entities.User()
                {
                    Id = Guid.NewGuid(),
                    Username = "admin",
                    Email = new DomainModel.Entities.Email()
                    {
                        EmailAddress = "admin@mail.com",
                        IsVerified = true
                    },
                    PasswordCredential = new DomainModel.Entities.PasswordCredential(
                        "password",
                        new PasswordHashingService(),
                        new SecureRngService()),
                    Privileges = new List<string>() { "ADMIN" }
                });

            context.SaveChanges();

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
