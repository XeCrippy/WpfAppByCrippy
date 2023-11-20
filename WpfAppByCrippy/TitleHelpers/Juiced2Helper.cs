using JRPCPlusPlus;
using System.Windows.Controls;

namespace WpfAppByCrippy.TitleHelpers
{
    internal class Juiced2Helper
    {
        public void AddMoney(uint amount)
        {
            uint num = App.xb.ReadUInt32(GetMoneyAddr());
            App.xb.WriteUInt32(GetMoneyAddr(), num + amount);
        }

        public uint GetCurrentMoney()
        {
            return App.xb.ReadUInt32(GetMoneyAddr());
        }

        private uint GetMoneyAddr()
        {
            uint num = App.xb.ReadUInt32(0x824CF0F8);
            uint num1 = App.xb.ReadUInt32(num);
            uint num2 = num1 + 0x14;
            return num2;
        }

        private uint GetMoneyPtr()
        {
            uint num = App.xb.ReadUInt32(0x824CF0F8);
            uint num1 = App.xb.ReadUInt32(num);
            return num1;
        }

        public void SetTotalMoney(TextBox moneyBox)
        {
            uint ptr = GetMoneyPtr();
            uint oldMoneyValue = App.xb.ReadUInt32(GetMoneyAddr());
            App.xb.CallVoid(0x8221B018, ptr, uint.Parse(moneyBox.Text), 0, oldMoneyValue, ptr, 0, 0, 0x82030000);
        }
    }
}
