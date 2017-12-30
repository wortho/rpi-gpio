
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Gpio;

namespace rpi.gpio.Model
{
    public enum DrivingState
    {
        Stopped,
        Forwards,
        Reverse,
        LeftTurn,
        RightTurn
    }
    
    public static class Driving
    {
        private const decimal MinimumDistance = 0.20m;
        private const decimal ClearPathDistance = 1.0m;
        private const decimal ClearTurnDistance = 0.30m;

        public static DrivingState State { get; private set; }

        public static int Speed { get; private set; }

        public static void Forward(int speed)
        {
            SetupMotors();
            Pi.Gpio[Motors.LeftMotorForwardsPin].SoftPwmValue = speed;
            Pi.Gpio[Motors.LeftMotorReversePin].SoftPwmValue = 0;
            Pi.Gpio[Motors.RightMotorForwardsPin].SoftPwmValue = speed;
            Pi.Gpio[Motors.RightMotorReversePin].SoftPwmValue = 0;
            State = DrivingState.Forwards;
            Speed = speed;
        }

        public static void Reverse(int speed)
        {
            SetupMotors();
            Pi.Gpio[Motors.LeftMotorReversePin].SoftPwmValue = speed;
            Pi.Gpio[Motors.LeftMotorForwardsPin].SoftPwmValue = 0;
            Pi.Gpio[Motors.RightMotorReversePin].SoftPwmValue = speed;
            Pi.Gpio[Motors.RightMotorForwardsPin].SoftPwmValue = 0;
            State = DrivingState.Reverse;
            Speed = speed;
        }

        internal static void Left(int speed)
        {
            SetupMotors();
            Pi.Gpio[Motors.LeftMotorForwardsPin].SoftPwmValue = 0;
            Pi.Gpio[Motors.LeftMotorReversePin].SoftPwmValue = 0;
            Pi.Gpio[Motors.RightMotorForwardsPin].SoftPwmValue = (int)(speed*1.2m);
            Pi.Gpio[Motors.RightMotorReversePin].SoftPwmValue = 0;
            State = DrivingState.LeftTurn;
            Speed = speed;
        }

        
        internal static void Right(int speed)
        {
            SetupMotors();
            Pi.Gpio[Motors.LeftMotorForwardsPin].SoftPwmValue = (int)(speed*1.2m);
            Pi.Gpio[Motors.LeftMotorReversePin].SoftPwmValue = 0;
            Pi.Gpio[Motors.RightMotorForwardsPin].SoftPwmValue = 0;
            Pi.Gpio[Motors.RightMotorReversePin].SoftPwmValue = 0;
            State = DrivingState.RightTurn;
            Speed = speed;
        }

        public static void Stop()
        {
            SetupMotors();
            Pi.Gpio[Motors.LeftMotorReversePin].SoftPwmValue = 0;
            Pi.Gpio[Motors.LeftMotorForwardsPin].SoftPwmValue = 0;
            Pi.Gpio[Motors.RightMotorReversePin].SoftPwmValue = 0;
            Pi.Gpio[Motors.RightMotorForwardsPin].SoftPwmValue = 0;
            State = DrivingState.Stopped;
        }

        private static void SetupMotors()
        {
            SetPwmMode(Pi.Gpio[Motors.LeftMotorForwardsPin]);
            SetPwmMode(Pi.Gpio[Motors.LeftMotorReversePin]);
            SetPwmMode(Pi.Gpio[Motors.RightMotorReversePin]);
            SetPwmMode(Pi.Gpio[Motors.RightMotorForwardsPin]);
        }

        
        public static void CheckDistance(decimal distance)
        {            
            if (State == DrivingState.Forwards && distance <= MinimumDistance)
            {
                Driving.Stop();
                Task.Run(() => AvoidObstacle());
            }
        }

        private static void AvoidObstacle()
        {
            Backup();
            FindClearPath();
            if (Distance.CurrentDistance >= ClearPathDistance)
                Driving.Forward(Speed);
        }

        private static void FindClearPath()
        {
            var sw = Stopwatch.StartNew();
            Driving.Left(Speed);
            while (
                State == DrivingState.LeftTurn && 
                Distance.CurrentDistance >= MinimumDistance && 
                Distance.CurrentDistance <= ClearPathDistance && 
                sw.Elapsed < TimeSpan.FromSeconds(5))
            {
                Task.Delay(200);
            }
            Driving.Stop();
        }

        private static void Backup()
        {
            Driving.Reverse(Speed);
            while (State == DrivingState.Reverse && 
                Distance.CurrentDistance <= ClearTurnDistance)
            {
                Task.Delay(200);
            }
            Driving.Stop();
        }

        private static void SetPwmMode(GpioPin pin)
        {
            if (!pin.IsInSoftPwmMode)
                pin.StartSoftPwm(0, 30);
        }
    }
}