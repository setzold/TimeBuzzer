using System;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using zold.TimeBuzzer.Business;
using zold.WPF.Common.Command;
using zold.WPF.Common.ViewModel;

namespace zold.TimeBuzzer.Frontend.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private const string BuzzerTitleRun ="RUN";
        private const string BuzzerTitleStop ="STOP";

        private string _buzzerTitle;

        private SessionEntriesViewModel _sessionEntriesViewModel;

        private ICommand _buzzerClickCommand;

        private ImageSource _greenBuzzerIcon;
        private ImageSource _redBuzzerIcon;

        private bool _isTrackingTime;

        public bool TimeIsTracking
        {
            get { return _isTrackingTime; }
        }

        public MainWindowViewModel()
        {
            _buzzerTitle = BuzzerTitleRun;
            _buzzerClickCommand = new RelayCommand(OnBuzzerClick);
            _sessionEntriesViewModel = new SessionEntriesViewModel();
            _greenBuzzerIcon = new BitmapImage(new Uri("pack://application:,,,/Resource/Images/BuzzerIcon_green.png"));
            _redBuzzerIcon = new BitmapImage(new Uri("pack://application:,,,/Resource/Images/BuzzerIcon.png")); 
        }

        public string WindowTitle
        {
            get { return "Time Buzzer"; }
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

        private void OnBuzzerClick(object context)
        {
            _isTrackingTime = !_isTrackingTime;
            BuzzerTitle = _isTrackingTime ? BuzzerTitleStop : BuzzerTitleRun;

            if (_isTrackingTime)
                _sessionEntriesViewModel.AddSession();
            else
                _sessionEntriesViewModel.StopSession();

            RaiseOnPropertyChanged(() => TimeIsTracking);
            RaiseOnPropertyChanged(() => WindowIcon);
        }
    }
}
