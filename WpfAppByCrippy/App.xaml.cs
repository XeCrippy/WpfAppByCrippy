using JRPCPlusPlus;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using XDevkit;

namespace WpfAppByCrippy
{
    public partial class App : Application
    {
        public static IXboxConsole xb;
        public static IXboxManager xbm;
        public static XboxConsole xbdbg;
        public static bool activeConnection;
        public static bool dbgConnection;
        public static string MsgBody { get; set; }
        public static string MsgTitle { get; set; }

        public static bool Connected()
        {
            try
            {
                if (xb.Connect(out xb))
                {
                    activeConnection = true;
                    return true;
                }
                else
                {
                    activeConnection = false;
                    return false;
                }
            }
            catch 
            {
                activeConnection=false;
                return false;
            }
        }

        public static void ConnectionError()
        {
            MsgTitle = "Connection Error";
            MsgBody = "You are not connected to your console";
            var msgBox = new MsgBox.MsgBox();
            msgBox.ShowDialog();
            activeConnection = false;
            dbgConnection = false;
        }

        public static bool DbgConnect()
        {
            try
            {
                xbm = new XboxManager();
                xbdbg = xbm.OpenConsole(xbm.DefaultConsole);
                xbdbg.OpenConnection(null);
                dbgConnection = true;
                return true;
            }
            catch
            {
                dbgConnection = false;
                return false;
            }
        }

        public static void Error(Exception e)
        {
            MsgTitle = e.GetType().ToString();
            MsgBody = e.Message;
            var msgBox = new MsgBox.MsgBox();
            msgBox.ShowDialog();

            if (e is COMException)
            {
                dbgConnection = false;
                activeConnection = false;
                MsgTitle = "Connection Error";
                MsgBody = "You are not connected to your console";
            }
        }

        public static void XMessageBox(string title, string msg)
        {
            MsgTitle = title;
            MsgBody = msg;
            var msgBox = new MsgBox.MsgBox(); 
            msgBox.ShowDialog();
        }

        public static void ToggleBtn_off(ToggleButton toggleButton)
        {
            toggleButton.Background = new SolidColorBrush(Color.FromArgb(0xff, 0x20, 0x20, 0x20));
            toggleButton.BorderBrush = Brushes.Firebrick;
        }

        public static void ToggleBtn_on(ToggleButton toggleButton)
        {
            toggleButton.Background = Brushes.Firebrick;
            toggleButton.BorderBrush = Brushes.Firebrick;
        }
    }
}
