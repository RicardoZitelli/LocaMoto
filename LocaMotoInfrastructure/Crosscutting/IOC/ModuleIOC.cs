
using Autofac;

namespace LocaMotoInfrastructure.Crosscutting.IOC
{
    public sealed class ModuleIOC : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            ConfigurationIOC.Load(builder);
        }
    }
}
