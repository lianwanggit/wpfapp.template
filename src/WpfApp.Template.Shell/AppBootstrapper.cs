using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Windows;
using Autofac;
using Autofac.Core;
using Caliburn.Micro;
using Hangfire;
using Serilog;
using WpfApp.Template.Core;
using WpfApp.Template.Data;
using WpfApp.Template.Shell.Autofac;
using WpfApp.Template.Shell.ViewModels;

namespace WpfApp.Template.Shell
{
	public class AppBootstrapper : CaliburnMetroAutofacBootstrapper<ShellViewModel>
	{
		private ILogger _logger;

		protected override void ConfigureContainer(ContainerBuilder builder)
		{
			string logsDirectory = Path.Combine(Environment.CurrentDirectory, "logs");

			// Configure Serilog pipeline
			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Debug()
				.WriteTo.RollingFile(Path.Combine(logsDirectory, "log-{Date}.txt"),
					outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {SourceContext} {Message}{NewLine}{Exception}")
				.CreateLogger();

			_logger = Log.Logger.ForContext<AppBootstrapper>();
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

		protected override void OnStartup(object sender, StartupEventArgs e)
		{
			_logger.Information("Setup WpfApp database");
			Container.Resolve<IWpfAppDbContext>().Migrate();

			_logger.Information("Create Hangfire database if not exist");
			Container.Resolve<HangfireDbContext>().Database.CreateIfNotExists();

			GlobalConfiguration.Configuration.UseSqlServerStorage("HangfireDbContext");

			using (var server = new BackgroundJobServer())
			{
				_logger.Information("Hangfire Server started");
			}

			base.OnStartup(sender, e);
		}

	}
}
