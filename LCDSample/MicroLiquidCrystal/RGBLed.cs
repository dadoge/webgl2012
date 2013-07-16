using System.Threading;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;

/*
 * Copyright 2011 Stefan Thoolen (http://netmftoolbox.codeplex.com/)
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
namespace Toolbox.NETMF.Hardware
{
    /// <summary>
    /// Common RGB-led
    /// </summary>
    public class RGBLed
    {
        /// <summary>Reference to the red pin</summary>
        private PWM _Red;
        /// <summary>Reference to the green pin</summary>
        private PWM _Green;
        /// <summary>Reference to the blue pin</summary>
        private PWM _Blue;
        /// <summary>True when it's common anode, false if it's common cathode</summary>
        private bool _CommonAnode;

        /// <summary>
        /// Common RGB-led
        /// </summary>
        /// <param name="RedPin">The PWM-pin connected to Red</param>
        /// <param name="GreenPin">The PWM-pin connected to Green</param>
        /// <param name="BluePin">The PWM-pin connected to Blue</param>
        /// <param name="CommonAnode">Specifies if the led is common anode</param>
        public RGBLed(Cpu.Pin RedPin, Cpu.Pin GreenPin, Cpu.Pin BluePin, bool CommonAnode = true)
        {
            this._Red = new PWM(RedPin);
            this._Green = new PWM(GreenPin);
            this._Blue = new PWM(BluePin);
            this._CommonAnode = CommonAnode;
        }

        /// <summary>
        /// Sets the value of the RGB led
        /// </summary>
        /// <param name="Value">The RGB value (0x000000 to 0xffffff)</param>
        public void Write(int Value)
        {
            byte Red = (byte)(Value >> 16);
            byte Green = (byte)(Value >> 8 & 0xff);
            byte Blue = (byte)(Value & 0xff);
            this.Write(Red, Green, Blue);
        }

        /// <summary>
        /// Sets the value of the RGB led
        /// </summary>
        /// <param name="Red">Red strength (0 to 255)</param>
        /// <param name="Green">Green strength (0 to 255)</param>
        /// <param name="Blue">Blue strength (0 to 255)</param>
        public void Write(byte Red, byte Green, byte Blue)
        {
            // Values are sent from 0 to 255, but we actually want 0 to 100.
            uint uRed = (uint)(Red * 100 / 255);
            uint uGreen = (uint)(Green * 100 / 255);
            uint uBlue = (uint)(Blue * 100 / 255);
            // Common anode?
            if (this._CommonAnode)
            {
                uRed = 100 - uRed;
                uGreen = 100 - uGreen;
                uBlue = 100 - uBlue;
            }
            // Sets the values
            this._Red.SetDutyCycle(uRed);
            this._Green.SetDutyCycle(uGreen);
            this._Blue.SetDutyCycle(uBlue);
        }

        /// <summary>
        /// Sets the value of the RGB led
        /// </summary>
        /// <param name="Red">Red strength (0 to 255)</param>
        /// <param name="Green">Green strength (0 to 255)</param>
        /// <param name="Blue">Blue strength (0 to 255)</param>
        public void Write(uint Red, uint Green, uint Blue)
        {
            // Values are sent from 0 to 255, but we actually want 0 to 100.
            uint uRed = (uint)(Red * 100 / 255);
            uint uGreen = (uint)(Green * 100 / 255);
            uint uBlue = (uint)(Blue * 100 / 255);
            // Common anode?
            if (this._CommonAnode)
            {
                uRed = 100 - uRed;
                uGreen = 100 - uGreen;
                uBlue = 100 - uBlue;
            }
            // Sets the values
            this._Red.SetDutyCycle(uRed);
            this._Green.SetDutyCycle(uGreen);
            this._Blue.SetDutyCycle(uBlue);
        }

        #region Direct Color Calls

        public void Blue()
        {

            uint uRed = 0;
            uint uGreen = 0;
            uint uBlue = 100;
            // Common anode?
            if (this._CommonAnode)
            {
                uRed = 100 - uRed;
                uGreen = 100 - uGreen;
                uBlue = 100 - uBlue;
            }
            // Sets the values
            this._Red.SetDutyCycle(uRed);
            this._Green.SetDutyCycle(uGreen);
            this._Blue.SetDutyCycle(uBlue);

        }

        public void Green()
        {

            uint uRed = 0;
            uint uGreen = 100;
            uint uBlue = 0;
            // Common anode?
            if (this._CommonAnode)
            {
                uRed = 100 - uRed;
                uGreen = 100 - uGreen;
                uBlue = 100 - uBlue;
            }
            // Sets the values
            this._Red.SetDutyCycle(uRed);
            this._Green.SetDutyCycle(uGreen);
            this._Blue.SetDutyCycle(uBlue);

        }

        public void Red()
        {

            uint uRed = 100;
            uint uGreen = 0;
            uint uBlue = 0;
            // Common anode?
            if (this._CommonAnode)
            {
                uRed = 100 - uRed;
                uGreen = 100 - uGreen;
                uBlue = 100 - uBlue;
            }
            // Sets the values
            this._Red.SetDutyCycle(uRed);
            this._Green.SetDutyCycle(uGreen);
            this._Blue.SetDutyCycle(uBlue);

        }

        public void Turquoise()
        {

            uint uRed = 0;
            uint uGreen = 100;
            uint uBlue = 100;
            // Common anode?
            if (this._CommonAnode)
            {
                uRed = 100 - uRed;
                uGreen = 100 - uGreen;
                uBlue = 100 - uBlue;
            }
            // Sets the values
            this._Red.SetDutyCycle(uRed);
            this._Green.SetDutyCycle(uGreen);
            this._Blue.SetDutyCycle(uBlue);

        }

        public void Purple()
        {

            uint uRed = 50;
            uint uGreen = 0;
            uint uBlue = 100;
            // Common anode?
            if (this._CommonAnode)
            {
                uRed = 100 - uRed;
                uGreen = 100 - uGreen;
                uBlue = 100 - uBlue;
            }
            // Sets the values
            this._Red.SetDutyCycle(uRed);
            this._Green.SetDutyCycle(uGreen);
            this._Blue.SetDutyCycle(uBlue);

        }

        public void Pink()
        {

            uint uRed = 255;
            uint uGreen = 0;
            uint uBlue = 50;
            // Common anode?
            if (this._CommonAnode)
            {
                uRed = 100 - uRed;
                uGreen = 100 - uGreen;
                uBlue = 100 - uBlue;
            }
            // Sets the values
            this._Red.SetDutyCycle(uRed);
            this._Green.SetDutyCycle(uGreen);
            this._Blue.SetDutyCycle(uBlue);

        }

        public void Yellow()
        {

            uint uRed = 100;
            uint uGreen = 100;
            uint uBlue = 0;
            // Common anode?
            if (this._CommonAnode)
            {
                uRed = 100 - uRed;
                uGreen = 100 - uGreen;
                uBlue = 100 - uBlue;
            }
            // Sets the values
            this._Red.SetDutyCycle(uRed);
            this._Green.SetDutyCycle(uGreen);
            this._Blue.SetDutyCycle(uBlue);

        }

        public void Orange()
        {

            uint uRed = 100;
            uint uGreen = 50;
            uint uBlue = 0;
            // Common anode?
            if (this._CommonAnode)
            {
                uRed = 100 - uRed;
                uGreen = 100 - uGreen;
                uBlue = 100 - uBlue;
            }
            // Sets the values
            this._Red.SetDutyCycle(uRed);
            this._Green.SetDutyCycle(uGreen);
            this._Blue.SetDutyCycle(uBlue);

        }

        public void White()
        {

            uint uRed = 100;
            uint uGreen = 100;
            uint uBlue = 100;
            // Common anode?
            if (this._CommonAnode)
            {
                uRed = 100 - uRed;
                uGreen = 100 - uGreen;
                uBlue = 100 - uBlue;
            }
            // Sets the values
            this._Red.SetDutyCycle(uRed);
            this._Green.SetDutyCycle(uGreen);
            this._Blue.SetDutyCycle(uBlue);

        }

        public void Black()
        {

            uint uRed = 0;
            uint uGreen = 0;
            uint uBlue = 0;
            // Common anode?
            if (this._CommonAnode)
            {
                uRed = 100 - uRed;
                uGreen = 100 - uGreen;
                uBlue = 100 - uBlue;
            }
            // Sets the values
            this._Red.SetDutyCycle(uRed);
            this._Green.SetDutyCycle(uGreen);
            this._Blue.SetDutyCycle(uBlue);

        }

        public void GreenFadeUp()
        {

            for (int i = 5; i < 100; i += 1)
            {

                uint uRed = 0;
                uint uGreen = (uint)i;
                uint uBlue = 0;
                // Common anode?
                if (this._CommonAnode)
                {
                    uRed = 100 - uRed;
                    uGreen = 100 - uGreen;
                    uBlue = 100 - uBlue;
                }
                // Sets the values
                this._Red.SetDutyCycle(uRed);
                this._Green.SetDutyCycle(uGreen);
                this._Blue.SetDutyCycle(uBlue);

                Thread.Sleep(15);
            }

        }

        #endregion

        #region Special Fade Functions

        public void GreenFadeDown()
        {

            for (int i = 100; i > 5; i -= 1)
            {

                uint uRed = 0;
                uint uGreen = (uint)i;
                uint uBlue = 0;
                // Common anode?
                if (this._CommonAnode)
                {
                    uRed = 100 - uRed;
                    uGreen = 100 - uGreen;
                    uBlue = 100 - uBlue;
                }
                // Sets the values
                this._Red.SetDutyCycle(uRed);
                this._Green.SetDutyCycle(uGreen);
                this._Blue.SetDutyCycle(uBlue);

                Thread.Sleep(15);
            }

        }

        public void GreenFadeUpthenDown()
        {

            GreenFadeUp();
            Thread.Sleep(50);
            GreenFadeDown();
            Thread.Sleep(50);

        }

        #endregion


    }
}