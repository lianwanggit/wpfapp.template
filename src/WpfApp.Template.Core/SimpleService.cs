using System;
using Serilog;
using WpfApp.Template.Data;

namespace WpfApp.Template.Core
{
	public class SimpleService : ISimpleService
	{
		private readonly ILogger _logger;
		private readonly IWpfAppDbContext _dbContext;

		public SimpleService(ILogger logger, IWpfAppDbContext dbContext)
		{
			_dbContext = dbContext;
			_logger = logger.ForContext<SimpleService>();
		}

		public void DoWork()
		{
			_logger.Information("DoWork");
		}
	}
}
