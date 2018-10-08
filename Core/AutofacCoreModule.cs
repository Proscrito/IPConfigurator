using Autofac;

namespace Core
{
    public class AutofacCoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<NetworkManager>().As<INetworkManager>();
        }
    }
}
