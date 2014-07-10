using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using zold.TimeBuzzer.Interface;
using System.Threading;

namespace zold.TimeBuzzer.Business.Tests
{
    [TestClass]
    public class SessionManagerTests
    {

        [TestMethod]
        public void Test_SessionManagerMethods()
        {
            SessionManager testSessionManger = new SessionManager();

            
            ISession testSession = testSessionManger.Start();

            Thread.Sleep(10000);

            testSessionManger.Stop(testSession);
            testSessionManger.EditDescription(testSession, "done my test");

            Assert.IsNotNull(testSession);
            Assert.AreNotEqual(DateTime.MinValue, testSession.StartTime);
            Assert.AreNotEqual(DateTime.MinValue, testSession.EndTime);
            Assert.AreEqual("done my test", testSession.Description);
        }
    }
}
