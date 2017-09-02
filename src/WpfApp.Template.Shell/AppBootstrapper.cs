using System;
using System.IO;
using System.Linq;
using System.Windows;
using Autofac;
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
		private BackgroundJobServer _jobServer;

		protected override void ConfigureContainer(ContainerBuilder builder)
		{
			string logsDirectory = Path.Combine(Environment.CurrentDirectory, "logs");

			// Configure Serilog pipeline
			var logger = new LoggerConfiguration()
				.MinimumLevel.Information()
				.WriteTo.RollingFile(Path.Combine(logsDirectory, "log-{Date}.txt"),
					outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {SourceContext} {Message}{NewLine}{Exception}")
				.CreateLogger();

			Log.Logger = logger;
			_logger = Log.Logger.ForContext<AppBootstrapper>();

			builder.RegisterInstance(logger);
			builder.RegisterInstance(Log.Logger);

			// Hangfire
			builder.RegisterType<BackgroundJobClient>().As<IBackgroundJobClient>();

			// Modules
			builder.RegisterModule(new CoreModule());

			// WindowManager and ViewModels
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

			GlobalConfiguration.Configuration.UseAutofacActivator(Container, false);
			GlobalConfiguration.Configuration.UseSqlServerStorage("HangfireDbContext");

			_jobServer = new BackgroundJobServer();
			_logger.Information("Hangfire Server started");

			base.OnStartup(sender, e);
		}

		protected override void OnExit(object sender, EventArgs e)
		{
			_jobServer.Dispose();

			base.OnExit(sender, e);
		}

	}
}
