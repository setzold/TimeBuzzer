using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace zold.TimeBuzzer.Frontend.Controller.Tests
{
    [TestClass]
    public class SessionRepositoryControllerTests
    {
        private static string TestFileName = "sessions.xml";
        
        [ClassInitialize]
        public static void Initialize(TestContext ctx)
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            if (File.Exists(TestFileName))
                File.Delete(TestFileName);
        }

        [TestMethod]
        public void Test_SessionRepositoryController_WithNoneExistingSessionsXml()
        {
            SessionRepositoryController testObject = new SessionRepositoryController();
            testObject.Init();

            Assert.AreEqual(0, testObject.Sessions.Count());
        }

        [TestMethod]
        public void Test_SessionRepositoryController_WithExistingSessionsXml()
        {
            FileStream sessionsFile = File.Create(TestFileName);
            sessionsFile.Close();
            
            SessionRepositoryController testObject = new SessionRepositoryController();
            testObject.Init();

            Assert.AreEqual(0, testObject.Sessions.Count());

            File.Delete(TestFileName);
        }

        [TestMethod]
        public void Test_SessionRepositoryController_Save()
        {
            SessionRepositoryController testObject = new SessionRepositoryController();
            testObject.Save();
            
            bool fileExist = File.Exists(TestFileName);
            Assert.IsTrue(fileExist);
        }
    }
}
