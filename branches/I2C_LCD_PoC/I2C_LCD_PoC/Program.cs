using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace I2C_LCD_PoC
{
    public class Program
    {
        public static byte[] buffer = { 0x50 };
        public static void Main()
        {
            // write your code here
            
            I2CDevice i2c = new I2CDevice(new I2CDevice.Configuration(0x50,100));
            Microsoft.SPOT.Hardware.I2CDevice.I2CTransaction[] actions;
            

            actions = new I2CDevice.I2CTransaction[]
            {
                I2CDevice.CreateWriteTransaction(buffer)
            };

            while (true)
            {
                i2c.Execute(actions, 1000);
                Thread.Sleep(30);
            }
        }

    }
}
