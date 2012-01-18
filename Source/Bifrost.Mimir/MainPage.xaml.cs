using System.Windows;
using GalaSoft.MvvmLight.Messaging;

namespace Bifrost.Mimir
{
	public partial class MainPage
	{
		public MainPage()
		{
			InitializeComponent();

            Messenger.Default.Register<DialogMessage>(this, DialogMessageReceived);
		}

        void DialogMessageReceived(DialogMessage dialogMessage)
        {
            MessageBox.Show(dialogMessage.Content, "Admin tool", MessageBoxButton.OK);
        }
	}
}
