using System.Dynamic;
using System.Windows;
using Caliburn.Micro;
using WpfApp.Template.Core;

namespace WpfApp.Template.Shell.ViewModels
{
	public class ShellViewModel : Screen
	{
		private readonly IWindowManager _windowManager;
		private readonly ISimpleService _simpleService;

		public ShellViewModel(IWindowManager windowManager, 
			ISimpleService simpleService,
			ChildViewModel child)
		{
			_windowManager = windowManager;
			_simpleService = simpleService;

			Child = child;
		}

		protected override void OnInitialize()
		{
			DisplayName = "Shell";

			_simpleService.DoWork();
		}

		public ChildViewModel Child { get; }

		public void OpenWindow()
		{
			dynamic settings = new ExpandoObject();
			settings.WindowStartupLocation = WindowStartupLocation.Manual;

			_windowManager.ShowWindow(new ChildViewModel(), null, settings);
		}
	}
}
