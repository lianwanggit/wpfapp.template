using System;
using System.Linq;
using Hangfire;
using Serilog;
using WpfApp.Template.Data;

namespace WpfApp.Template.Core
{
	public class SimpleService : ISimpleService
	{
		private readonly ILogger _logger;
		private readonly IWpfAppDbContext _dbContext;
		private readonly IBackgroundJobClient _backgroundJob;

		public SimpleService(ILogger logger,
			IBackgroundJobClient backgroundJob,
			IWpfAppDbContext dbContext)
		{
			_dbContext = dbContext;
			_backgroundJob = backgroundJob;

			_logger = logger.ForContext<SimpleService>();
		}

		public void DoWork()
		{
			_backgroundJob.Enqueue(() => Task());
		}

		public void Task()
		{
			_logger.Information("Fire-and-forget!");
			var persons = _dbContext.Persons.ToList();

			_logger.Information("Persion: {0}", persons);
		}
	}
}
