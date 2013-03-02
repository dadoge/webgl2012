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
        public static TokenState state;
        public static int sleep = 10;

        public static void Main()
        {

            InputPort digitalIn = new InputPort(Pins.GPIO_PIN_D3, false, Port.ResistorMode.Disabled);
            OutputPort led = new OutputPort(Pins.ONBOARD_LED, false);

            state = TokenState.LISTEN;
            string message = "";
            int count = 0;

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
                        //Debug.Print(String.Concat(message, "\n"));
                        break;
                    case TokenState.READ:
                        if (message == "01")
                        {
                            Debug.Print(count.ToString() + ": Received " + message + ". You now have a Shield!");
                        }
                        else if (message == "10")
                        {
                            Debug.Print(count.ToString() + ": Received " + message + ". You now have a more Powerful Gun!");
                        }
                        count++;
                        state = TokenState.LISTEN;
                        message = "";
                        break;
                }
            }
        }
        public static void GetListenByte(InputPort digitalIn)
        {
            while (true)
            {
                //Found our first one...now wait for a 0
                if (!digitalIn.Read())
                {
                    while (!digitalIn.Read())
                    {
                        //noop
                    }
                }
                state = TokenState.STARTBYTE;
                Thread.Sleep(sleep);
                break;
            }
        }
        public static void GetStartByte(InputPort digitalIn)
        {
            //Better be a 1
            if (!digitalIn.Read())
            {
                Thread.Sleep(sleep);
                //And still a 1
                if (!digitalIn.Read())
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
            //DateTime startTime;

            if (!digitalIn.Read())
            {
                message += "1";
            }
            else
            {
                message += "0";
            }

            Thread.Sleep(sleep);

            if (!digitalIn.Read())
            {
                message += "1";
            }
            else
            {
                message += "0";
            }

            Thread.Sleep(sleep);

            if (message == "11" || message == "00")
            {
                Debug.Print("I got a " + message);
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
            if (digitalIn.Read())
            {
                Thread.Sleep(sleep);
                //And still a 0
                if (digitalIn.Read())
                {
                    Thread.Sleep(sleep);
                    state = TokenState.READ;
                    return;
                }
                state = TokenState.LISTEN;
            }
            state = TokenState.LISTEN;
        }
    }
}