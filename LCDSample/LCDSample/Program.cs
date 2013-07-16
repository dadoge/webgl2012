using System;
using System.Threading;
using FusionWare.SPOT.Hardware;
using MicroLiquidCrystal;
using Microsoft.SPOT;
using Microsoft.SPOT.Net.NetworkInformation;
using SecretLabs.NETMF.Hardware.Netduino;

namespace LCDSample
{
    public class Program
    {
        // Define the I2CBus
        private static I2CBus _bus;

        // Define the LCD Display
        private static Lcd LCD;

        public static void Main()
        {
            _bus = new I2CBus();

            initializeLCD(_bus);

            NetworkInterface networkInterface = NetworkInterface.GetAllNetworkInterfaces()[0];

            LCD.CreateChar(0, new byte[] { 0xFF, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0xFF });
            LCD.CreateChar(1, new byte[] { 0xFF, 0x11, 0x11, 0x11, 0x11, 0x11, 0xFF, 0x00 });
            LCD.CreateChar(2, new byte[] { 0xFF, 0x11, 0x11, 0x11, 0x11, 0xFF, 0x00, 0x00 });
            LCD.CreateChar(3, new byte[] { 0xFF, 0x11, 0x11, 0x11, 0xFF, 0x00, 0x00, 0x00 });
            // Write out messages
            //LCD.Print(Lcd.Position.ROW_1, Lcd.Position.COLUMN_1, Lcd.FillRow(" Tony and Jessie's   "));
            LCD.Print(Lcd.Position.ROW_2, Lcd.Position.COLUMN_1, Lcd.FillRow("     Laser Tag!  "));
            LCD.Print(Lcd.Position.ROW_3, Lcd.Position.COLUMN_1, Lcd.FillRow("     Press Button  "));
            LCD.Print(Lcd.Position.ROW_4, Lcd.Position.COLUMN_1, Lcd.FillRow("     To Start Game  "));
            LCD.SetCursorPosition(0, 0);
            LCD.WriteByte(0x00);
            LCD.WriteByte(0x01);
            LCD.WriteByte(0x02);
            LCD.WriteByte(0x03);

        }
        private static void initializeLCD(I2CBus bus)
        {

            // Use I2C provider
            // Default configuration coresponds to Adafruit's LCD backpack
            // Initialize provider (multiple devices can be attached to same bus)
            var lcdProvider = new MCP23008LcdTransferProvider(_bus);
            // Create the LCD interface
            LCD = new Lcd(lcdProvider);
            // Set the LCD Color property = Led.  This is for cleaner code only.
            //LCD.Color = Led;
            // Set up the LCD's number of columns and rows: 
            LCD.Begin(20, 4);
            // Clear the LCD
            LCD.Clear();
        }

    }
}
