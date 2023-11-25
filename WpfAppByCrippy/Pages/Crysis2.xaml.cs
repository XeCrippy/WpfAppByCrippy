using System;
using System.Windows;
using System.Windows.Controls;
using WpfAppByCrippy.TitleHelpers;

namespace WpfAppByCrippy.Pages
{
    public partial class Crysis2 : Page
    {
        private readonly Crysis2Helper helper = new();

        public Crysis2()
        {
            InitializeComponent();
        }

        private void DemigodBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                helper.Demigod(DemigodBtn);
            }
            catch (Exception ex)
            { 
                App.Error("Crysis 2 : Demigod", ex);
            }
        }

        private void AmmoBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                helper.InfiniteAmmo(AmmoBtn);
            }
            catch (Exception ex) 
            { 
                App.Error("Crysis 2 : Infinite Ammo", ex); 
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            FovBtn.Content = "FOV : " + (int)e.NewValue;
        }

        private void CmdBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (App.activeConnection)
                {
                    helper.ExecuteStringInternal(CmdBox.Text);
                }
                else App.ConnectionError();
            }
            catch(Exception ex) 
            { 
                App.Error("Crysis 2 : Send Command", ex);
            }
        }

        private void FovBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (App.activeConnection)
                {
                    helper.ExecuteStringInternal("cl_fov " + FovSlider.Value.ToString());
                }
                else App.ConnectionError();
            }
            catch(Exception ex)
            { 
                App.Error("Crysis 2 : FOV", ex);
            }
        }

        private void AimAssistBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                helper.AimAssist(AimAssistBtn);
            }
            catch (Exception ex) 
            { 
                App.Error("Crysis 2 : Aim Assist", ex);
            }
        }
    }
}
