using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using zold.TimeBuzzer.Interface;

namespace zold.TimeBuzzer.Business
{
    public class SessionManager
    {
        public ISession Start()
        {
            ISession session = new Session();
            session.StartTime = DateTime.Now;
            return session;
        }

        public void Stop(ISession session)
        {
            if (session == null)
                throw new ArgumentNullException("session");

            session.EndTime = DateTime.Now;
        }

        public void EditDescription(ISession session, string description)
        {
            if (session == null)
                throw new ArgumentNullException("session");


            session.Description = description ?? string.Empty;
        }
    }
}
