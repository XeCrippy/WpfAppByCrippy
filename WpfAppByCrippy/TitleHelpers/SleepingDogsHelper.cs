using JRPCPlusPlus;
using System.Windows.Controls;

namespace WpfAppByCrippy.TitleHelpers
{
    internal class SleepingDogsHelper
    {
        private readonly uint moneyPtr = 0x8304CDFC;
        private readonly uint moneyOffset = 0x3C0;
        private readonly uint playerPtr = 0x82FDEA08;
        private readonly uint playerPtrOffset = 0x1C3A0;
        private readonly uint healthOffset = 0x1C3CC;
        private readonly uint maxHealthOffset = 0x1C3D4;

        public void AddMoney(TextBox moneyBox)
        {
            if (uint.TryParse(moneyBox.Text, out uint moneyToAdd))
            {
                uint currentMoney = App.xb.ReadUInt32(GetMoneyAddress());
                App.xb.WriteUInt32(GetMoneyAddress(), currentMoney + moneyToAdd);
            }
            else
            {
                // Handle invalid input (non-numeric) in moneyBox, e.g., display an error message.
                App.XMessageBox("Invalid Input", "The value you entered is not a valid 32 bit unsigned integer");
            }
        }

        private uint GetAddressFromOffset(uint baseAddress, uint offset)
        {
            return App.xb.ReadUInt32(baseAddress) + offset;
        }

        private uint GetHealthAddress()
        {
            return GetAddressFromOffset(playerPtr, healthOffset);
        }

        private uint GetMaxHealthAddress()
        {
            return GetAddressFromOffset(playerPtr, maxHealthOffset);
        }

        private uint GetMoneyAddress()
        {
            return GetAddressFromOffset(moneyPtr, moneyOffset);
        }

        private uint GetPlayerStruct()
        {
            return GetAddressFromOffset(playerPtr, playerPtrOffset);
        }

        public void SetPlayerHealth(float healthValue)
        {
            // Set max health
            App.xb.WriteFloat(GetMaxHealthAddress(), healthValue);

            // Set current health
            App.xb.CallVoid(0x82793720, GetPlayerStruct(), healthValue, 0);
        }
    }
}
