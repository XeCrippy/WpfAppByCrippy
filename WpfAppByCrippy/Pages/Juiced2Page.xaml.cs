using System;
using System.Windows;
using System.Windows.Controls;
using WpfAppByCrippy.TitleHelpers;

namespace WpfAppByCrippy.Pages
{
    public partial class Juiced2Page : Page
    {
        readonly Juiced2Helper helper = new();

        public Juiced2Page()
        {
            InitializeComponent();
        }

        private void MoneyBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (App.activeConnection)
                {
                    helper.SetTotalMoney(MoneyBox);
                }
                else App.ConnectionError();
            }
            catch(Exception ex) { App.Error("Juiced 2 : Set Money", ex); }
        }
    }
}
