using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using JRPCPlusPlus;
using XDevkit;

namespace WpfAppByCrippy.TitleHelpers
{
    internal class Crackdown2Helper
    {
        // original function for console commands
        /*void* __fastcall sub_82771EB0(_BYTE* a1, int a2, __int64 a3, __int64 a4)
        {
            unsigned int v4; // r11
            char* v5; // r10
            char v6; // r9
            __int64 v7; // r10
            char* v8; // r11
            void* result; // r3
            char v10; // [sp+4Eh] [-212h] BYREF
            unsigned __int16 v11[260]; // [sp+50h] [-210h] BYREF

            HIDWORD(a3) = 0;
            v4 = 0;
            if (*a1)
            {
                v5 = &v10;
                do
                {
                    if (v4 >= 0xFF)
                        break;
                    v6 = a1[v4++];
                    v5 += 2;
                    *(_WORD*)v5 = v6;
                    LODWORD(a3) = (unsigned __int8)a1[v4];
                }
                while (a1[v4]);
            }
            LODWORD(v7) = &unk_83800000;
            HIDWORD(v7) = 2 * v4;
            LODWORD(a4) = v11;
            v8 = (char*)off_83800F88;
            *(unsigned __int16 *)((char*)v11 + HIDWORD(v7)) = 0;
            if (v8)
                HIDWORD(a3) = v8 + 1352;
            result = off_83800AF4;
            if (off_83800AF4)
                result = (void*)sub_825CE628((int)off_83800AF4, v11, a3, a4, v7);
            return result;
        }*/

        private static bool debugInfo;
       private static bool drawOutlines;
        private static bool perfGraphs;
        private static bool redFpsText;

        private static uint enableafterlights = 0x830A0545;
        private static uint enableoutlines = 0x830A0544;
        private static uint enableshadows = 0x830A054F;
        private static uint gputiming = 0x83801A00;
        private static uint hudopacity = 0x83092608;
        private static uint outlinemode = 0x83801164; // dword
        private static uint perfgraphs = 0x83801A01;
        private static uint redDebugText = 0x83800AEF;
        private static uint showfps = 0x83801B0C; // dword
        private static uint togglebloom = 0x830A054E;

        public static void  Apocolypse()
        {
            try
            {
                if (App.activeConnection)
                    App.xb.CallVoid(0x82719790);
                else App.ConnectionError();
            }
            catch(Exception ex)
            {
                App.Error("Apocalypse", ex);
            }
        }

        public static void ConsoleCommand(string command)
        {
            try
            {
                if (App.activeConnection)
                    App.xb.CallVoid(0x82771EB0, command); // looks like there are supposed to be 4 arguments total
                else App.ConnectionError();
            }
            catch(Exception ex)
            {
                App.Error("Console Command", ex);
            }
        }

        public static bool DebugInfo(ToggleButton toggleButton)
        {
            try
            {
                if (App.activeConnection)
                {
                    if (!debugInfo)
                    {
                        App.xb.WriteUInt32(showfps, 0x1);
                        App.xb.WriteByte(gputiming, 0x1);
                        debugInfo = true;
                        App.ToggleButtonState(debugInfo, toggleButton);
                    }
                    else
                    {
                        App.xb.WriteUInt32(showfps, 0x0);
                        App.xb.WriteByte(gputiming, 0x0);
                        debugInfo = false;
                        App.ToggleButtonState(debugInfo, toggleButton);
                    }
                }
                else App.ConnectionError();
                return debugInfo;
            }
            catch (Exception ex)
            {
                App.Error("Debug Info", ex);
                return debugInfo;
            }
        }

        public static bool DrawOutlines(ToggleButton togglebutton)
        {
            try
            {
                if (App.activeConnection)
                {
                    if (!drawOutlines)
                    {
                        App.xb.WriteUInt32(outlinemode, 0x2);
                        drawOutlines = true;
                        App.ToggleButtonState(drawOutlines, togglebutton);
                    }
                    else
                    {
                        App.xb.WriteUInt32(outlinemode, 0x0);
                        drawOutlines = false;
                        App.ToggleButtonState(drawOutlines, togglebutton);
                    }
                }
                else App.ConnectionError();
                return drawOutlines;
            }
            catch(Exception ex)
            {
                App.Error("Draw Outlines", ex);
                return drawOutlines;
            }
        }

        public static bool PerformanceGraphs(ToggleButton toggleButton)
        {
            try
            {
                if (App.activeConnection)
                {
                    if (!perfGraphs)
                    {
                        App.xb.WriteByte(perfgraphs, 0x1);
                        perfGraphs = true;
                        App.ToggleButtonState(perfGraphs, toggleButton);
                    }
                    else
                    {
                        App.xb.WriteByte(perfgraphs, 0x0);
                        perfGraphs = false;
                        App.ToggleButtonState(perfGraphs, toggleButton);
                    }
                }
                else App.ConnectionError();
                return perfGraphs;
            }
            catch (Exception ex)
            {
                App.Error("Draw Outlines", ex);
                return perfGraphs;
            }
        }

        public static bool RedFpsText(ToggleButton toggleButton)
        {
            try
            {
                if (App.activeConnection)
                {
                    if (!redFpsText)
                    {
                        App.xb.WriteByte(redDebugText, 0x1);
                        redFpsText = true;
                        App.ToggleButtonState(redFpsText, toggleButton);
                    }
                    else
                    {
                        App.xb.WriteByte(redDebugText, 0x0);
                        redFpsText = false;
                        App.ToggleButtonState(redFpsText, toggleButton);
                    }
                }
                else App.ConnectionError();
                return redFpsText;
            }
            catch (Exception ex)
            {
                App.Error("Draw Outlines", ex);
                return redFpsText;
            }
        }

        public static void ToggleTargetLag()
        {
            try
            {
                if (App.activeConnection)
                    App.xb.CallVoid(0x823AA290);
                else App.ConnectionError();
            }
            catch (Exception ex)
            {
                App.Error("Target Lag", ex);
            }
        }

        public static void ToggleFlyMode()
        {
            ConsoleCommand("fly");
        }

        public static void ToggleGodMode()
        {
            try
            {
                if (App.activeConnection)
                    ConsoleCommand("god");
                else App.ConnectionError();
            }
            catch (Exception ex)
            {
                App.Error("God Mode", ex);
            }
        }

        public static void ToggleInfinteAmmo()
        {
            try
            {
                if (App.activeConnection)
                    ConsoleCommand("infiniteammo");
                else App.ConnectionError();
            }
            catch (Exception ex)
            {
                App.Error("Infinite Ammo", ex);
            }
        }

        public static void ToggleStickyTargeting()
        {
            try
            {
                if (App.activeConnection)
                    App.xb.CallVoid(0x82534858);
                else App.ConnectionError();
            }
            catch (Exception ex)
            {
                App.Error("Sticky Targeting", ex);
            }
        }
    }
}
