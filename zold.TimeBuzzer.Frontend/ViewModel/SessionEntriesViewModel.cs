using System.Collections.ObjectModel;
using zold.TimeBuzzer.Business;
using zold.TimeBuzzer.Interface;
using zold.WPF.Common.ViewModel;

namespace zold.TimeBuzzer.Frontend.ViewModel
{
    public class SessionEntriesViewModel : ViewModelBase
    {
        private ObservableCollection<SessionEntryViewModel> _sessionEntries;
        private SessionEntryViewModel _selectedSession;


        private SessionManager _sessionManger;

        public SessionEntriesViewModel()
        {
            _sessionEntries = new ObservableCollection<SessionEntryViewModel>();
            _sessionManger = new SessionManager();
        }

        public ObservableCollection<SessionEntryViewModel> SessionEntries
        {
            get { return _sessionEntries; }
            set { _sessionEntries = value; }
        }

        public SessionEntryViewModel SelectedSession
        {
            get { return _selectedSession; }
            set { _selectedSession = value; 
            }
        }

        public void AddSession()
        {
            ISession session = _sessionManger.Start();
            _selectedSession = new SessionEntryViewModel(session);
            SessionEntries.Add(_selectedSession);
        }

        public void StopSession()
        {
            _sessionManger.Stop();
            _selectedSession.RefreshViewModel();
        }
    }
}
