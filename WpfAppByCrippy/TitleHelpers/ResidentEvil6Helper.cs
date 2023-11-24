using JRPCPlusPlus;
using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace WpfAppByCrippy.TitleHelpers
{
    internal class ResidentEvil6Helper
    {
        private bool ammo;
        private bool god;
        private bool stamina;
        private bool timer;

        private uint ammoAddr = 0x830ADF30;
        private uint ammoOn = 0x39090000;
        private uint ammoOff = 0x7D044850;
        private uint enemiesEntry = 0x475913B4;
        private uint godmodeAddr = 0x82B81408;
        private uint godOn = 0x38E80000;
        private uint godOff = 0x7CE44050;
        private uint medalsEntryAgentHunt = 0x47592128;
        private uint medalsLengthAgentHunt = 0x32;
        private uint medalsEntryMercs = 0x4759217A;
        private uint medalsLengthMercs = 0x2C;
        private uint medalsEntryCampaigns = 0x47592042;
        private uint medalsLengthCampaigns = 0xC2;
        private uint mercHighComboTimer = 0x4758C27C;
        private uint mercHighCombo = 0x4758C274;
        private uint mercKills = 0x4758C26C;
        private uint mercScore = 0x4758C290;
        private uint mercTimer = 0x4758C878;
        private uint freezeMercTimer = 0x824E0A74;
        private uint mercTimerOn = 0x60000000;
        private uint mercTimerOff = 0xEC00F828;
        private uint staminaAddr = 0x82C15384;
        private uint staminaOn = 0x60000000;
        private uint staminaOff = 0xD11F3330;
        private uint weaponsEntry = 0x47590B1C;
        private int enemiesLength = 84;
        private int weaponsLength = 0x2B;

        public uint CampaignMedalsEntry(int index)
        {
            return medalsEntryCampaigns + (uint)(index * 1);
        }

        public uint MercMedalsEntry(int index)
        {
            return medalsEntryMercs + (uint)(index * 1);
        }

        public uint AgentHuntMedalsEntry(int index)
        {
            return medalsEntryAgentHunt + (uint)(index * 1);
        }

        public uint EnemyKillsAddr(int index)
        {
            return enemiesEntry + (uint)(index * 0x8);
        }

        public uint TimesDefeatedByEnemy(int index)
        {
            return (enemiesEntry + 0x4) + (uint)(index * 0x8);
        }

        public uint Headshots(int index)
        {
            return (weaponsEntry + 0xC) + (uint)(index * 0x2C);
        }

        public uint Reloads(int index)
        {
            return (weaponsEntry + 0x10) + (uint)(index * 0x2C);
        }

        public uint Quickshots(int index)
        {
            return (weaponsEntry + 0x14) + (uint)(index * 0x2C);
        }

        public uint WeaponsEntryAddr(int index)
        {
            return weaponsEntry + (uint)(index * 0x2C);
        }

        public uint TimesKilledEnemyWithWeapon(int index)
        {
            return (weaponsEntry + 0x4) + (uint)(index * 0x2C);
        }

        public bool FreezeMercTimer(ToggleButton toggleButton)
        {
            if (!App.activeConnection)
            {
                App.ConnectionError();
                ToggleTimerState(false, toggleButton);
            }
            else
            {
                ToggleTimerState(timer, toggleButton);
                timer = !timer;
            }

            return timer;
        }

        private void ToggleTimerState(bool isTimerOn, ToggleButton toggleButton)
        {
            App.xb.WriteUInt32(freezeMercTimer, isTimerOn ? mercTimerOff : mercTimerOn);
            App.ToggleButtonState(isTimerOn, toggleButton);
        }

        public bool GodMode(ToggleButton toggleButton)
        {
            if (App.activeConnection)
            {
                god = !god; // Toggle god mode
                App.xb.WriteUInt32(godmodeAddr, god ? godOn : godOff);
                App.ToggleButtonState(god, toggleButton);
            }
            else
            {
                god = false; // Ensure god mode is off if there's no active connection
                App.ConnectionError();
            }

            App.ToggleButtonState(god, toggleButton); // Update toggle button regardless of the connection status
            return god;
        }

        public bool InfiniteAmmo(ToggleButton toggleButton)
        {
            if (App.activeConnection)
            {
                ammo = !ammo; // Toggle ammo state
                App.xb.WriteUInt32(ammoAddr, ammo ? ammoOn : ammoOff);
            }
            else
            {
                ammo = false; // Ensure ammo is off if there's no active connection
                App.ConnectionError();
            }

            App.ToggleButtonState(ammo, toggleButton);
            return ammo;
        }

        public bool InfiniteStamina(ToggleButton toggleButton)
        {
            if (App.activeConnection)
            {
                stamina = !stamina; // Toggle stamina state
                App.xb.WriteUInt32(staminaAddr, stamina ? staminaOn : staminaOff);
            }
            else
            {
                stamina = false; // Ensure stamina is off if there's no active connection
                App.ConnectionError();
            }

            App.ToggleButtonState(stamina, toggleButton);
            return stamina;
        }

        public void ModMedals()
        {
            if (App.activeConnection)
            {
                ModMedalEntries(AgentHuntMedalsEntry, (int)medalsLengthAgentHunt);
                ModMedalEntries(CampaignMedalsEntry, (int)medalsLengthCampaigns);
                ModMedalEntries(MercMedalsEntry, (int)medalsLengthMercs);
            }
            else
            {
                App.ConnectionError();
            }
        }

        private static void ModMedalEntries(Func<int, uint> getMedalEntry, int medalsLength)
        {
            for (int i = 0; i < medalsLength; ++i)
            {
                App.xb.WriteByte(getMedalEntry(i), 0xFF);
            }
        }

        public void SetEnemiesKilled(TextBox enemiesKilledBox)
        {
            if (App.activeConnection)
            {
                if (uint.TryParse(enemiesKilledBox.Text, out uint value))
                {
                    for (int i = 0; i < enemiesLength; ++i)
                    {
                        App.xb.WriteUInt32(EnemyKillsAddr(i), value);
                    }
                }
                else
                {
                    // Handle parsing error, e.g., display an error message to the user.
                    App.XMessageBox("Invalid Input", "The value you entered is not a valid 32 bit unsigned integer");
                }
            }
            else App.ConnectionError();
        }

        public void SetMercKills(TextBox killsBox)
        {
            if (App.activeConnection)
            {
                if (uint.TryParse(killsBox.Text, out uint value))
                {
                    App.xb.WriteUInt32(mercKills, value);
                }
                else
                {
                    // Handle parsing error, e.g., display an error message to the user.
                    App.XMessageBox("Invalid Input", "The value you entered is not a valid 32 bit unsigned integer");
                }
            }
            else App.ConnectionError();
        }

        public void SetMercScore(TextBox scoreBox)
        {
            if (App.activeConnection)
            {
                if (uint.TryParse(scoreBox.Text, out uint value))
                {
                    App.xb.WriteUInt32(mercScore, value);
                }
                else
                {
                    // Handle parsing error, e.g., display an error message to the user.
                    App.XMessageBox("Invalid Input", "The value you entered is not a valid 32 bit unsigned integer");
                }
            }
            else
            {
                App.ConnectionError();
            }
        }

        public void SetSkillPoints(TextBox skillPointsBox)
        {
            if (App.activeConnection)
            {
                if (uint.TryParse(skillPointsBox.Text, out uint value))
                {
                    App.xb.CallVoid(0x82AFAB80, 0x47590060, value);
                }
                else
                {
                    // Handle parsing error, e.g., display an error message to the user.
                    App.XMessageBox("Invalid Input", "The value you entered is not a valid 32 bit unsigned integer");
                }
            }
            else
            {
                App.ConnectionError();
            }
        }

        public void SetWeaponStats(TextBox weaponStatsBox)
        {
            if (App.activeConnection)
            {
                if (uint.TryParse(weaponStatsBox.Text, out uint value))
                {
                    for (int i = 0; i < weaponsLength; i++)
                    {
                        SetWeaponStat(WeaponsEntryAddr(i), value);
                        SetWeaponStat(TimesKilledEnemyWithWeapon(i), value);
                        SetWeaponStat(Reloads(i), value);
                        SetWeaponStat(Quickshots(i), value);
                        SetWeaponStat(Headshots(i), value);
                    }
                }
                else
                {
                    // Handle invalid input, e.g., display an error message.
                    App.XMessageBox("Invalid Input", "The value you entered is not a valid 32 bit unsigned integer");
                }
            }
            else
            {
                App.ConnectionError();
            }
        }

        private void SetWeaponStat(uint address, uint value)
        {
            App.xb.WriteUInt32(address, value);
        }

        public void ZeroMercTimer()
        {
            // Sets the Mercenaries timer to 1 second instantly ending the match
            if (App.activeConnection)
            {
                App.xb.WriteFloat(mercTimer, 1.0f);
            }
            else App.ConnectionError();
        }
    }
}
