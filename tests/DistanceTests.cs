using System;
using Xunit;
using rpi.gpio.Model;

namespace rpi.gpio.Model.tests
{
    public class DistanceTests
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 171.63)]
        [InlineData(2, 343.26)]
        [InlineData(0.1, 17.163)]
        [InlineData(0.01, 1.7163)]
        public void CalculateDistanceReturnsExpected(
            decimal secs, 
            decimal expected)
        {
            var actual = Distance.CalulateDistance(secs);
            Assert.Equal(expected, actual);
        }
    }
}
