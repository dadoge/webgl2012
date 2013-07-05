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
            Regular = 0,
            MAN = 1
        }
        public static TokenState state;
        public static int sleep = 20;
        public static string message = "10110100";
        public static string message2 = "10111000";
        public static Gun playerGun = Gun.Regular;
        public static Microsoft.SPOT.Hardware.PWM infraredOut;
        public static bool powerUp = false;
        public static DateTime d;
        public static DateTime c;
        public static int count;
        public static int playerHeath = 7;
        public static int playerAmmo = 7;

        public static void Main()
        {
            count = 0;
            d = DateTime.Now;
            c = DateTime.Now;

            InputPort digitalIn = new InputPort(Pins.GPIO_PIN_D3, false, Port.ResistorMode.Disabled);
            OutputPort powerUpPort = new OutputPort(Pins.GPIO_PIN_D0, false);
            OutputPort healthOut0 = new OutputPort(Pins.GPIO_PIN_D1, false);
            OutputPort healthOut1 = new OutputPort(Pins.GPIO_PIN_D2, false);
            OutputPort healthOut2 = new OutputPort(Pins.GPIO_PIN_D4, false);
            OutputPort healthOut3 = new OutputPort(Pins.GPIO_PIN_D5, false);
            OutputPort healthOut4 = new OutputPort(Pins.GPIO_PIN_D7, false);
            OutputPort healthOut5 = new OutputPort(Pins.GPIO_PIN_D8, false);
            OutputPort healthOut6 = new OutputPort(Pins.GPIO_PIN_D9, false);

            OutputPort sanityPort = new OutputPort(Pins.GPIO_PIN_D13, true);
            infraredOut = new Microsoft.SPOT.Hardware.PWM(PWMChannels.PWM_PIN_D6, 38000, .5, true);
            InterruptPort sender = new InterruptPort(Pins.GPIO_PIN_D10, false, Port.ResistorMode.PullUp, Port.InterruptMode.InterruptEdgeLow);
            sender.OnInterrupt += sender_OnInterrupt;
            state = TokenState.LISTEN;
            string message = "";


            while (true)
            {
                sanityPort.Write(true);
                GetPowerUp();
                DisplayHeath(healthOut0, healthOut1, healthOut2, healthOut3, healthOut4, healthOut5, healthOut6);
                powerUpPort.Write(powerUp);
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
                        Debug.Print(String.Concat(message + " " + "PlayerHealth" + ": " + playerHeath , "\n"));
                        UpdatePlayer(message);
                        state = TokenState.LISTEN;
                        message = "";
                        break;
                }
            }
        }
        public static void DisplayHeath(OutputPort healthOut0, OutputPort healthOut1, OutputPort healthOut2, OutputPort healthOut3, OutputPort healthOut4, OutputPort healthOut5, OutputPort healthOut6)
        {
            switch (playerAmmo)
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


            //switch (playerHeath)
            //{
            //    case 0:
            //        healthOut.Write(false);
            //        healthOut2.Write(false);
            //        healthOut3.Write(false);
            //        break;
            //    case 1:
            //        healthOut.Write(false);
            //        healthOut2.Write(false);
            //        healthOut3.Write(true);
            //        break;
            //    case 2:
            //        healthOut.Write(false);
            //        healthOut2.Write(true);
            //        healthOut3.Write(false);
            //        break;
            //    case 3:
            //        healthOut.Write(false);
            //        healthOut2.Write(true);
            //        healthOut3.Write(true);
            //        break;
            //    case 4:
            //        healthOut.Write(true);
            //        healthOut2.Write(false);
            //        healthOut3.Write(false);
            //        break;
            //    case 5:
            //        healthOut.Write(true);
            //        healthOut2.Write(false);
            //        healthOut3.Write(true);
            //        break;
            //    case 6:
            //        healthOut.Write(true);
            //        healthOut2.Write(true);
            //        healthOut3.Write(false);
            //        break;
            //    case 7:
            //        healthOut.Write(true);
            //        healthOut2.Write(true);
            //        healthOut3.Write(true);
            //        break;
            //}


            //if (playerHeath >= 15)
            //{
            //    healthOut.Write(false);
            //    healthOut2.Write(false);
            //}
            //else if (playerHeath >= 10)
            //{
            //    healthOut.Write(true);
            //    healthOut2.Write(false);
            //}
            //else if (playerHeath >= 5)
            //{
            //    healthOut.Write(false);
            //    healthOut2.Write(true);
            //}
            //else if (playerHeath <= 0)
            //{
            //    healthOut.Write(true);
            //    healthOut2.Write(true);
            //}
        }
        public static void UpdatePlayer(string message)
        {
            if (message == "01")
            {
                playerHeath--;
            }
            else
            {
                playerHeath -= 2;
            }
        }
        public static void GetPowerUp()
        {
            if (c.AddMilliseconds(5000) < DateTime.Now)
            {
                c = DateTime.Now;
                Random r = new Random();
                var random = r.Next() % 10;
                if (random > 5)
                {
                    powerUp = true;
                    playerGun = Gun.MAN;
                }
                else
                {
                    powerUp = false;
                    playerGun = Gun.Regular;
                    playerHeath++;
                }
            }
        }
        static void sender_OnInterrupt(uint data1, uint data2, DateTime time)
        {
            if (d.AddMilliseconds(350) < DateTime.Now)
            {
                d = DateTime.Now;
                count++;
                if (playerGun == Gun.Regular)
                {
                    Debug.Print(count.ToString() + ": Regular Gun");
                    SendMessage(infraredOut, message);
                }
                else
                {
                    Debug.Print(count.ToString() + ": Man Gun");
                    SendMessage(infraredOut, message2);
                }
                playerAmmo--;
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
                    while (digitalIn.Read() && d.AddMilliseconds(500) < DateTime.Now )
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
            DateTime startTime;

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

        public static void SendMessage(PWM infraredOut, string message)
        {
            foreach (char c in message)
            {
                SendBit(infraredOut, c);

            }
        }


        public static void SendBit(PWM infraredOut, char c)
        {

            if (c == '1')
            {
                var startTime = DateTime.Now;
                infraredOut.Start();
                while (startTime.AddMilliseconds(sleep) > DateTime.Now)
                {

                }
                infraredOut.Stop();
            }
            else
            {
                var startTime = DateTime.Now;
                while (startTime.AddMilliseconds(sleep) > DateTime.Now)
                {

                }
                startTime = DateTime.Now;
            }
        }
    }
}
