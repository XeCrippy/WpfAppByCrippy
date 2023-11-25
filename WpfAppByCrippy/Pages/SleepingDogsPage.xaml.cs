using System;
using System.Windows;
using System.Windows.Controls;
using WpfAppByCrippy.TitleHelpers;

namespace WpfAppByCrippy.Pages
{
    /// <summary>
    /// Interaction logic for SleepingDogsPage.xaml
    /// </summary>
    public partial class SleepingDogsPage : Page
    {
        readonly SleepingDogsHelper helper = new();

        public SleepingDogsPage()
        {
            InitializeComponent();
        }

        private void HealthBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (App.activeConnection)
                {
                    helper.SetPlayerHealth(9999999.0f);
                }
                else App.ConnectionError();
            }
            catch (Exception ex) { App.Error("Sleeping Dogs : Set Health", ex); }
        }

        private void MoneyBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (App.activeConnection)
                {
                    helper.AddMoney(MoneyBox);
                }
                else App.ConnectionError();
            }
            catch(Exception ex) { App.Error("Sleeping Dogs : Set Money", ex); }
        }
    }
}
