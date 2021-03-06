﻿using log4net;
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
            _currentSession.Date = DateTime.Now;
            _currentSession.StartTime = _currentSession.Date.TimeOfDay;

            return _currentSession;
        }

        public void Stop()
        {
            _currentSession.EndTime = DateTime.Now.TimeOfDay;

            _currentSession.TotalHours = Math.Round((_currentSession.EndTime.Value.Subtract(_currentSession.StartTime)).TotalHours, 2, MidpointRounding.ToEven);
            
            _sessionRuns = false;
        }

        public void EditDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentNullException("description");
            
            _currentSession.Description = description ?? string.Empty;
        }
    }
}
