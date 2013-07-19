using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace LT_Gun
{
    public class LT_Gun
    {
        public enum Gun
        {
            Regular = 0,
            MAN = 1
        }
        public static int sleep = 20;
        public static string message = "10110100";
        public static string message2 = "10111000";
        public static Gun playerGun = Gun.Regular;
        public static Microsoft.SPOT.Hardware.PWM infraredOut;
        public static bool powerUp = false;
        public static int playerAmmo = 7;
        public static int count;
        public static DateTime d;

        public static void Main()
        {
            infraredOut = new Microsoft.SPOT.Hardware.PWM(PWMChannels.PWM_PIN_D6, 38000, .5, true);
            InterruptPort sender = new InterruptPort(Pins.GPIO_PIN_D10, false, Port.ResistorMode.PullUp, Port.InterruptMode.InterruptEdgeLow);
            sender.OnInterrupt += sender_OnInterrupt;

            while (true)
            {
                
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
                if (playerAmmo >= 0)
                {
                    playerAmmo--;
                }
                else
                {
                    playerAmmo = 8;
                }

            }
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
        }
    }
}
