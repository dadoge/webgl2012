using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using System.Threading;

namespace InfraredDetector
{
    public class Program
    {
        public enum TokenState
        {
            LISTEN = 0,
            STARTBYTE = 1,
            MESSAGE = 2,
            ENDBYTE = 3,
            READ = 4

        }
        public enum Gun
        {
            PUSSY = 0,
            MAN = 1
        }
        public static TokenState state;
        public static int sleep = 200;
        public static string message = "10110100";
        public static string message2 = "10111000";
        public static Gun playerGun = Gun.PUSSY;

        public static void Main()
        {

            InputPort digitalIn = new InputPort(Pins.GPIO_PIN_D3, false, Port.ResistorMode.Disabled);
            OutputPort led = new OutputPort(Pins.ONBOARD_LED, false);

            InterruptPort sender = new InterruptPort(Pins.GPIO_PIN_D13, false, Port.ResistorMode.Disabled, Port.InterruptMode.InterruptEdgeHigh);
            sender.OnInterrupt += sender_OnInterrupt;
            state = TokenState.LISTEN;
            string message = "";

            while (true)
            {

                switch (state)
                {
                    case TokenState.LISTEN:
                        GetListenByte(digitalIn);
                        break;
                    case TokenState.STARTBYTE:
                        GetStartByte(digitalIn);
                        break;
                    case TokenState.MESSAGE:
                        message = GetMessage(digitalIn);
                        break;
                    case TokenState.ENDBYTE:
                        GetEndByte(digitalIn);
                        break;
                    case TokenState.READ:
                        Debug.Print(String.Concat(message, "\n"));
                        state = TokenState.LISTEN;
                        message = "";
                        break;
                }
            }
        }

        static void sender_OnInterrupt(uint data1, uint data2, DateTime time)
        {
            var infraredOut = new Microsoft.SPOT.Hardware.PWM(PWMChannels.PWM_PIN_D6, 38000, .5, true); //50% brightness
            var led = new OutputPort(Pins.ONBOARD_LED, false);
            if (playerGun == Gun.PUSSY)
            {
                SendMessage(infraredOut, led, message);
            }
            else
            {
                SendMessage(infraredOut, led, message2);
            }
        }
        public static void GetListenByte(InputPort digitalIn)
        {
            while (true)
            {
                //Found our first one...now wait for a 0
                if (digitalIn.Read())
                {
                    while (digitalIn.Read())
                    {
                        //noop
                    }
                }
                state = TokenState.STARTBYTE;
                Thread.Sleep(199);
                break;
            }
        }
        public static void GetStartByte(InputPort digitalIn)
        {
            //Better be a 1
            if (digitalIn.Read())
            {
                Thread.Sleep(sleep);
                //And still a 1
                if (digitalIn.Read())
                {
                    Thread.Sleep(sleep);
                    state = TokenState.MESSAGE;
                    return;
                }
                state = TokenState.LISTEN;
            }
            state = TokenState.LISTEN;
        }
        public static string GetMessage(InputPort digitalIn)
        {
            var message = "";
            DateTime startTime;

            if (digitalIn.Read())
            {
                message += "1";
            }
            else
            {
                message += "0";
            }

            Thread.Sleep(sleep);

            if (digitalIn.Read())
            {
                message += "1";
            }
            else
            {
                message += "0";
            }

            if (message == "11" || message == "00")
            {
                state = TokenState.LISTEN;
                message = "";
                return "";
            }

            state = TokenState.ENDBYTE;
            return message;
        }
        public static void GetEndByte(InputPort digitalIn)
        {
            //Better be a 0
            if (!digitalIn.Read())
            {
                Thread.Sleep(sleep);
                //And still a 0
                if (!digitalIn.Read())
                {
                    Thread.Sleep(sleep);
                    state = TokenState.READ;
                    return;
                }
                state = TokenState.LISTEN;
            }
            state = TokenState.LISTEN;
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
                while (startTime.AddMilliseconds(sleep) > DateTime.Now)
                {
                    led.Write(true);
                    //noop
                }
                infraredOut.Stop();
            }
            else
            {
                var startTime = DateTime.Now;
                while (startTime.AddMilliseconds(sleep) > DateTime.Now)
                {
                    led.Write(false);
                    //noop
                }
                startTime = DateTime.Now;
            }
        }


    }
}
