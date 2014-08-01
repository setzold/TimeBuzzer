using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zold.TimeBuzzer.Business.Tests
{
    /// <summary>
    /// Class represents a container testing any stuff that is not assigned to existing business logic of the application.
    /// </summary>
    [TestClass]
    public class StuffTests
    {
        [TestMethod]
        public void Test_TimeSpan_Stuff()
        {
            //Test TimeSpan
            TimeSpan time = new TimeSpan(12, 18, 02);

            DateTime checkedDateTime = new DateTime(2014, 07, 01, 12, 18, 02);

            Assert.AreEqual(checkedDateTime.TimeOfDay, time, "Expected TimeofDay not equal");
            Assert.AreEqual(checkedDateTime.ToString("hh:mm:ss"), time.ToString(), "Expected datetime string not equal");


            TimeSpan startTime = time;
            double hours = 25.5;

            TimeSpan endTime = startTime.Add(TimeSpan.FromHours(hours));

            Assert.AreEqual(13, endTime.Hours, "Expected added timespan is wrong");


        }
    }
}
