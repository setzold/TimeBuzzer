using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using zold.TimeBuzzer.Interface;

namespace zold.TimeBuzzer.Business
{
    public class SessionsFileRepository : ISessionRepository
    {
        private string _sessionsFilePath = "sessions.xml";

        public SessionsFileRepository(string filePath)
        {
            _sessionsFilePath = filePath;
        }

        public IEnumerable<ISession> LoadAllSessions()
        {
            return DeserializeFromFile(_sessionsFilePath);
        }

        public void SaveSessions(IEnumerable<ISession> sessions)
        {
            SerializeToFile(sessions, _sessionsFilePath);
        }

        private IEnumerable<ISession> DeserializeFromFile(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException(filePath);

            XmlSerializer serializer = new XmlSerializer(typeof(List<Session>));

            IEnumerable<ISession> result;

            using (FileStream fileStream = new FileStream(_sessionsFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                result = serializer.Deserialize(fileStream) as IEnumerable<ISession>;
            }

            return result;
        }

        private void SerializeToFile(IEnumerable<ISession> sessions, string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Session>));

            using (FileStream fileStream = new FileStream(_sessionsFilePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            {
                List<Session> serializableSessions = sessions.Cast<Session>().ToList();
                serializer.Serialize(fileStream, serializableSessions);
            }
        }
    }
}
