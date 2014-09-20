using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using zold.TimeBuzzer.Business;
using zold.TimeBuzzer.Interface;

namespace zold.TimeBuzzer.Frontend.Controller
{
    public class SessionRepositoryController : IDisposable
    {
        private ILog _logger;
        private static string _sessionsFileName = "sessions.xml";
        private IList<ISession> _sessions;
        private ISessionRepository _sessionRepository;
        private bool _disposing;

        public SessionRepositoryController()
        {
            _logger = LogManager.GetLogger(GetType());
            _sessionRepository = new SessionsFileRepository(_sessionsFileName);
            _sessions = new List<ISession>();
        }

        public IEnumerable<ISession> Sessions
        {
            get { return _sessions; }
            private set { _sessions = value.ToList(); }
        }

        public void Init()
        {
            LogMethod(() =>
                {
                    if (!File.Exists(_sessionsFileName))
                    {
                        _logger.WarnFormat("Sessions file ({0}) doesn´t exist.", _sessionsFileName);
                        return;
                    }

                    try
                    {
                        _sessions = _sessionRepository.LoadAllSessions().ToList();
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("Error on Init.", ex);
                    }

                    int count = _sessions != null ? _sessions.Count : 0;
                    _logger.DebugFormat("{0} sessions found");
                });
        }

        public void Save()
        {
            LogMethod(() =>
                   {
                       _sessionRepository.SaveSessions(_sessions);

                       if (_disposing)
                       {
                           _logger.Warn("Already disposed. No externa save processes are allowed.");
                           return;
                       }
                           
                       InternalSave();
                   });
        }

        private void InternalSave()
        {
            LogMethod(() =>
                {
                    if (_sessions != null)
                        _sessionRepository.SaveSessions(_sessions);
                });
        }

        public void Dispose()
        {
            LogMethod(() =>
               {
                   _disposing = true;
                   InternalSave();

                   if (_sessions != null)
                       _sessions.Clear();
               });
        }

        private void LogMethod(Action action)
        {
            string methodName = new StackFrame(1).GetMethod().Name;
            _logger.Debug(methodName + " - enter");

            action();

            _logger.Debug(methodName + " - leave");
        }
    }
}
