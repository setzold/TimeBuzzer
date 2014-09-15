using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using zold.TimeBuzzer.Interface;

namespace zold.TimeBuzzer.Business.Tests
{
    [TestClass]
    public class SessionsFileRepositoryTests
    {
        private static IList<ISession> _testSessions;
        private string _fileName = "testSessions.xml";

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _testSessions = new List<ISession>();
            _testSessions.Add(new Session
            {
                Date = new DateTime(2014, 8, 24),
                Description = "Test session 1",
                StartTime = new TimeSpan(12, 00, 05),
                EndTime = new TimeSpan(12, 30, 05),
                TotalHours = 0.5
            });

            _testSessions.Add(new Session
            {
                Date = new DateTime(2014, 8, 25),
                Description = "Test session 2",
                StartTime = new TimeSpan(8, 00, 24),
                EndTime = new TimeSpan(9, 30, 24),
                TotalHours =1.5
            });
        }

        [TestInitialize]
        public void Initialize()
        {
            try
            {
                File.Delete(_fileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception on initialize: {0}", ex);
            }
        }

        [TestMethod]
        public void Test_SaveSessions()
        {
            var testobject = new SessionsFileRepository(_fileName);
            testobject.SaveSessions(_testSessions);

            Assert.IsTrue(File.Exists(_fileName));
            Assert.IsTrue(new FileInfo(_fileName).Length > 0);
        }

        [TestMethod]
        public void Test_LoadSavedSessions()
        {
            var testobject = new SessionsFileRepository(_fileName);
            testobject.SaveSessions(_testSessions);

            var savedSessions = testobject.LoadAllSessions();

            Assert.AreNotEqual(_testSessions, savedSessions);

            Assert.AreEqual(_testSessions.Count, savedSessions.Count());
            Assert.AreEqual(_testSessions.First().Date, savedSessions.First().Date);
            Assert.AreEqual(_testSessions.First().Description, savedSessions.First().Description);
            Assert.AreEqual(_testSessions.Last().Description, savedSessions.Last().Description);

        }
    }
}
