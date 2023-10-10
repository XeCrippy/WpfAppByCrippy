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

        private readonly uint defaultValue = 0xD1BF78C0; // the default assembly code for the oneHitKill address which translates to : stfs f13,0x78C0(r31) | f13 = float register 13 | This is storing the new updated health value (value stored in f13) at the current dynamic memory address which would be *r31 + 0x78C0 (the value stored in r31 at this point will be a dynamic memory adress. so that address + 0x78C0) 
        private readonly uint god = 0x60000000; // if it's our player receiving damage we will nop the function so that we don't take any damage
        private readonly uint kill = 0xD07F78C0; // the modified assembly code for oneHitKill which translates to : stfs f3,0x78C0(r31) | This is simply swapping the new updated health value (which should be stored in f13) with the value stored in f3 instead. In this case I know it's always 0 so it will set the enemy health to 0
        private readonly uint oneHitKill = 0x823B573C; // the address we are going to breakpoint. when the breakpoint is hit, we will compare the value that sits in register r4, if its 1 we do god mode and if not we do instakill. 1 is the pointer we're using for our player, any other value we assume is the enemy
        
        /// <summary>
        /// This will hook one hit kill/god mode until disabled. This is good for example when your player's health and the enemy's health are shared within the same function where noping would just give everyone god mode. Can be adapted to other functions/games
        /// </summary>
        /// <param name="Addr">The address you want to breakpoint</param>
        /// <param name="cmpValue">Pointer you are using to compare</param>
        /// <param name="cmpRegister">The register where the compare pointer is stored</param>
        private void MortalKombatTest(uint Addr, uint cmpValue, XboxRegisters64 cmpRegister) // compare value (player pointer) = 1  compare register = r4
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
                    App.xbdbg.WriteUInt32(oneHitKill, defaultValue);
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
