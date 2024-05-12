using Autofac;
using FluentValidation;
using LocaMotoApplication.DTOs.Requests;
using LocaMotoApplication.Interfaces;
using LocaMotoApplication.Services;
using LocaMotoApplication.Validator;
using LocaMotoDomain.Interfaces.Repositories;
using LocaMotoDomain.Interfaces.Services;
using LocaMotoDomain.Services;
using LocaMotoInfrastructure.Data.Repositories;

namespace LocaMotoInfrastructure.Crosscutting.IOC
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
        }

        private static void ConfigureApplication(ContainerBuilder builder)
        {
            builder
                .RegisterType<MotorcycleApplicationService>()
                .As<IMotorcycleApplicationService>()
                .InstancePerLifetimeScope();
        }

        private static void ConfigureServices(ContainerBuilder builder)
        {
            builder
                .RegisterType<MotorcycleService>()
                .As<IMotorcycleService>()
                .InstancePerLifetimeScope();
        }

        private static void ConfigureRepositories(ContainerBuilder builder)
        {
            builder
                  .RegisterType<MotorcycleRepository>()
                  .As<IMotorcycleRepository>()
                  .InstancePerLifetimeScope();
        }
    }
}
