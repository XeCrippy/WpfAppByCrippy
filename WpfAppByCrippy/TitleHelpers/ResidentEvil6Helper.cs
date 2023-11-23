using JRPCPlusPlus;
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
            if (!timer && App.activeConnection)
            {
                App.xb.WriteUInt32(freezeMercTimer, mercTimerOn);
                App.ToggleBtn_on(toggleButton);
                timer = true;
            }
            else if (!App.activeConnection)
            {
                App.ConnectionError();
                App.ToggleBtn_off(toggleButton);
                timer = false;
            }
            else
            {
                App.xb.WriteUInt32(freezeMercTimer, mercTimerOff);
                App.ToggleBtn_off(toggleButton);
                timer = false;
            }
            return timer;
        }

        public bool GodMode(ToggleButton toggleButton)
        {
            if (!god && App.activeConnection)
            {
                App.xb.WriteUInt32(godmodeAddr, godOn);
                god = true;
                App.ToggleBtn_on(toggleButton);
            }
            else if (!App.activeConnection)
            {
                App.ConnectionError();
                App.ToggleBtn_off(toggleButton);
                god = false;
            }
            else
            {
                App.xb.WriteUInt32(godmodeAddr, godOff);
                god = false;
                App.ToggleBtn_off(toggleButton);
            }
            return god;
        }

        public bool InfiniteAmmo(ToggleButton toggleButton)
        {
            if (!ammo && App.activeConnection)
            {
                App.xb.WriteUInt32(ammoAddr, ammoOn); 
                ammo = true;
                App.ToggleBtn_on(toggleButton);
            }
            else if (!App.activeConnection)
            {
                App.ConnectionError();
                ammo = false;
                App.ToggleBtn_off(toggleButton);
            }
            else
            {
                App.xb.WriteUInt32(ammoAddr, ammoOff);
                ammo = false;
                App.ToggleBtn_off(toggleButton);
            }
            return ammo;
        }

        public bool InfiniteStamina(ToggleButton toggleButton)
        {
            if (!stamina && App.activeConnection)
            {
                App.xb.WriteUInt32(staminaAddr, staminaOn);
                stamina = true;
                App.ToggleBtn_on(toggleButton);
            }
            else if (!App.activeConnection)
            {
                App.ConnectionError();
                stamina = false;
                App.ToggleBtn_off(toggleButton);
            }
            else
            {
                App.xb.WriteUInt32(staminaAddr, staminaOff);
                stamina = false;
                App.ToggleBtn_off(toggleButton);
            }
            return stamina;
        }

        public void ModMedals()
        {
            if (App.activeConnection)
            {
                for (int i = 0; i < medalsLengthAgentHunt; ++i)
                {
                    App.xb.WriteByte(AgentHuntMedalsEntry(i), 0xFF);
                }
                for (int i = 0; i <medalsLengthCampaigns; ++i)
                {
                    App.xb.WriteByte(CampaignMedalsEntry(i), 0xFF);
                }
                for (int i = 0; i <medalsLengthMercs; ++i)
                {
                    App.xb.WriteByte(MercMedalsEntry(i), 0xFF);
                }
            }
            else App.ConnectionError();
        }

        public void SetEnemiesKilled(TextBox enemiesKilledBox)
        {
            if (App.activeConnection)
            {
                for (int i = 0; i < enemiesLength; ++i)
                {
                    App.xb.WriteUInt32(EnemyKillsAddr(i), uint.Parse(enemiesKilledBox.Text));
                }
            }
            else App.ConnectionError();
        }

        public void SetMercKills(TextBox killsBox)
        {
            if (App.activeConnection)
            {
                App.xb.WriteUInt32(mercKills, uint.Parse(killsBox.Text));
            }
            else App.ConnectionError();
        }

        public void SetMercScore(TextBox scoreBox)
        {
            if (App.activeConnection)
            {
                App.xb.WriteUInt32(mercScore, uint.Parse(scoreBox.Text));
            }
            else App.ConnectionError();
        }

        public void SetSkillPoints(TextBox skillPointsBox)
        {
            if (App.activeConnection)
            {
                uint value = uint.Parse(skillPointsBox.Text);
                App.xb.CallVoid(0x82AFAB80, 0x47590060, value);
            }
            else App.ConnectionError();
        }

        public void SetWeaponStats(TextBox weaponStatsBox)
        {
            if (App.activeConnection)
            {
                uint value = uint.Parse(weaponStatsBox.Text);
                for (int i = 0; i < weaponsLength; i++)
                {
                    App.xb.WriteUInt32(WeaponsEntryAddr(i), value);
                    App.xb.WriteUInt32(TimesKilledEnemyWithWeapon(i), value);
                    App.xb.WriteUInt32(Reloads(i), value);
                    App.xb.WriteUInt32(Quickshots(i), value);
                    App.xb.WriteUInt32(Headshots(i), value);
                }
            }
            else App.ConnectionError();
        }

        public void ZeroMercTimer()
        {
            if (App.activeConnection)
            {
                App.xb.WriteFloat(mercTimer, 1.0f);
            }
            else App.ConnectionError();
        }
    }
}
