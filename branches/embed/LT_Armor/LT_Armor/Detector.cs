using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace LT_Armor
{
    public class Detector
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
        public static int sleep = 20;
        public static string message = "10110100";
        public static string message2 = "10111000";
        public static int playerHealth = 7;
        public static void Main()
        {
            InputPort digitalIn = new InputPort(Pins.GPIO_PIN_D3, false, Port.ResistorMode.Disabled);
            OutputPort healthOut0 = new OutputPort(Pins.GPIO_PIN_D1, false);
            OutputPort healthOut1 = new OutputPort(Pins.GPIO_PIN_D2, false);
            OutputPort healthOut2 = new OutputPort(Pins.GPIO_PIN_D4, false);
            OutputPort healthOut3 = new OutputPort(Pins.GPIO_PIN_D5, false);
            OutputPort healthOut4 = new OutputPort(Pins.GPIO_PIN_D7, false);
            OutputPort healthOut5 = new OutputPort(Pins.GPIO_PIN_D8, false);
            OutputPort healthOut6 = new OutputPort(Pins.GPIO_PIN_D9, false);

            while (true)
            {

                DisplayHealth(healthOut0, healthOut1, healthOut2, healthOut3, healthOut4, healthOut5, healthOut6);

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
                        Debug.Print(String.Concat(message + " " + "PlayerHealth" + ": " + playerHealth, "\n"));
                        UpdatePlayer(message);
                        state = TokenState.LISTEN;
                        message = "";
                        break;
                }
            }
        }
        public static void DisplayHealth(OutputPort healthOut0, OutputPort healthOut1, OutputPort healthOut2, OutputPort healthOut3, OutputPort healthOut4, OutputPort healthOut5, OutputPort healthOut6)
        {
            switch (playerHealth)
            {
                case 0:
                    healthOut0.Write(false);
                    healthOut1.Write(false);
                    healthOut2.Write(false);
                    healthOut3.Write(false);
                    healthOut4.Write(false);
                    healthOut5.Write(false);
                    healthOut6.Write(true);
                    break;
                case 1:
                    healthOut0.Write(true);
                    healthOut1.Write(false);
                    healthOut2.Write(false);
                    healthOut3.Write(true);
                    healthOut4.Write(true);
                    healthOut5.Write(true);
                    healthOut6.Write(true);
                    break;
                case 2:
                    healthOut0.Write(false);
                    healthOut1.Write(false);
                    healthOut2.Write(true);
                    healthOut3.Write(false);
                    healthOut4.Write(false);
                    healthOut5.Write(true);
                    healthOut6.Write(false);
                    break;
                case 3:
                    healthOut0.Write(false);
                    healthOut1.Write(false);
                    healthOut2.Write(false);
                    healthOut3.Write(false);
                    healthOut4.Write(true);
                    healthOut5.Write(true);
                    healthOut6.Write(false);
                    break;
                case 4:
                    healthOut0.Write(true);
                    healthOut1.Write(false);
                    healthOut2.Write(false);
                    healthOut3.Write(true);
                    healthOut4.Write(true);
                    healthOut5.Write(false);
                    healthOut6.Write(false);
                    break;
                case 5:
                    healthOut0.Write(false);
                    healthOut1.Write(true);
                    healthOut2.Write(false);
                    healthOut3.Write(false);
                    healthOut4.Write(true);
                    healthOut5.Write(false);
                    healthOut6.Write(false);
                    break;
                case 6:
                    healthOut0.Write(false);
                    healthOut1.Write(true);
                    healthOut2.Write(false);
                    healthOut3.Write(false);
                    healthOut4.Write(false);
                    healthOut5.Write(false);
                    healthOut6.Write(false);
                    break;
                case 7:
                    healthOut0.Write(false);
                    healthOut1.Write(false);
                    healthOut2.Write(false);
                    healthOut3.Write(true);
                    healthOut4.Write(true);
                    healthOut5.Write(true);
                    healthOut6.Write(true);
                    break;
                case 8:
                    healthOut0.Write(false);
                    healthOut1.Write(false);
                    healthOut2.Write(false);
                    healthOut3.Write(false);
                    healthOut4.Write(false);
                    healthOut5.Write(false);
                    healthOut6.Write(false);
                    break;
            }
        }
        public static void UpdatePlayer(string message)
        {
            if (message == "01")
            {
                playerHealth--;
            }
            else
            {
                playerHealth -= 2;
            }
        }
        public static void GetListenByte(InputPort digitalIn)
        {
            while (true)
            {
                //Found our first one...now wait for a 0
                if (!digitalIn.Read())
                {
                    var d = DateTime.Now;
                    while (digitalIn.Read() && d.AddMilliseconds(500) < DateTime.Now)
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
