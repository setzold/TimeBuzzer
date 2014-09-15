using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zold.TimeBuzzer.Interface
{
    public interface ISessionRepository
    {
        IEnumerable<ISession> LoadAllSessions();
        void SaveSessions(IEnumerable<ISession> sessions);
    }
}
