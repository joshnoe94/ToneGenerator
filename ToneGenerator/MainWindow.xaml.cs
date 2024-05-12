using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Resources;
using System.Windows.Shapes;
using ToneGenerator.AudioSource;

namespace ToneGenerator
{
	/// <summary>
	/// MainWindow.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class MainWindow : Window
	{
		public ToneAudioRenderer ToneAudioRenderer
		{
			get { return (ToneAudioRenderer)GetValue(ToneAudioRendererProperty); }
			set { SetValue(ToneAudioRendererProperty, value); }
		}

        private System.Windows.Forms.NotifyIcon NotifyIcon;

		public static readonly DependencyProperty ToneAudioRendererProperty = DependencyProperty.Register(
			"ToneAudioRenderer", typeof(ToneAudioRenderer), typeof(MainWindow), new PropertyMetadata(null));

        public MainWindow()
        {
            InitializeComponent();
            ToneAudioRenderer = new ToneAudioRenderer();

            NotifyIcon = new System.Windows.Forms.NotifyIcon();
            Uri iconUri = new Uri("/icon.ico", UriKind.Relative);
            NotifyIcon.Icon = new System.Drawing.Icon(Application.GetResourceStream(iconUri).Stream);

            NotifyIcon.MouseDoubleClick +=
                new System.Windows.Forms.MouseEventHandler(NotifyIcon_MouseDoubleClick);
        }


        void NotifyIcon_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.WindowState = WindowState.Normal;
            NotifyIcon.Visible = false;
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                Console.WriteLine("Disabled Show In Taskbar");
                //MyNotifyIcon.BalloonTipTitle = "Minimize Sucessful";
                //MyNotifyIcon.BalloonTipText = "Minimized the app ";
                //MyNotifyIcon.ShowBalloonTip(400);
                NotifyIcon.Visible = true;
            }
        }
		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);

			ToneAudioRenderer?.Dispose();
		}
	}
}
