namespace rpi.gpio.Model
{
    public class Status
    {
        public string Info { get; private set; }

        public Status(string info)
        {
            Info = info;
        }
    }

}