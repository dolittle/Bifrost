using System;
using System.Windows;
using Bifrost.Configuration.Defaults;
using Bifrost.Execution;
using Bifrost.Ninject;
using Bifrost.Notification;
using Microsoft.Practices.ServiceLocation;
using Ninject;

namespace Bifrost.Mimir
{
    public partial class App
	{
		public static IContainer Container;

		static App()
		{
            var dispatcher = new Dispatcher(Deployment.Current.Dispatcher);
            DispatcherManager.Current = dispatcher;
		}

		public App()
		{
			Startup += Application_Startup;
			Exit += Application_Exit;
			UnhandledException += Application_UnhandledException;

            var kernel = new StandardKernel();
            Container = new Container(kernel);
            var serviceLocator = new ContainerServiceLocator(Container);
            Container.Bind<IServiceLocator>(serviceLocator);
            ServiceLocator.SetLocatorProvider(() => serviceLocator);

            var bindings = new DefaultBindings();
            bindings.Initialize(Container);

            var conventions = new DefaultConventions();
            conventions.Initialize();

			InitializeComponent();
		}

		private void Application_Startup(object sender, StartupEventArgs e)
		{
            var path = Application.Current.Host.Source.AbsolutePath;
			RootVisual = new MainPage();
		}

		private void Application_Exit(object sender, EventArgs e)
		{

		}

		private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
		{
			// If the app is running outside of the debugger then report the exception using
			// the browser's exception mechanism. On IE this will display it a yellow alert 
			// icon in the status bar and Firefox will display a script error.
			if (!System.Diagnostics.Debugger.IsAttached)
			{

				// NOTE: This will allow the application to continue running after an exception has been thrown
				// but not handled. 
				// For production applications this error handling should be replaced with something that will 
				// report the error to the website and stop the application.
				e.Handled = true;
				Deployment.Current.Dispatcher.BeginInvoke(delegate { ReportErrorToDOM(e); });
			}
		}

		private void ReportErrorToDOM(ApplicationUnhandledExceptionEventArgs e)
		{
			try
			{
				string errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
				errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

				System.Windows.Browser.HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
			}
			catch (Exception)
			{
			}
		}
	}
}
