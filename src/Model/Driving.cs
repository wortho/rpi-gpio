
using System;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Gpio;

namespace rpi.gpio.Model
{
    public static class Driving
    { 
        public static void Forward(int speed)
        {
            SetupMotors();
            Pi.Gpio[Motors.LeftMotorForwardsPin].SoftPwmValue = speed;
            Pi.Gpio[Motors.LeftMotorReversePin].SoftPwmValue = 0;
            Pi.Gpio[Motors.RightMotorForwardsPin].SoftPwmValue = speed;
            Pi.Gpio[Motors.RightMotorReversePin].SoftPwmValue = 0;
        }

        public static void Reverse(int speed)
        {
            SetupMotors();
            Pi.Gpio[Motors.LeftMotorReversePin].SoftPwmValue = speed;
            Pi.Gpio[Motors.LeftMotorForwardsPin].SoftPwmValue = 0;
            Pi.Gpio[Motors.RightMotorReversePin].SoftPwmValue = speed;
            Pi.Gpio[Motors.RightMotorForwardsPin].SoftPwmValue = 0;
        }

        internal static void Left(int speed)
        {
            SetupMotors();
            Pi.Gpio[Motors.LeftMotorForwardsPin].SoftPwmValue = 0;
            Pi.Gpio[Motors.LeftMotorReversePin].SoftPwmValue = 0;
            Pi.Gpio[Motors.RightMotorForwardsPin].SoftPwmValue = speed;
            Pi.Gpio[Motors.RightMotorReversePin].SoftPwmValue = 0;
        }

        
        internal static void Right(int speed)
        {
            SetupMotors();
            Pi.Gpio[Motors.LeftMotorForwardsPin].SoftPwmValue = speed;
            Pi.Gpio[Motors.LeftMotorReversePin].SoftPwmValue = 0;
            Pi.Gpio[Motors.RightMotorForwardsPin].SoftPwmValue = 0;
            Pi.Gpio[Motors.RightMotorReversePin].SoftPwmValue = 0;
        }

        public static void Stop()
        {
            SetupMotors();
            Pi.Gpio[Motors.LeftMotorReversePin].SoftPwmValue = 0;
            Pi.Gpio[Motors.LeftMotorForwardsPin].SoftPwmValue = 0;
            Pi.Gpio[Motors.RightMotorReversePin].SoftPwmValue = 0;
            Pi.Gpio[Motors.RightMotorForwardsPin].SoftPwmValue = 0;
        }

        private static void SetupMotors()
        {
            SetPwmMode(Pi.Gpio[Motors.LeftMotorForwardsPin]);
            SetPwmMode(Pi.Gpio[Motors.LeftMotorReversePin]);
            SetPwmMode(Pi.Gpio[Motors.RightMotorReversePin]);
            SetPwmMode(Pi.Gpio[Motors.RightMotorForwardsPin]);
        }

        private static void SetPwmMode(GpioPin pin)
        {
            if (!pin.IsInSoftPwmMode)
                pin.StartSoftPwm(0, 30);
        }
    }
}