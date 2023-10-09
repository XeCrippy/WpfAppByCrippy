﻿using System;
using System.Windows;
using System.Windows.Controls;
using WpfAppByCrippy.TitleHelpers;

namespace WpfAppByCrippy.Pages
{
    public partial class L4D2 : Page
    {
        private readonly Left4Dead2Helper helper = new();
        
        public L4D2()
        {
            InitializeComponent();
        }

        private void GodModeBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                helper.GodMode(GodModeBtn);
            }
            catch (Exception ex) { App.Error(ex); }
        }

        private void SendCmdBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (App.activeConnection)
                    Left4Dead2Helper.Cbuf_AddText(CmdBox.Text);
                else App.ConnectionError();
            }
            catch (Exception ex) { App.Error(ex); }
        }

        private void AmmoBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                helper.InfiniteAmmo(AmmoBtn);
            }
            catch(Exception ex) { App.Error(ex); }
        }

        private void FpsBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                helper.FpsCounter(FpsBtn);
            }
            catch(Exception ex) { App.Error(ex); }
        }

        private void PlayerPosBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                helper.PlayerPosition(PlayerPosBtn);
            }
            catch(Exception ex) { App.Error(ex); }
        }

        private void GraphsBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                helper.ShowGraphs(GraphsBtn);
            }
            catch(Exception ex) { App.Error(ex);}
        }

        private void SvCheatsBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                helper.SV_Cheats(SvCheatsBtn);
            }
            catch (Exception ex) { App.Error(ex); }
        }

        private void NoClipBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Left4Dead2Helper.NoClip();
            }
            catch(Exception ex) { App.Error(ex);}
        }
    }
}
