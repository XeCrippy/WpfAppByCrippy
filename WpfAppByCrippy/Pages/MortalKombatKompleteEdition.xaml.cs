using JRPCPlusPlus;
using System;
using System.Windows;
using System.Windows.Controls;
using XDevkit;

namespace WpfAppByCrippy.Pages
{
    public partial class MortalKombatKompleteEdition : Page
    {
        private bool mkOhk = false;

        private readonly uint god = 0x60000000;
        private readonly uint kill = 0xD07F78C0;
        private readonly uint oneHitKill = 0x823B573C;
        
        private void MortalKombatTest(uint Addr, uint cmpValue, XboxRegisters64 cmpRegister) // addr reg = r31 chkreg = r23
        {
            try
            {
                if (App.dbgConnection)
                {
                    if (App.xbdbg.DebugTarget.IsDebuggerConnected(out string dbgName, out string usrName) == false)
                    {
                        App.xbdbg.DebugTarget.ConnectAsDebugger("XB360Anarchy", XboxDebugConnectFlags.Force);
                    }
                    App.xbdbg.DebugTarget.SetBreakpoint(Addr);
                    App.xbdbg.OnStdNotify += (EventType, EventInfo) =>
                    {
                        if (EventType == XboxDebugEventType.ExecutionBreak && EventInfo.Info.Address == Addr)
                        {
                            EventInfo.Info.Thread.TopOfStack.GetRegister64(cmpRegister, out long playerchk);
                            Dispatcher.BeginInvoke(new Action(() =>
                            {
                                if (playerchk == cmpValue) App.xbdbg.WriteUInt32(Addr, god);
                                else App.xbdbg.WriteUInt32(Addr, kill);
                                EventInfo.Info.Thread.Continue(true);
                                App.xbdbg.DebugTarget.Go(out bool flag2);
                                App.xbdbg.DebugTarget.FreeEventInfo(EventInfo.Info);
                             }));
                        }
                    };
                }
                else App.ConnectionError();
            }
            catch { }
        }

        public MortalKombatKompleteEdition()
        {
            InitializeComponent();
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
                App.Error(ex);
            }
        }

        private void OHKBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!mkOhk && App.activeConnection)
                {
                    MortalKombatTest(oneHitKill, 1, XboxRegisters64.r4);
                    mkOhk = true;
                    App.ToggleBtn_on(OHKBtn);
                }
                else if (!App.activeConnection)
                {
                    App.ConnectionError();
                    mkOhk = false;
                    App.ToggleBtn_off(OHKBtn);
                }
                else
                {
                    App.xbdbg.DebugTarget.RemoveAllBreakpoints();
                    App.xbdbg.WriteUInt32(oneHitKill, 0xD1BF78C0);
                    mkOhk = false;
                    App.ToggleBtn_off(OHKBtn);
                }
            }
            catch (Exception ex)
            {
                App.Error(ex);
            }
        }
    }
}
