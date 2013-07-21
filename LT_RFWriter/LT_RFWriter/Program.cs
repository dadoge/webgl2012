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
            SerialPort SPort = new SerialPort(SerialPorts.COM2, 9600, Parity.None, 8, StopBits.One);
            SPort.ReadTimeout = 1000;
            SPort.WriteTimeout = 1000;
            byte[] buf = new byte[5];
            string CardId = "";
            SPort.Open();
            byte[] writeCommand = { 0x21, 0x52, 0x57, 0x02, 0x03, 0x10, 0x20, 0x10, 0x21};

            while (true)
            {
                SPort.Write(writeCommand, 0, 9);
                int readcnt = SPort.Read(buf, 0, SPort.BytesToRead);
                string s = "";
                if (buf[0] == 0x01)
                {
                    Debug.Print("Success");
                }
            }
        }
    }
}

