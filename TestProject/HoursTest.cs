using NationalParks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    [TestClass]
    public class HoursTest
    {
        [TestMethod]
        public void TestProperties()
        {
            var hours = new Hours
            {
                Monday = "Monday",
                Tuesday = "Tuesday",
                Wednesday = "Wednesday",
                Thursday = "Thursday",
                Friday = "Friday",
                Saturday = "Saturday",
                Sunday = "Sunday"
            };

            Assert.AreEqual(hours.Monday, "Monday");
            Assert.AreEqual(hours.Tuesday, "Tuesday");
            Assert.AreEqual(hours.Wednesday, "Wednesday");
            Assert.AreEqual(hours.Thursday, "Thursday");
            Assert.AreEqual(hours.Friday, "Friday");
            Assert.AreEqual(hours.Saturday, "Saturday");
            Assert.AreEqual(hours.Sunday, "Sunday");
        }
    }
}
