using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using JRPCPlusPlus;
using XDevkit;

namespace WpfAppByCrippy
{
    public partial class App : Application
    {
        public static IXboxConsole xb;
        public static bool activeConnection = false;

        public static string msgBody { get; set; }
        public static string msgTitle { get; set; }

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
            msgTitle = "Connection Error";
            msgBody = "You are not connected to your console";
            activeConnection=false;
            var msgBox = new MsgBox.MsgBox();
            msgBox.ShowDialog();
        }

        public static void Error(Exception e)
        {
            msgTitle = e.GetType().ToString();
            msgBody = e.Message;
            var msgBox = new MsgBox.MsgBox();
            msgBox.ShowDialog();
        }

        public static void xMessageBox(string title, string msg)
        {
            msgTitle = title;
            msgBody = msg;
            var msgBox = new MsgBox.MsgBox(); 
            msgBox.ShowDialog();
        }

        public static bool state;

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
