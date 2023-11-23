using System;
using System.Windows;
using System.Windows.Input;
using WpfAppByCrippy.Properties;

namespace WpfAppByCrippy
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Settings.Default.AutoConnect)
                {
                    if (!App.Connected())
                    {
                        App.ConnectionError();
                    }
                }
            }
            catch(Exception ex) { App.Error(ex); }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
 
        private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
        { 
             WindowState = WindowState.Minimized;
        }

        private void L4D2Btn_Click(object sender, RoutedEventArgs e)
        {
            NavFrame1.Source = new Uri("Pages/L4D2.xaml", UriKind.RelativeOrAbsolute);
        }

        private void ConnectBtn2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!App.Connected())
                {
                    App.ConnectionError();
                }
            }
            catch (Exception ex)
            {
                App.Error(ex);
            }
        }

        private void MKBtn_Click(object sender, RoutedEventArgs e)
        {
            NavFrame1.Source = new Uri("Pages/MortalKombatKompleteEdition.xaml", UriKind.RelativeOrAbsolute);
        }

        private void Crysis2Btn_Click(object sender, RoutedEventArgs e)
        {
            NavFrame1.Source = new Uri("Pages/Crysis2.xaml", UriKind.RelativeOrAbsolute);
        }

        private void Juiced2Btn_Click(object sender, RoutedEventArgs e)
        {
            NavFrame1.Source = new Uri("Pages/Juiced2Page.xaml", UriKind.RelativeOrAbsolute);
        }

        private void SleepingDogsBtn_Click(object sender, RoutedEventArgs e)
        {
            NavFrame1.Source = new Uri("Pages/SleepingDogsPage.xaml", UriKind.RelativeOrAbsolute);
        }

        private void ResidentEvil6Btn_Click(object sender, RoutedEventArgs e)
        {
            NavFrame1.Source = new Uri("Pages/ResidentEvil6Page.xaml", UriKind.RelativeOrAbsolute);
        }

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            NavFrame1.Source = new Uri("Pages/SettingsPage.xaml", UriKind.RelativeOrAbsolute);
        }
    }
}
