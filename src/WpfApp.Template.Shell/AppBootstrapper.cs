using System;
using System.IO;
using System.Linq;
using Autofac;
using Caliburn.Micro;
using Serilog;
using WpfApp.Template.Core;
using WpfApp.Template.Shell.Autofac;
using WpfApp.Template.Shell.ViewModels;

namespace WpfApp.Template.Shell
{
	public class AppBootstrapper : CaliburnMetroAutofacBootstrapper<ShellViewModel>
	{
		// private ILogger _logger;

		protected override void ConfigureContainer(ContainerBuilder builder)
		{
			string logsDirectory = Path.Combine(Environment.CurrentDirectory, "logs");

			// Configure Serilog pipeline
			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Debug()
				.WriteTo.RollingFile(Path.Combine(logsDirectory, "log-{Date}.txt"),
					outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {SourceContext} {Message}{NewLine}{Exception}")
				.CreateLogger();

			builder.RegisterInstance(Log.Logger);

			// register modules
			builder.RegisterModule(new CoreModule());

			builder.RegisterType<AppWindowManager>().As<IWindowManager>().SingleInstance();
			var assembly = typeof(ShellViewModel).Assembly;
			builder.RegisterAssemblyTypes(assembly)
				 .Where(item => item.Name.EndsWith("ViewModel") && item.IsAbstract == false)
				 .AsSelf()
				 .SingleInstance();
		}

	}
}
