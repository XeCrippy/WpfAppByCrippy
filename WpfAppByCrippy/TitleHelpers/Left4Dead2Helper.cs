using JRPCPlusPlus;
using System.Windows.Controls.Primitives;

namespace WpfAppByCrippy.TitleHelpers
{
    internal class Left4Dead2Helper
    {
        private bool fpsCounter = false;
        private bool godMode = false;
        private bool graphs = false;
        private bool infAmmo = false;
        private bool playerPos = false;
        private bool svCheats = false;

        /// <summary>
        /// Sends console commands
        /// </summary>
        /// <param name="command">Ex: "cl_showfps 1"</param>
        public static void Cbuf_AddText(string command)
        {
            App.xb.CallVoid(0x86BCDDB0, 0, command, 0);
        }

        private bool ToggleCvarState(bool currentState, ToggleButton toggleButton, string command, string enabledMessage, string disabledMessage)
        {
            if (App.activeConnection)
            {
                Cbuf_AddText($"{command} {(currentState ? "0" : "1")};say {(currentState ? disabledMessage : enabledMessage)}");
                currentState = !currentState;
                App.ToggleButtonState(currentState, toggleButton);
            }
            else
            {
                App.ConnectionError();
                currentState = false;
                App.ToggleButtonState(currentState, toggleButton);
            }

            return currentState;
        }

        public bool FpsCounter(ToggleButton toggleButton)
        {
            const string enableFpsCounterCommand = "cl_showfps 4;say FPS Counter Enabled";
            const string disableFpsCounterCommand = "cl_showfps 0;say FPS Counter Disabled";

            if (App.activeConnection)
            {
                Cbuf_AddText(fpsCounter ? disableFpsCounterCommand : enableFpsCounterCommand);
                fpsCounter = !fpsCounter;
                App.ToggleButtonState(fpsCounter, toggleButton);
            }
            else
            {
                App.ConnectionError();
                fpsCounter = false;
                App.ToggleButtonState(fpsCounter, toggleButton);
            }
            return fpsCounter;
        }

        public bool GodMode(ToggleButton toggleButton)
        {
            godMode = ToggleCvarState(godMode, toggleButton, "god", "God Mode Enabled", "God Mode Disabled");
            return godMode;
        }

        public bool InfiniteAmmo(ToggleButton toggleButton)
        {
            infAmmo = ToggleCvarState(infAmmo, toggleButton, "sv_infinite_ammo", "Infinite Ammo Enabled", "Infinite Ammo Disabled");
            return infAmmo;
        }

        public static void NoClip()
        {
            if (App.activeConnection)
                Cbuf_AddText("noclip;say Toggled No Clip");
            else App.ConnectionError();
        }

        public bool PlayerPosition(ToggleButton toggleButton)
        {
            playerPos = ToggleCvarState(playerPos, toggleButton, "cl_showpos", "Show Position Enabled", "Show Position Disabled");
            return playerPos;
        }

        public bool ShowGraphs(ToggleButton toggleButton)
        {
            string enableGraphsCommand = "+graph;say Show Performance Graphs Enabled";
            string disableGraphscommand = "-graph;say Show Performance Graphs Disabled";

            if (App.activeConnection)
            {
                Cbuf_AddText(graphs ? disableGraphscommand : enableGraphsCommand);
                graphs = !graphs;
                App.ToggleButtonState(graphs, toggleButton);
            }
            else
            {
                App.ConnectionError();
                graphs = false;
                App.ToggleButtonState(graphs, toggleButton);
            }
            return graphs;
        }

        public bool SV_Cheats(ToggleButton toggleButton)
        {
            svCheats = ToggleCvarState(svCheats, toggleButton, "sv_cheats", "SV Cheats Enabled", "SV Cheats Disabled");
            return svCheats;
        }
    }
}
