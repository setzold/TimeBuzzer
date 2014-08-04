using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace zold.TimeBuzzer.Frontend.Tray
{
    public class TrayIcon : IDisposable
    {
        IDictionary<string, Icon> _iconCache;
        
        NotifyIcon _notifyIcon;

        Action _onMouseClickAction;
        Action _onMouseDoubleClickAction;

        private AutoResetEvent _waitForDoubleClick;

        private bool _doubleClickOccured;

        private Func<ImageSource> _getWindowIcon;

        private Stopwatch _stopWatch; 
        
        public TrayIcon(Func<ImageSource> getWindowIcon, Action onMouseDoubleClick, Action onMouseClick)
        {
            _iconCache = new Dictionary<string, Icon>();


            _onMouseClickAction = onMouseClick;
            _onMouseDoubleClickAction = onMouseDoubleClick;

            _notifyIcon = new NotifyIcon();
            _notifyIcon.MouseClick += _notifyIcon_MouseClick;
            _notifyIcon.MouseDoubleClick += _notifyIcon_MouseDoubleClick;

            _notifyIcon.Visible = true;

            _waitForDoubleClick = new AutoResetEvent(false);
            _getWindowIcon = getWindowIcon;

            _stopWatch = new Stopwatch();

            Icon icon = GetIcon();
            
            _notifyIcon.Icon = icon;
        }

        void _notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            _doubleClickOccured = true;
            _waitForDoubleClick.Set();

            _onMouseDoubleClickAction();
        }

        void _notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            
            _stopWatch.Stop();

            var elapsedTime = _stopWatch.ElapsedMilliseconds;

            //when elapsed time is greater zero, an double click event occured
            if(elapsedTime > 0) 
            {
                _stopWatch.Reset();
                return;
            }

            _waitForDoubleClick.WaitOne(SystemInformation.DoubleClickTime);

            if (_doubleClickOccured)
            {
                _doubleClickOccured = false;
                _stopWatch.Restart();
            }
            else
            {
                _onMouseClickAction();
                _stopWatch.Reset();
            }
        }

        public void WindowStateChanged()
        {
            Icon icon = GetIcon();

            if (_notifyIcon.Icon != icon)
                _notifyIcon.Icon = icon;
        }

        private Icon GetIcon()
        {
            ImageSource imageSource = _getWindowIcon();
            string iconUri =((BitmapImage)imageSource).UriSource.AbsoluteUri;
            
            Icon result = GetIcon(iconUri);
            return result;
        }

        private Icon GetIcon(string iconUri)
        {
            Icon result = null;

            if (!_iconCache.TryGetValue(iconUri, out result))
            {

                Bitmap bmp = new Bitmap(System.Windows.Application.GetResourceStream(new Uri(iconUri)).Stream);
                result = Icon.FromHandle(bmp.GetHicon());

                _iconCache[iconUri] = result;
            }

            return result;
        }

        public void Dispose()
        {
            _notifyIcon.MouseClick -= _notifyIcon_MouseClick;
            _notifyIcon.MouseDoubleClick -= _notifyIcon_MouseDoubleClick;

            if (_stopWatch != null)
                _stopWatch.Stop();

            if (_notifyIcon != null)
                _notifyIcon.Dispose();
        }
    }
}
