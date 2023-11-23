using System.Windows;
using System.Windows.Controls;
using WpfAppByCrippy.Properties;

namespace WpfAppByCrippy.Pages
{
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Settings.Default.AutoConnect = true;
            Settings.Default.Save();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Settings.Default.AutoConnect = false;
            Settings.Default.Save();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Settings.Default.AutoConnect)
                {
                    AutoConnectCheck.IsChecked = true;
                }
                else AutoConnectCheck.IsChecked = false;
            } 
            catch { }
        }
    }
}
