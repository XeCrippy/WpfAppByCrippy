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
            uint currentMoney = App.xb.ReadUInt32(GetMoneyAddress());
            App.xb.WriteUInt32(GetMoneyAddress(), currentMoney + uint.Parse(moneyBox.Text));
        }

        private uint GetHealthAddress()
        {
            return App.xb.ReadUInt32(playerPtr) + healthOffset;
        }

        private uint GetMaxHealthAddress()
        {
            return App.xb.ReadUInt32(playerPtr) + maxHealthOffset;
        }

        private uint GetMoneyAddress()
        {
            return App.xb.ReadUInt32(moneyPtr) + moneyOffset;
        }

        private uint GetPlayerStruct()
        {
            return App.xb.ReadUInt32(playerPtr) + playerPtrOffset;
        }

        public void MaxPlayerHealth()
        {
            App.xb.WriteFloat(GetMaxHealthAddress(), 9999999.0f);
            App.xb.CallVoid(0x82793720, GetPlayerStruct(), 9999999u, 0);
        }
    }
}
