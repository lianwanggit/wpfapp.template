using Autofac;
using WpfApp.Template.Data;


// 
namespace WpfApp.Template.Core
{
	public class CoreModule : Module
    {
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<SimpleService>().As<ISimpleService>().SingleInstance();
			builder.RegisterType<WpfAppDbContext>().As<IWpfAppDbContext>();
			builder.RegisterType<HangfireDbContext>();
		}
	}
}
