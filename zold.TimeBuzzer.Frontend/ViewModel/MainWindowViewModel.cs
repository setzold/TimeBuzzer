using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using zold.TimeBuzzer.Frontend.Controller;
using zold.TimeBuzzer.Frontend.Tray;
using zold.WPF.Common.Command;

namespace zold.TimeBuzzer.Frontend.ViewModel
{
    public class MainWindowViewModel : LoggedViewModel, IDisposable
    {
        private const string BuzzerTitleRun = "RUN";
        private const string BuzzerTitleStop = "STOP";

        private string _buzzerTitle;

        private SessionRepositoryController _sessionRepositoryController;
        private SessionEntriesViewModel _sessionEntriesViewModel;

        private ICommand _buzzerClickCommand;

        private ImageSource _greenBuzzerIcon;
        private ImageSource _redBuzzerIcon;

        private TrayIcon _tray;

        private bool _isInitialized;

        private bool _isTrackingTime;

        public bool TimeIsTracking
        {
            get { return _isTrackingTime; }
        }

        public MainWindowViewModel()
        {
            _buzzerTitle = BuzzerTitleRun;

            _sessionRepositoryController = new SessionRepositoryController();

            _buzzerClickCommand = new RelayCommand(OnBuzzerClick, CanExecuteBuzzerClickCommand);
            _sessionEntriesViewModel = new SessionEntriesViewModel();
            _greenBuzzerIcon = new BitmapImage(new Uri("pack://application:,,,/Resource/Images/BuzzerIcon_green.png"));
            _redBuzzerIcon = new BitmapImage(new Uri("pack://application:,,,/Resource/Images/BuzzerIcon.png"));

            _tray = new TrayIcon(GetWindowIcon, OnTrayMouseDoubleClick, OnTrayMouseClick);

            InitializeSessionsAsync();
        }

        public string WindowTitle
        {
            get { return "Time Buzzer"; }
        }

        public ImageSource GetWindowIcon()
        {
            return WindowIcon;
        }

        public ImageSource WindowIcon
        {
            get
            {
                if (_isTrackingTime)
                    return _redBuzzerIcon;

                return _greenBuzzerIcon;
            }
        }

        public string BuzzerTitle
        {
            get { return _buzzerTitle; }
            set
            {
                _buzzerTitle = value;
                RaiseOnPropertyChanged(() => BuzzerTitle);
            }
        }

        public SessionEntriesViewModel SessionEntriesViewModel
        {
            get { return _sessionEntriesViewModel; }
            set { _sessionEntriesViewModel = value; }
        }

        public ICommand BuzzerClickCommand
        {
            get { return _buzzerClickCommand; }
        }

        public bool CanExecuteBuzzerClickCommand(object ctx)
        {
            return _isInitialized;
        }

        private void OnBuzzerClick(object context)
        {
            Logger.Debug("OnBuzzerClick - enter");

            _isTrackingTime = !_isTrackingTime;
            BuzzerTitle = _isTrackingTime ? BuzzerTitleStop : BuzzerTitleRun;

            if (_isTrackingTime)
                _sessionEntriesViewModel.AddSession();
            else
                _sessionEntriesViewModel.StopSession();

            RaiseOnPropertyChanged(() => TimeIsTracking);
            RaiseOnPropertyChanged(() => WindowIcon);

            _tray.WindowStateChanged();

            Logger.Debug("OnBuzzerClick - leave");
        }

        private void OnTrayMouseClick()
        {
            BuzzerClickCommand.Execute(null);
        }

        private void OnTrayMouseDoubleClick()
        {
            //MessageBox.Show("Tray double clicked");
        }

        public void OnMainWindowClosing(object sender, CancelEventArgs e)
        {
            Dispose();
        }

        private void InitializeSessionsAsync()
        {
            Task initSessionsTask = Task.Factory.StartNew(() =>
            {
                if (_sessionRepositoryController == null)
                    return;

                _sessionRepositoryController.Init();
            });

            initSessionsTask.ContinueWith(OnSessionInitializingCompleted);
        }

        private void OnSessionInitializingCompleted(Task caller)
        {
            Logger.Info("Sessions initialized.");

            if (_sessionRepositoryController == null)
            {
                Logger.Error("Error on initializing sessions. Repository controller was null!");
                return;
            }

            if (_sessionRepositoryController.Sessions == null) return;
            if (_sessionEntriesViewModel == null) return;

            //Delegate to UI Thread
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                _sessionEntriesViewModel.Init(_sessionRepositoryController.Sessions);
                _isInitialized = true;
            }));
        }

        public void Dispose()
        {
            if (_tray != null)
                _tray.Dispose();

            if (_sessionRepositoryController == null) return;
            if (_sessionRepositoryController.Sessions == null) return;

            if (_sessionEntriesViewModel == null) return;
            if (_sessionEntriesViewModel.SessionEntries == null) return;

            foreach (var sessionEntry in _sessionEntriesViewModel.SessionEntries)
            {
                if (sessionEntry.Session == null) continue;

                //ignore "empty" sessions with no description
                if (string.IsNullOrWhiteSpace(sessionEntry.Session.Description)) continue;

                if (_sessionRepositoryController.Sessions.FirstOrDefault(session => session.Equals(sessionEntry.Session)) != null)
                    continue;

                _sessionRepositoryController.Sessions.Add(sessionEntry.Session);
            }

            _sessionRepositoryController.Save();
        }
    }
}
