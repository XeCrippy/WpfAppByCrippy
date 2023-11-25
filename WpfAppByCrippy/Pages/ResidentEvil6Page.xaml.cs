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
            catch(Exception ex) 
            { 
                App.Error("Resident Evil 6 : God Mode", ex);
            }
        }

        private void AmmoBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                helper.InfiniteAmmo(AmmoBtn);
            }
            catch(Exception ex)
            { 
                App.Error("Resident Evil 6 : Infinite Ammo", ex); 
            }
        }

        private void StaminaBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                helper.InfiniteStamina(StaminaBtn);
            }
            catch(Exception ex) 
            { 
                App.Error("Resident Evil 6 : Infinite Stamina", ex);
            }
        }

        private void FreezeTimerBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                helper.FreezeMercTimer(FreezeTimerBtn);
            }
            catch(Exception ex)
            {
                App.Error("Resident Evil 6 : Freeze Mercenaries Timer", ex); 
            }
        }

        private void MercScoreBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                helper.SetMercScore(MercScoreBox);
            }
            catch(Exception ex) 
            { 
                App.Error("Resident Evil 6 : Mercenaries Score", ex); 
            }
        }

        private void MercKillsBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                helper.SetMercKills(MercKillsBox);
            }
            catch(Exception ex) 
            { 
                App.Error("Resident Evil 6 : Mercenaries Kills", ex);
            }
        }

        private void MercTimerBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                helper.ZeroMercTimer();
            }
            catch(Exception ex) 
            { 
                App.Error("Resident Evil 6 : Zero Mercenaries Timer", ex); 
            }
        }

        private void WeaponStatsBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                helper.SetWeaponStats(WeaponsStatsBox);
            }
            catch(Exception ex) 
            { 
                App.Error("Resident Evil 6 : Set Weapon Stats", ex);
            }
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
            catch (Exception ex)
            { 
                App.Error("Resident Evil 6 : Set Enemies Killed", ex); 
            }
        }

        private void SkillPointsBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                helper.SetSkillPoints(SkillPointsBox);
            }
            catch (Exception ex)
            { 
                App.Error("Resident Evil 6 : Set Skill Points", ex);
            }
        }

        private void MedalsBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                helper.ModMedals();
            }
            catch (Exception ex) 
            { 
                App.Error("Resident Evil 6 : Set Medals", ex); 
            }
        }
    }
}
