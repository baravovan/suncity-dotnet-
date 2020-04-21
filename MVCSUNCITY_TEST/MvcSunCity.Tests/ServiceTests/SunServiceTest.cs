using System;
using Xunit;
using MvcSunCity.Services;

namespace MvcSunCity.Tests.ServiceTests
{
    public class SunServiceTest
    {
        [Fact]
        public void GetDataFromServerResultNotEmpty()
        {
            string result = SunService.GetDataFromServer("36.7201600","-4.4203400","today").Result;
            Console.WriteLine(result);
            Assert.NotEmpty(result);
        }
    }
}
