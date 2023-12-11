using JRPCPlusPlus;
using System;
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

        // Constants for Xbox memory addresses
        private uint cxconsolePtr = 0x83AC6E58;
        const uint executeStringInternal = 0x822D5B68;

        /// <summary>
        /// Sends console commands to Xbox.
        /// </summary>
        /// <param name="cmd">Example: "cl_fov 55"</param>
        /// <param name="fromConsole">Indicates whether the command originates from the console.</param>
        /// <param name="silentMode">Indicates whether to execute the command in silent mode.</param>
        public void ExecuteStringInternal(string cmd, bool fromConsole=true, bool silentMode=false)
        {
            // Retrieve the console pointer from Xbox memory
            uint arg1 = App.xb.ReadUInt32(cxconsolePtr);

            // Call the Xbox method to execute the command
            App.xb.CallVoid(executeStringInternal, arg1, cmd, fromConsole, silentMode);
        }

        /// <summary>
        /// Stronger aim assist. This could probably use some work
        /// </summary>
        /// <param name="toggleButton">Name of the ToggleButton being used</param>
        /// <returns>Aim Assist toggle state</returns>
        public bool AimAssist(ToggleButton toggleButton)
        {
            const int shortDelayMilliseconds = 250;
            const string aimAssistOnCommands = "aim_assistFalloffDistance 9999;aim_assistGlidingMultiplier 99;aim_assistMaxDistance 9999;aim_assistMaxDistanceTagged 9999";
            const string aimAssistOffCommands = "aim_assistFalloffDistance 50;aim_assistGlidingMultiplier 2;aim_assistMaxDistance 50;aim_assistMaxDistanceTagged 50";

            try
            {
                if (App.activeConnection)
                {
                    string commands;
                    if (!aimAssist)
                    {
                        commands = aimAssistOnCommands;
                        aimAssist = true;
                    }
                    else
                    {
                        commands = aimAssistOffCommands;
                        aimAssist = false;
                    }

                    // Configure basic aim assist settings
                    ConfigureAimAssist(commands);
                    Thread.Sleep(shortDelayMilliseconds);

                    // Further configure aim assist for IronSight
                    ConfigureAimAssist("aim_assistMaxDistance_IronSight 9999;aim_assistMinDistance 0;aim_assistSnapRadiusScale 30;aim_assistSnapRadiusTaggedScale 30;aim_assistStrength 99;aim_assistStrength_IronSight 99");

                    // Toggle the button state based on aimAssist
                    App.ToggleButtonState(aimAssist, toggleButton);
                }
                else
                {
                    // Handle connection error
                    App.ConnectionError();
                    aimAssist = false;
                    App.ToggleButtonState(aimAssist, toggleButton);
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception using App.Error
                App.Error("Crysis 2 : Aim Assist", ex);
            }

            return aimAssist;
        }



        private void ConfigureAimAssist(string commands)
        {
            ExecuteStringInternal(commands);
        }

        private void ApplyDemigodSettings(uint thresholdTimeAddress, uint regenerationRateAddress, ToggleButton toggleButton)
        {
            // Xbox memory addresses
            uint plHealthNormalThresholdTimeToRegenerateSP = App.xb.ReadUInt32(thresholdTimeAddress);
            uint plHealthNormalRegenerationRateSP = App.xb.ReadUInt32(regenerationRateAddress);

            
            float MaxRegenerationRate = 9999;
            float MinThresholdTimeToRegenerate = 0;
            float DefaultRegenerationRate = 2;
            float DefaultThresholdTimeToRegenerate = 15.0f;

            if (App.activeConnection)
            {
                if (!godMode)
                {
                    // Apply settings for god mode
                    App.xb.WriteFloat(plHealthNormalRegenerationRateSP, MaxRegenerationRate);
                    App.xb.WriteFloat(plHealthNormalThresholdTimeToRegenerateSP, MinThresholdTimeToRegenerate);
                    godMode = true;
                    App.ToggleButtonState(true, toggleButton);
                }
                else
                {
                    // Apply default settings
                    App.xb.WriteFloat(plHealthNormalRegenerationRateSP, DefaultRegenerationRate);
                    App.xb.WriteFloat(plHealthNormalThresholdTimeToRegenerateSP, DefaultThresholdTimeToRegenerate);
                    godMode = false;
                    App.ToggleButtonState(false, toggleButton);
                }
            }
            else
            {
                App.ConnectionError();
                godMode = false;
                App.ToggleButtonState(false, toggleButton);
            }
        }

        /// <summary>
        /// This is not perfect and could use some work. Can still die while in a vehicle and possibly big enough explosions
        /// </summary>
        /// <param name="toggleButton">Name of the ToggleButton being used</param>
        /// <returns>Demigod toggle state</returns>
        public bool Demigod(ToggleButton toggleButton)
        {
            const uint plHealthNormalThresholdTimeToRegenerateSPAddress = 0x403CC138;
            const uint plHealthNormalRegenerationRateSPAddress = 0x403CC170;

            // Xbox memory related logic
            ApplyDemigodSettings(plHealthNormalThresholdTimeToRegenerateSPAddress, plHealthNormalRegenerationRateSPAddress, toggleButton);

            return godMode;
        }

        /// <summary>
        /// Infinite Ammunition
        /// </summary>
        /// <param name="toggleButton">Name of the ToggleButton being used</param>
        /// <returns>InfiniteAmmo toggle state</returns>
        public bool InfiniteAmmo(ToggleButton toggleButton)
        {
            if (App.activeConnection)
            {
                infAmmo = !infAmmo; // Toggle ammo state
                App.xb.WriteUInt32(ammo, infAmmo ? nop : ammo_off); // Use infAmmo state for the condition
            }
            else
            {
                infAmmo = false; // Ensure ammo is off if there's no active connection
                App.ConnectionError();
            }

            App.ToggleButtonState(infAmmo, toggleButton); // Use infAmmo state for ToggleBtn
            return infAmmo;
        }
    }
}
