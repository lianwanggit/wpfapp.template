using MahApps.Metro.Controls;
using WpfApp.Template.Shell.Autofac;
using WpfApp.Template.Shell.Views;

namespace WpfApp.Template.Shell
{
	public class AppWindowManager : MetroWindowManager
	{
		public override MetroWindow CreateCustomWindow(object view, bool windowIsView)
		{
			if (windowIsView)
			{
				return view as ShellView;
			}

			return new ShellView
			{
				Content = view
			};
		}
	}
}
