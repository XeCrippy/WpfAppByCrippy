using JRPCPlusPlus;
using System;
using System.Windows;
using System.Windows.Controls;
using WpfAppByCrippy.TitleHelpers;

namespace WpfAppByCrippy.Pages
{
    public partial class ResidentEvil6Page : Page
    {
        readonly ResidentEvil6Helper helper = new();

        public ResidentEvil6Page()
        {
            InitializeComponent();
        }

        private void GodBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                helper.GodMode(GodBtn);
            }
            catch(Exception ex) { App.Error(ex); }
        }

        private void AmmoBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                helper.InfiniteAmmo(AmmoBtn);
            }
            catch(Exception ex) { App.Error(ex); }
        }

        private void StaminaBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                helper.InfiniteStamina(StaminaBtn);
            }
            catch(Exception ex) { App.Error(ex); }
        }

        private void FreezeTimerBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                helper.FreezeMercTimer(FreezeTimerBtn);
            }
            catch(Exception ex) { App.Error(ex); }
        }

        private void MercScoreBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                helper.SetMercScore(MercScoreBox);
            }
            catch(Exception ex) { App.Error(ex); }
        }

        private void MercKillsBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                helper.SetMercKills(MercKillsBox);
            }
            catch(Exception ex) { App.Error(ex); }
        }

        private void MercTimerBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                helper.ZeroMercTimer();
            }
            catch(Exception ex) { App.Error(ex); }
        }

        private void WeaponStatsBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                helper.SetWeaponStats(WeaponsStatsBox);
            }
            catch(Exception ex) { App.Error(ex); }
        }

        private void EnemiesKilledBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (App.activeConnection)
                {
                    helper.SetEnemiesKilled(EnemiesKilledBox);
                }
                else App.ConnectionError();
            }
            catch (Exception ex) { App.Error(ex); }
        }

        private void SkillPointsBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                helper.SetSkillPoints(SkillPointsBox);
            }
            catch (Exception ex) { App.Error(ex); }
        }

        private void MedalsBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                helper.ModMedals();
            }
            catch (Exception ex) { App.Error(ex); }
        }
    }
}
