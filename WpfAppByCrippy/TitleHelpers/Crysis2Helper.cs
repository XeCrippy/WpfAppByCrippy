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

        /// <summary>
        /// Sends console commands
        /// </summary>
        /// <param name="cmd">Ex: "cl_fov 55"</param>
        public void ExecuteStringInternal(string cmd, bool fromConsole=false, bool silentMode=true)
        {
            uint arg1 = App.xb.ReadUInt32(0x83AC6E58);
            App.xb.CallVoid(0x822D5B68, arg1, cmd, fromConsole, silentMode);
        }

        /// <summary>
        /// Stronger aim assist. This could probably use some work
        /// </summary>
        /// <param name="toggleButton">Name of the ToggleButton being used</param>
        /// <returns>Aim Assist toggle state</returns>
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

        /// <summary>
        /// This is not perfect and could use some work. Can still die while in a vehicle and possibly big enough explosions
        /// </summary>
        /// <param name="toggleButton">Name of the ToggleButton being used</param>
        /// <returns>Demigod toggle state</returns>
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

        /// <summary>
        /// Infinite Ammunition
        /// </summary>
        /// <param name="toggleButton">Name of the ToggleButton being used</param>
        /// <returns>InfiniteAmmo toggle state</returns>
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
