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

        public bool FpsCounter(ToggleButton toggleButton)
        {
            if (!fpsCounter && App.activeConnection) 
            {
                Cbuf_AddText("cl_showfps 4;say FPS Counter Enabled");
                fpsCounter = true;
                App.ToggleBtn_on(toggleButton);
            }
            else if (!App.activeConnection)
            {
                App.ConnectionError();
                fpsCounter = false;
                App.ToggleBtn_off(toggleButton);
            }
            else
            {
                Cbuf_AddText("cl_showfps 0;say FPS Counter Disabled");
                fpsCounter = false;
                App.ToggleBtn_off(toggleButton);
            }
            return fpsCounter;
        }

        public bool GodMode(ToggleButton toggleButton)
        {
            if (!godMode && App.activeConnection)
            {
                Cbuf_AddText("god 1;say God Mode Enabled");
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
                Cbuf_AddText("god 0;say God Mode Disabled");
                godMode = false;
                App.ToggleBtn_off(toggleButton);
            }
            return godMode;
        }

        public bool InfiniteAmmo(ToggleButton toggleButton)
        {
            if(!infAmmo && App.activeConnection)
            {
                Cbuf_AddText("sv_infinite_ammo 1;say Infinite Ammo Enabled");
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
                Cbuf_AddText("sv_infinite_ammo 0;say Infinite Ammo Disabled");
                infAmmo = false;
                App.ToggleBtn_off(toggleButton);
            }
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
            if (!playerPos && App.activeConnection)
            {
                Cbuf_AddText("cl_showpos 1;say Show Position Enabled");
                playerPos = true;
                App.ToggleBtn_on(toggleButton);
            }
            else if (!App.activeConnection)
            {
                App.ConnectionError();
                playerPos = false;
                App.ToggleBtn_off(toggleButton);
            }
            else
            {
                Cbuf_AddText("cl_showpos 0;say Show Position Disabled");
                playerPos = false;
                App.ToggleBtn_off(toggleButton);
            }
            return playerPos;
        }

        public bool ShowGraphs(ToggleButton toggleButton)
        {
            if (!graphs && App.activeConnection)
            {
                Cbuf_AddText("+graph;say Show Graphs Enabled");
                graphs = true;
                App.ToggleBtn_on(toggleButton);
            }
            else if (!App.activeConnection)
            {
                App.ConnectionError();
                graphs = false;
                App.ToggleBtn_off(toggleButton);
            }
            else
            {
                Cbuf_AddText("-graph;say Show Graphs Disabled");
                graphs = false;
                App.ToggleBtn_off(toggleButton);
            }
            return graphs;
        }

        public bool SV_Cheats(ToggleButton toggleButton)
        {
            if (!svCheats&& App.activeConnection)
            {
                Cbuf_AddText("sv_cheats 1;say SV Cheats Enabled");
                svCheats = true;
                App.ToggleBtn_on(toggleButton);
            }
            else if (!App.activeConnection)
            {
                App.ConnectionError();
                svCheats = false;
                App.ToggleBtn_off(toggleButton);
            }
            else
            {
                Cbuf_AddText("sv_cheats 0;say SV Cheats Disabled");
                svCheats = false;
                App.ToggleBtn_off(toggleButton);
            }
            return svCheats;
        }
    }
}
