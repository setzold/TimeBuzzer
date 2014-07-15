using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using zold.TimeBuzzer.Interface;

namespace zold.TimeBuzzer.Business
{
    public class SessionManager
    {
        private bool _sessionRuns;
        private ISession _currentSession;

        public ISession Start()
        {
            if (_sessionRuns)
                return _currentSession;

            _sessionRuns = true;

            _currentSession = new Session();
            _currentSession.StartTime = DateTime.Now;

            return _currentSession;
        }

        public void Stop()
        {
            if (_currentSession == null)
                throw new NullReferenceException("_currentSession");


            _currentSession.EndTime = DateTime.Now;

            _sessionRuns = false;
        }

        public void EditDescription(string description)
        {
            if (_currentSession == null)
                throw new NullReferenceException("_currentSession");


            _currentSession.Description = description ?? string.Empty;
        }
    }
}
