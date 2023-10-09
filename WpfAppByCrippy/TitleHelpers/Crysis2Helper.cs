using JRPCPlusPlus;
using System.Threading;
using System.Windows.Controls.Primitives;

namespace WpfAppByCrippy.TitleHelpers
{
    internal class Crysis2Helper
    {
        private bool aimAssist;
        private bool godMode;
        private bool infAmmo;

        private readonly uint ammo = 0x82FFB5B8;
        private readonly uint nop = 0x60000000;
        private readonly uint ammo_off = 0x90AB0004;

        public void ExecuteStringInternal(string cmd)
        {
            uint ptr = App.xb.ReadUInt32(0x83AC6E58);
            App.xb.CallVoid(0x822D5B68, ptr, cmd, 0, 0);
        }

        public bool AimAssist(ToggleButton toggleButton)
        {
            if (!aimAssist && App.activeConnection)
            {
                ExecuteStringInternal("aim_assistFalloffDistance 9999;aim_assistGlidingMultiplier 99;aim_assistMaxDistance 9999;aim_assistMaxDistanceTagged 9999");
                Thread.Sleep(500);
                ExecuteStringInternal("aim_assistMaxDistance_IronSight 9999;aim_assistMinDistance 0;aim_assistSnapRadiusScale 30;aim_assistSnapRadiusTaggedScale 30;aim_assistStrength 99;aim_assistStrength_IronSight 99");
                aimAssist = true;
                App.ToggleBtn_on(toggleButton);
            }
            else if (!App.activeConnection)
            {
                App.ConnectionError();
                aimAssist = false;
                App.ToggleBtn_off(toggleButton);
            }
            else
            {
                ExecuteStringInternal("aim_assistFalloffDistance 50;aim_assistGlidingMultiplier 2;aim_assistMaxDistance 50;aim_assistMaxDistanceTagged 50");
                Thread.Sleep(500);
                ExecuteStringInternal("aim_assistMaxDistance_IronSight 50;aim_assistMinDistance 0;aim_assistSnapRadiusScale 1;aim_assistSnapRadiusTaggedScale 1;aim_assistStrength 0.5;aim_assistStrength_IronSight 0.5");
                aimAssist = false;
                App.ToggleBtn_off(toggleButton);
            }
            return aimAssist;
        }

        public bool Demigod(ToggleButton toggleButton)
        {
            uint pl_health_normal_threshold_time_to_regenerateSP = App.xb.ReadUInt32(0x403CC138);
            uint pl_health_normal_regeneration_rateSP = App.xb.ReadUInt32(0x403CC170);

            if (!godMode && App.activeConnection)
            {
                App.xb.WriteFloat(pl_health_normal_regeneration_rateSP, 9999);
                App.xb.WriteFloat(pl_health_normal_threshold_time_to_regenerateSP, 0);
                godMode = true;
                App.ToggleBtn_on(toggleButton);
            }

            else if (!App.activeConnection)
            {
                App.ConnectionError();
                godMode = false;
                App.ToggleBtn_off(toggleButton);
            }

            else
            {
                App.xb.WriteUInt32(pl_health_normal_regeneration_rateSP, 2);
                App.xb.WriteFloat(pl_health_normal_threshold_time_to_regenerateSP, 15.0f);
                godMode = false;
                App.ToggleBtn_off(toggleButton);
            }
            return godMode;
        }

        public bool InfiniteAmmo(ToggleButton toggleButton)
        {
            if (!infAmmo && App.activeConnection)
            {
                App.xb.WriteUInt32(ammo, nop);
                infAmmo = true;
                App.ToggleBtn_on(toggleButton);
            }

            else if (!App.activeConnection)
            {
                App.ConnectionError();
                infAmmo = false;
                App.ToggleBtn_off(toggleButton);
            }

            else
            {
                App.xb.WriteUInt32(ammo, ammo_off);
                infAmmo = false;
                App.ToggleBtn_off(toggleButton);
            }
            return infAmmo;
        }
    }
}
