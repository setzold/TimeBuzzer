using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using zold.TimeBuzzer.Frontend.Tray;

namespace zold.TimeBuzzer.Frontend.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TrayIcon _tray;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _tray = new TrayIcon(this, OnTrayMouseDoubleClick, OnTrayMouseClick);

        }

        private void OnTrayMouseClick()
        {
            MessageBox.Show("Tray clicked");
        }


        private void OnTrayMouseDoubleClick()
        {
            MessageBox.Show("Tray double clicked");
        }
    }
}
