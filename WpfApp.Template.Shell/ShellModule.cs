using Autofac;
using Caliburn.Micro;

namespace WpfApp.Template.Shell
{
	public class ShellModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterAssemblyTypes(ThisAssembly)
				 .AssignableTo<Screen>()
				 .AsSelf();
		}
	}
}
