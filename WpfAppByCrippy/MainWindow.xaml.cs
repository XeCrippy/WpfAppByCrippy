using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAppByCrippy.Pages;

namespace WpfAppByCrippy
{
    public partial class MainWindow : Window
    {
        private bool l4d2;

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
    }
}
