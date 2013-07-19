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
        public static int playerAmmo = 8;
        public static int count;
        public static DateTime d;
        public static DateTime c;

        public static void Main()
        {
            infraredOut = new Microsoft.SPOT.Hardware.PWM(PWMChannels.PWM_PIN_D6, 38000, .5, true);
            InterruptPort sender = new InterruptPort(Pins.GPIO_PIN_D10, false, Port.ResistorMode.PullUp, Port.InterruptMode.InterruptEdgeLow);

            OutputPort ammoOut0 = new OutputPort(Pins.GPIO_PIN_D0, false);
            OutputPort ammoOut1 = new OutputPort(Pins.GPIO_PIN_D1, false);
            OutputPort ammoOut2 = new OutputPort(Pins.GPIO_PIN_D2, false);
            OutputPort ammoOut3 = new OutputPort(Pins.GPIO_PIN_D3, false);
            
            sender.OnInterrupt += sender_OnInterrupt;

            while (true)
            {
                GetPowerUp();
                DisplayAmmo(ammoOut0, ammoOut1, ammoOut2, ammoOut3);   
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
        public static void DisplayAmmo(OutputPort ammoOut0, OutputPort ammoOut1, OutputPort ammoOut2, OutputPort ammoOut3)
        {
            switch (playerAmmo)
            {
                case 0:
                    ammoOut0.Write(false);
                    ammoOut1.Write(false);
                    ammoOut2.Write(false);
                    ammoOut3.Write(false);
                    break;
                case 1:
                    ammoOut0.Write(true);
                    ammoOut1.Write(false);
                    ammoOut2.Write(false);
                    ammoOut3.Write(false);
                    break;
                case 2:
                    ammoOut0.Write(false);
                    ammoOut1.Write(true);
                    ammoOut2.Write(false);
                    ammoOut3.Write(false);
                    break;
                case 3:
                    ammoOut0.Write(true);
                    ammoOut1.Write(true);
                    ammoOut2.Write(false);
                    ammoOut3.Write(false);
                    break;
                case 4:
                    ammoOut0.Write(false);
                    ammoOut1.Write(false);
                    ammoOut2.Write(true);
                    ammoOut3.Write(false);
                    break;
                case 5:
                    ammoOut0.Write(true);
                    ammoOut1.Write(false);
                    ammoOut2.Write(true);
                    ammoOut3.Write(false);
                    break;
                case 6:
                    ammoOut0.Write(false);
                    ammoOut1.Write(true);
                    ammoOut2.Write(true);
                    ammoOut3.Write(false);
                    break;
                case 7:
                    ammoOut0.Write(true);
                    ammoOut1.Write(true);
                    ammoOut2.Write(true);
                    ammoOut3.Write(false);
                    break;
                case 8:
                    ammoOut0.Write(false);
                    ammoOut1.Write(false);
                    ammoOut2.Write(false);
                    ammoOut3.Write(true);
                    break;
            }
        }
    }
}
