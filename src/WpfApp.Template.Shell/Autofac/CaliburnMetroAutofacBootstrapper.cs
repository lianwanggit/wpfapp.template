using Caliburn.Micro;

namespace WpfApp.Template.Shell.Autofac
{
	public class CaliburnMetroAutofacBootstrapper<TRootModel> : AutofacBootstrapper<TRootModel>
	{
		protected override void ConfigureBootstrapper()
		{
			AutoSubscribeEventAggegatorHandlers = false;
			CreateWindowManager = () => new MetroWindowManager();
			CreateEventAggregator = () => new EventAggregator();

			EnforceNamespaceConvention = true;
			ViewModelBaseType = typeof(System.ComponentModel.INotifyPropertyChanged);
		}
	}
}
