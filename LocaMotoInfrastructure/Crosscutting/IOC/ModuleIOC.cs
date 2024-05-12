
using Autofac;

namespace LocaMoto.Infrastructure.Crosscutting.IOC
{
    public sealed class ModuleIOC : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            ConfigurationIOC.Load(builder);
        }
    }
}
