using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Chromium;
using Chromium.Event;
using Chromium.Remote;
using Chromium.Remote.Event;
using Chromium.WebBrowser;

namespace SuperChromium.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.browser.Click += Browser_Click;
            
        }

        private void Browser_Click(object sender, EventArgs e)
        {
            this.browser.LoadUrl("http://www.baidu.com");

            CfxWindowInfo windowInfo = new CfxWindowInfo();

            windowInfo.Style = Chromium.WindowStyle.WS_OVERLAPPEDWINDOW | Chromium.WindowStyle.WS_CLIPCHILDREN | Chromium.WindowStyle.WS_CLIPSIBLINGS | Chromium.WindowStyle.WS_VISIBLE;
            windowInfo.ParentWindow = IntPtr.Zero;
            windowInfo.WindowName = "Dev Tools";
            windowInfo.X = 200;
            windowInfo.Y = 200;
            windowInfo.Width = 800;
            windowInfo.Height = 600;

            if (this.browser.Created)
            {
               
                this.browser.BrowserHost.ShowDevTools(windowInfo, new CfxClient(), new CfxBrowserSettings(), null);
            }
        }

    }
}
