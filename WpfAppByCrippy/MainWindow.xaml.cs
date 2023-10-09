using System;
using System.Windows;
using System.Windows.Input;

namespace WpfAppByCrippy
{
    public partial class MainWindow : Window
    {
        private bool l4d2;
        private bool mk;

        public MainWindow()
        {
            InitializeComponent();
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
            if (!l4d2)
            {
                NavFrame1.Source= new Uri("Pages/L4D2.xaml", UriKind.RelativeOrAbsolute);
                l4d2 = true;
            }
            else
            {
                NavFrame1.Content = null;
                l4d2 = false;
            }
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
            if (!mk)
            {
                NavFrame1.Source = new Uri("Pages/MortalKombatKompleteEdition.xaml", UriKind.RelativeOrAbsolute);
                mk = true;
            }
            else
            {
                NavFrame1.Content = null;
                mk = false;
            }
        }

        private void Crysis2Btn_Click(object sender, RoutedEventArgs e)
        {
            NavFrame1.Source = new Uri("Pages/Crysis2.xaml", UriKind.RelativeOrAbsolute);
        }
    }
}
