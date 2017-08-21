using System;
using Serilog;

namespace WpfApp.Template.Core
{
	public class SimpleService : ISimpleService
	{
		private readonly ILogger _logger;

		public SimpleService(ILogger logger)
		{
			_logger = logger.ForContext<SimpleService>();
		}

		public void DoWork()
		{
			_logger.Information("DoWork");
		}
	}
}
