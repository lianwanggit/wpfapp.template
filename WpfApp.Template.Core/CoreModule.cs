using Autofac;

namespace WpfApp.Template.Core
{
	public class CoreModule : Module
    {
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<SimpleService>().As<ISimpleService>().SingleInstance();
		}
	}
}
