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
            // Read the value at address 0x824CF0F8
            uint num = App.xb.ReadUInt32(0x824CF0F8);

            // Read the value at the address obtained in the previous step
            uint num1 = App.xb.ReadUInt32(num);

            // Calculate the final money address
            uint num2 = num1 + 0x14;

            // Return the calculated address
            return num2;
        }

        private uint GetMoneyPtr()
        {
            // Read the value at address 0x824CF0F8
            uint num = App.xb.ReadUInt32(0x824CF0F8);

            // Read the value at the address obtained in the previous step
            uint num1 = App.xb.ReadUInt32(num);

            // Return the calculated pointer value
            return num1;
        }

        public void SetTotalMoney(TextBox moneyBox)
        {
            // Obtain the pointer and current money value
            uint ptr = GetMoneyPtr();
            uint oldMoneyValue = App.xb.ReadUInt32(GetMoneyAddr());

            // Parse the new money value from the TextBox
            uint newMoneyValue;
            if (!uint.TryParse(moneyBox.Text, out newMoneyValue))
            {
                // Handle parsing error, e.g., log, display a message, or set a default value
                App.XMessageBox("Invalid Input", "The value you entered is not a valid 32 bit unsigned integer");
            }

            // Make a call to update the money value
            App.xb.CallVoid(0x8221B018, ptr, newMoneyValue, 0, oldMoneyValue, ptr, 0, 0, 0x82030000);
        }
    }
}
