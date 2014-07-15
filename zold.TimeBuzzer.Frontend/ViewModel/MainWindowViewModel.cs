using System.Windows.Input;
using zold.WPF.Common.Command;
using zold.WPF.Common.ViewModel;

namespace zold.TimeBuzzer.Frontend.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private const string BuzzerTitleRun ="RUN";
        private const string BuzzerTitleStop ="STOP";

        private string _buzzerTitle;

        private ICommand _buzzerClickCommand;

        
        private bool _trackTime;

        public MainWindowViewModel()
        {
            _buzzerTitle = BuzzerTitleRun;
            _buzzerClickCommand = new RelayCommand(OnBuzzerClick);
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
        
        public ICommand BuzzerClickCommand
        {
            get { return _buzzerClickCommand; }
        }

        private void OnBuzzerClick(object context)
        {
            _trackTime = !_trackTime;
            BuzzerTitle = _trackTime ? BuzzerTitleStop : BuzzerTitleRun;
        }
    }
}
