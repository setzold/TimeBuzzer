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
        private string _editStartTime;

        private ICommand _editCommand;

        private bool _isEditMode;

        private bool _sessionIsRunning;

        public SessionEntryViewModel(ISession session)
        {
            _sessionIsRunning = true;
            _session = session;
            _editStartTime = _session.StartTime.Date.ToString("dd.MM.yyyy");
            _editCommand = new RelayCommand(OnEditCommand);
        }

        public double TotalHours
        {
            get { return _session.TotalHours; }
            set
            {
                _session.TotalHours = value;
                EndTime = _session.StartTime.AddHours(_session.TotalHours);
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

        public DateTime StartTime
        {
            get { return _session.StartTime; }
            set
            {
                _session.StartTime = value;
                RaiseOnPropertyChanged(() => StartTime);
            }
        }

        public string EditStartTime
        {
            get { return _editStartTime; }
            set
            {
                _editStartTime = value;

                DateTime result;
                if (DateTime.TryParse(_editStartTime, out result))
                    StartTime = result;


                RaiseOnPropertyChanged(() => EditStartTime);
            }
        }

        public DateTime? EndTime
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
                        if (!string.IsNullOrWhiteSpace(_editStartTime) && !DateTime.TryParse(_editStartTime, out dateResult))
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
                EditStartTime = _session.StartTime.Date.ToString("dd.MM.yyyy");
        }
    }
}
