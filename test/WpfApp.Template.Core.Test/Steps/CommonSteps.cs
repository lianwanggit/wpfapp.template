using Autofac;
using TechTalk.SpecFlow;
using WpfApp.Template.Data;

namespace WpfApp.Template.Core.Test.Steps
{
	[Binding]
	public class CommonSteps
	{
		private readonly ScenarioContext _context;

		public CommonSteps(ScenarioContext context)
		{
			_context = context;
		}

		[BeforeTestRun]
		public static void BeforeScenario()
		{
		}

		[BeforeScenario]
		public void Setup()
		{
			var builder = new ContainerBuilder();
			builder.RegisterModule(new CoreModule());
			builder.RegisterType<WpfAppTestDbContext>().As<IWpfAppDbContext>().SingleInstance();

			var container = builder.Build();
			_context.Set(container);
		}
	}
}
