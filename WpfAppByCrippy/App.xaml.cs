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
                IXboxConsole newConnection = xb;
                if (newConnection.Connect(out newConnection))
                {
                    activeConnection = true;
                    xb = newConnection;  // Update the xb reference
                    return true;
                }
                else
                {
                    activeConnection = false;
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                Error(ex);
                activeConnection = false;
                return false;
            }
        }

        /// <summary>
        /// Provides a generic connection error anytime a function returns activeConnection = false
        /// </summary>
        public static void ConnectionError()
        {
            MsgTitle = "Connection Error";
            MsgBody = "You are not connected to your console";
            var msgBox = new MsgBox.MsgBox();
            msgBox.ShowDialog();
            activeConnection = false;
            dbgConnection = false;
        }

        /// <summary>
        /// Connection for debugger. Only necessary for features like the one hit kill/god mode for Mortal Kombat
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Provides a custom message box with the error exception message. 
        /// *Note: the e.Message can be changed to e.ToString() to see the entire exception 
        /// </summary>
        /// <param name="e">Ex: e.Message | e.ToString()</param>
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
            }
        }

        /// <summary>
        /// Provides a custom message box for errors or other messages. Uses the same design theme as the main program.
        /// </summary>
        /// <param name="title">Title for the message box</param>
        /// <param name="msg">Custom message for the body of the message box</param>
        public static void XMessageBox(string title, string msg)
        {
            MsgTitle = title;
            MsgBody = msg;
            var msgBox = new MsgBox.MsgBox(); 
            msgBox.ShowDialog();
        }

        public static void ToggleButtonState(bool state, ToggleButton toggleButton)
        {
            if (state)
            {
                toggleButton.Background = Brushes.Firebrick;
                toggleButton.BorderBrush = Brushes.Firebrick;
            }
            else
            {
                toggleButton.Background = new SolidColorBrush(Color.FromArgb(0xff, 0x20, 0x20, 0x20));
                toggleButton.BorderBrush = Brushes.Firebrick;
            }
        }

        /// <summary>
        /// Fixes toggle button background color when deactivated. This broke in the xaml somewhere along the line
        /// </summary>
        /// <param name="toggleButton">Name of the ToggleButton being used</param>
        public static void ToggleBtn_off(ToggleButton toggleButton)
        {
            toggleButton.Background = new SolidColorBrush(Color.FromArgb(0xff, 0x20, 0x20, 0x20));
            toggleButton.BorderBrush = Brushes.Firebrick;
        }

        /// <summary>
        /// Fixes toggle button background color when activated. This broke in the xaml somewhere along the line
        /// </summary>
        /// <param name="toggleButton">Name of the ToggleButton being used</param>
        public static void ToggleBtn_on(ToggleButton toggleButton)
        {
            toggleButton.Background = Brushes.Firebrick;
            toggleButton.BorderBrush = Brushes.Firebrick;
        }
    }
}
