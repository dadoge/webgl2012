using System;
using System.Threading;
using Microsoft.SPOT;
using SecretLabs.NETMF.Hardware.Netduino;
using Microsoft.SPOT.Hardware;

namespace InfraredPwmSender
{
    public class Main
    {
        public static void Main()
        {
            var infraredOut = new Microsoft.SPOT.Hardware.PWM(PWMChannels.PWM_PIN_D6, 38000, .5, true); //50% brightness
            var led = new OutputPort(Pins.ONBOARD_LED, false);

            string message = "10110100";
            string message2 = "10111000";

            while (true)
            {
                SendMessage(infraredOut, led, message);
                var temp = message;
                message = message2;
                message2 = temp;
            }

        }


        public static void SendMessage(PWM infraredOut, OutputPort led, string message)
        {
            foreach (char c in message)
            {
                SendBit(infraredOut, led, c);

            }
        }


        public static void SendBit(PWM infraredOut, OutputPort led, char c)
        {
            if (c == '1')
            {
                var startTime = DateTime.Now;
                infraredOut.Start();
                while (startTime.AddMilliseconds(200) > DateTime.Now)
                {
                    led.Write(true);
                    //noop
                }
                infraredOut.Stop();
            }
            else
            {
                var startTime = DateTime.Now;
                while (startTime.AddMilliseconds(200) > DateTime.Now)
                {
                    led.Write(false);
                    //noop
                }
                startTime = DateTime.Now;
            }
        }
    }
}


