using System;
using Xunit;
using rpi.gpio.Model;

namespace rpi.gpio.Model.tests
{
    public class StatusTests
    {
        [Fact]
        public void GetMotorPinStatus()
        {
            var sut = new Status("Something");
            Assert.Equal("Something", sut.Info);
        }
    }
}
