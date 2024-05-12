using Autofac;
using FluentValidation;
using LocaMoto.Application.DTOs.Requests;
using LocaMoto.Application.Interfaces;
using LocaMoto.Application.Services;
using LocaMoto.Application.Validator;
using LocaMoto.Domain.Interfaces.Repositories;
using LocaMoto.Domain.Interfaces.Services;
using LocaMoto.Domain.Services;
using LocaMoto.Infrastructure.Data.Repositories;

namespace LocaMoto.Infrastructure.Crosscutting.IOC
{
    public class ConfigurationIOC
    {
        public static void Load(ContainerBuilder builder)
        {
            ConfigureRepositories(builder);

            ConfigureServices(builder);

            ConfigureApplication(builder);

            ConfigureValidators(builder);
        }

        private static void ConfigureValidators(ContainerBuilder builder)
        {
            builder
                .RegisterType<MotorcyleRequestDtoValidator>()
                .As<IValidator<MotorcycleRequestDto>>()
                .InstancePerLifetimeScope();

            builder
               .RegisterType<UserRequestDtoValidator>()
               .As<IValidator<UserRequestDto>>()
               .InstancePerLifetimeScope();
        }

        private static void ConfigureApplication(ContainerBuilder builder)
        {
            builder
                .RegisterType<MotorcycleApplicationService>()
                .As<IMotorcycleApplicationService>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<UserApplicationService>()
                .As<IUserApplicationService>()
                .InstancePerLifetimeScope();
        }

        private static void ConfigureServices(ContainerBuilder builder)
        {
            builder
                .RegisterType<MotorcycleService>()
                .As<IMotorcycleService>()
                .InstancePerLifetimeScope();

            builder
               .RegisterType<UserService>()
               .As<IUserService>()
               .InstancePerLifetimeScope();
        }

        private static void ConfigureRepositories(ContainerBuilder builder)
        {
            builder
                  .RegisterType<MotorcycleRepository>()
                  .As<IMotorcycleRepository>()
                  .InstancePerLifetimeScope();

            builder
                  .RegisterType<UserRepository>()
                  .As<IUserRepository>()
                  .InstancePerLifetimeScope();
        }
    }
}
