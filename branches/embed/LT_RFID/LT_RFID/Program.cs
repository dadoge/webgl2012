using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using System.IO.Ports;

namespace LT_RFID
{
    public class Program
    {
        public static SerialPort SPort;
        public static void Main()
        {
            SerialPort SPort = new SerialPort(SerialPorts.COM2, 9600);
            SPort.ReadTimeout = 1000;
            SPort.WriteTimeout = 1000;
            byte[] buf = new byte[5];
            string CardId = "";
            SPort.Open();
            SPort.Write(System.Text.Encoding.UTF8.GetBytes("!RW"), 0, 3);
            SPort.Write(new byte[] {0x01, 0x20}, 0, 2);
            while (true)
            {
                //SPort.Write(new byte[] { 0xFF, 0xFF, 0X39, 0x44 }, 0, 4);
                int readcnt = SPort.Read(buf, 0, SPort.BytesToRead);
                string s = "";
                foreach(byte b in buf)
                {
                    s = s + (b.ToString() + ",");
                }
                if (s[0] == '1')
                {
                    Debug.Print(s + "\n");
                }
                SPort.Write(System.Text.Encoding.UTF8.GetBytes("!RW"), 0, 3);
                SPort.Write(new byte[] { 0x01, 0x21}, 0, 2);

            }
        }
    }
}

