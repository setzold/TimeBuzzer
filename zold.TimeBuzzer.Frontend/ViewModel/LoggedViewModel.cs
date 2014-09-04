using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using zold.WPF.Common.ViewModel;

namespace zold.TimeBuzzer.Frontend.ViewModel
{
    /// <summary>
    /// Represents a base class for viewmodel with integrated logging
    /// </summary>
    public class LoggedViewModel : ViewModelBase
    {
        protected ILog Logger {get; private set;}

        public LoggedViewModel()
        {
            Logger = LogManager.GetLogger(this.GetType());
        }
    }
}
