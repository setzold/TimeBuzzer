using System.Windows;
using zold.TimeBuzzer.Frontend.ViewModel;

namespace zold.TimeBuzzer.Frontend.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //handle close event in viewmodel
            Application.Current.MainWindow.Closing += ((MainWindowViewModel)DataContext).OnMainWindowClosing;
        }

    }
}
