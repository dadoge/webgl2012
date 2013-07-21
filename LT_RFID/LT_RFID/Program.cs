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
            int count = 0;
            SerialPort SPort = new SerialPort(SerialPorts.COM2, 9600, Parity.None,8,StopBits.One);
            SPort.ReadTimeout = 1000;
            SPort.WriteTimeout = 1000;
            byte[] buf = new byte[5];
            string CardId = "";
            SPort.Open();
            byte[] readCommand = { 0x21, 0x52, 0x57, 0x01, 0x03 };
            int numCodes = 0;
            while (true)
            {
                //SPort.Write(new byte[] { 0xFF, 0xFF, 0X39, 0x44 }, 0, 4);
                SPort.Write(readCommand, 0, 5);
                SPort.Flush();
                int readcnt = SPort.Read(buf, 0, SPort.BytesToRead);
                SPort.Flush();
                string s = "";
                if (buf[0] == 0x01 && numCodes < 10)
                {
                    foreach (byte b in buf)
                    {
                        s = s + b.ToString() + ",";
                    }
                    if (s[0] == '1')
                    {
                        count++;
                        Debug.Print(count.ToString() + ":" + s + "\n");
                        numCodes++;
                    }
                }
            }
        }
    }
}

