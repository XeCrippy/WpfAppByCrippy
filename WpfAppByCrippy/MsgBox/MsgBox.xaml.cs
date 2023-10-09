using System.Windows;
using System.Windows.Input;

namespace WpfAppByCrippy.MsgBox
{
    public partial class MsgBox : Window
    {
        public MsgBox()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MsgTitle.Text = App.MsgTitle;
            MsgContent.Text = App.MsgBody;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
