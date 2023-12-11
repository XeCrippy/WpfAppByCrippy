using JRPCPlusPlus;
using System;
using System.Windows;
using System.Windows.Controls;
using WpfAppByCrippy.Logging;
using XDevkit;

namespace WpfAppByCrippy.Pages
{
    public partial class Forza2Page : Page
    {
        public Forza2Page()
        {
            InitializeComponent();
        }

        private void EnsureDebuggerConnection()
        {
            if (!App.xbdbg.DebugTarget.IsDebuggerConnected(out _, out _))
            {
                App.xbdbg.DebugTarget.ConnectAsDebugger("XB360Anarchy", XboxDebugConnectFlags.Force);
            }
        }

        private void SetBreakpoint(uint address)
        {
            App.xbdbg.DebugTarget.SetBreakpoint(address);
        }

        private void HandleDebugEvent(XboxDebugEventType eventType, IXboxEventInfo eventInfo)
        {
            try
            {
                if (eventType == XboxDebugEventType.ExecutionBreak && eventInfo.Info.Address == 0x824765C4)
                {
                    HandleExecutionBreak(eventInfo);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions during event handling
                Logger.LogError("Forza 2 : Handling Debug Event", ex);
            }
        }

        private void HandleExecutionBreak(IXboxEventInfo eventInfo)
        {
            try
            {
                // Get the value in the r31 register (program counter)
                eventInfo.Info.Thread.TopOfStack.GetRegister64(XboxRegisters64.r31, out long programCounter);

                // Update UI using Dispatcher (since it's in a different thread)
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    // Validate and parse the MoneyBox.Text input
                    if (uint.TryParse(MoneyBox.Text, out uint moneyValue))
                    {
                        // Write the value in the MoneyBox TextBox to the memory address + 0x8
                        App.xbdbg.WriteUInt32((uint)programCounter + 0x8, moneyValue);

                        // Remove all breakpoints
                        App.xbdbg.DebugTarget.RemoveAllBreakpoints();

                        // Continue the thread
                        eventInfo.Info.Thread.Continue(true);

                        // Continue execution of the debug target
                        App.xbdbg.DebugTarget.Go(out bool flag2);

                        // Free the event information
                        App.xbdbg.DebugTarget.FreeEventInfo(eventInfo.Info);
                    }
                    else
                    {
                        // Handle invalid input in MoneyBox.Text
                        App.ParsingError();
                    }
                }));
            }
            catch (Exception ex)
            {
                // Handle exceptions during execution break handling
                Logger.LogError("Forza 2 : Handling Execution Break", ex);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!App.dbgConnection)
                {
                    if (App.DbgConnect()) App.dbgConnection = true;
                    else
                    {
                        App.dbgConnection = false;
                        App.ConnectionError();
                    }
                }
            }
            catch (Exception ex)
            {
                App.Error("Forza 2 : Load Page", ex);
            }
        }

        /// <summary>
        /// Checks if the application is in debug mode (App.dbgConnection).
        /// If in debug mode, checks if the debugger is connected. If not, it attempts to connect as a debugger.
        /// Sets a breakpoint at the specified address (0x824765C4).
        /// Subscribes to the OnStdNotify event, which is triggered when certain debug events occur.
        /// When an execution break occurs at the specified breakpoint address, it retrieves the value in the r31 register (program counter).
        /// Updates the UI by writing the value in the MoneyBox TextBox to the memory address (address + 0x8).
        /// Removes all breakpoints, continues the thread, and resumes the execution of the debug target.
        /// Optionally disconnects the debugger after performing the desired operations.
        /// </summary>
        private void MoneyBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (App.dbgConnection)
                {
                    // Check and connect as a debugger if not connected
                    EnsureDebuggerConnection();

                    // Set a breakpoint at the specified address
                    SetBreakpoint(0x824765C4);

                    // Subscribe to the OnStdNotify event for handling debug events
                    App.xbdbg.OnStdNotify += HandleDebugEvent;

                    // Optionally disconnect the debugger after performing the desired operations
                     //App.xbdbg.DebugTarget.DisconnectAsDebugger();
                }
                else
                {
                    // Handle the case where the application is not in debug mode
                    App.ConnectionError();
                }
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                Logger.LogError("Forza 2 : Add Money", ex);
            }
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                App.xbdbg.DebugTarget.RemoveAllBreakpoints();
                App.xbdbg.DebugTarget.DisconnectAsDebugger();
            }
            catch (Exception ex) 
            {
                Logger.LogError("Forza 2 : Remove Breakpoints on page exit", ex);
            }
        }
    }
}
