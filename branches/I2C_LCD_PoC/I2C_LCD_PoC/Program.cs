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
        public static void Main()
        {

            OutputPort o = new OutputPort(Pins.ONBOARD_LED, true);

            while (true)
            {
                Thread.Sleep(1000);
                o.Write(true);
                Thread.Sleep(1000);
                o.Write(false);
            }
            // write your code here
            
        //    I2CDevice i2c = new I2CDevice(new I2CDevice.Configuration(0x38,100));
        //    Microsoft.SPOT.Hardware.I2CDevice.I2CTransaction[] actions;
        //    var buffer = new byte[1];

        //    actions = new I2CDevice.I2CTransaction[]
        //    {
        //        I2CDevice.CreateWriteTransaction(buffer)
        //    };

        //    while (true)
        //    {
        //        buffer[0] = 255;
        //        i2c.Execute(actions, 1000);
        //        Thread.Sleep(1000);


        //        buffer[0] = 0;
        //        i2c.Execute(actions, 1000);
        //        Thread.Sleep(1000);
        //    }
        }

    }
}
