using System;
using System.ComponentModel;
using System.Windows.Input;
using zold.TimeBuzzer.Interface;
using zold.WPF.Common.Command;
using zold.WPF.Common.ViewModel;

namespace zold.TimeBuzzer.Frontend.ViewModel
{
    public class SessionEntryViewModel : ViewModelBase, IDataErrorInfo
    {
        private ISession _session;

        private string _error;

        private bool _sessionIsRunning;

        public SessionEntryViewModel(ISession session)
        {
            _sessionIsRunning = true;
            _session = session;
        }

        public double TotalHours
        {
            get { return _session.TotalHours; }
            set
            {
                _session.TotalHours = value;

                TimeSpan addedTime = TimeSpan.FromHours(_session.TotalHours);
                EndTime = _session.StartTime.Add(addedTime);
                RaiseOnPropertyChanged(() => TotalHours);
            }
        }

        public string Description
        {
            get { return _session.Description; }
            set
            {
                _session.Description = value;
                RaiseOnPropertyChanged(() => Description);
            }
        }

        public TimeSpan StartTime
        {
            get { return _session.StartTime; }
            set
            {
                _session.StartTime = value;
                RaiseOnPropertyChanged(() => StartTime);
            }
        }

        public DateTime SessionDate
        {
            get { return _session.Date; }
            set
            {
                _session.Date = value;
                RaiseOnPropertyChanged(() => SessionDate);
            }
        }

        public TimeSpan? EndTime
        {
            get { return _session.EndTime; }
            set
            {
                _session.EndTime = value;
                RaiseOnPropertyChanged(() => EndTime);
            }
        }
                
        public void RefreshViewModel()
        {
            foreach (var property in this.GetType().GetProperties())
                if (property.GetSetMethod(false) != null)
                    RaiseOnPropertyChanged(property.Name);


            _sessionIsRunning = false;
        }

        public string Error
        {
            get { return _error; }
        }

        public string this[string columnName]
        {
            get
            {
                _error = null;
                switch (columnName)
                {
                    case "TotalHours":
                        if (TotalHours.ToString().Length > 5)
                            _error = "Die Anzahl der Stunden erlaubt maximal 2 Stellen nach dem Komma.";
                        break;
                }

                return _error;
            }
        }
    }
}
