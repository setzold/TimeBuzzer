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
        private string _editSessionDate;

        private ICommand _editCommand;

        private bool _isEditMode;

        private bool _sessionIsRunning;

        public SessionEntryViewModel(ISession session)
        {
            _sessionIsRunning = true;
            _session = session;
            _editSessionDate = _session.Date.ToString("dd.MM.yyyy");
            _editCommand = new RelayCommand(OnEditCommand);
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

        public string EditSessionDate
        {
            get { return _editSessionDate; }
            set
            {
                _editSessionDate = value;

                DateTime result;
                if (DateTime.TryParse(_editSessionDate, out result))
                    SessionDate = result;


                RaiseOnPropertyChanged(() => EditSessionDate);
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

        public bool IsEditMode
        {
            get { return _isEditMode; }
            set
            {
                _isEditMode = value;
                RaiseOnPropertyChanged(() => IsEditMode);
            }
        }

        public ICommand EditCommand
        {
            get { return _editCommand; }
        }
        
        public string EditButtonToolTip
        {
            get { return "Change session date"; }
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
                    case "EditStartTime":
                        DateTime dateResult;
                        if (!string.IsNullOrWhiteSpace(_editSessionDate) && !DateTime.TryParse(_editSessionDate, out dateResult))
                            _error = "Kein richtiges Datumsformat.";
                        break;

                }

                return _error;
            }
        }

        private void OnEditCommand(object context)
        {
            if (_sessionIsRunning)
                return;

            IsEditMode = !_isEditMode;

            if (_isEditMode)
                EditSessionDate = _session.Date.ToString("dd.MM.yyyy");
        }
    }
}
