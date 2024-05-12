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
        private System.Windows.Forms.NotifyIcon NotifyIcon;

        public double MinimumFrequency { get; private set; } = Double.Parse(System.Configuration.ConfigurationManager.AppSettings["MinimumFrequency"]);
        public double MaximumFrequency { get; private set; } = Double.Parse(System.Configuration.ConfigurationManager.AppSettings["MaximumFrequency"]);
        public bool MinimizeTray { get; private set; } = Boolean.Parse(System.Configuration.ConfigurationManager.AppSettings["MinimizeTray"]);

        public ToneAudioRenderer ToneAudioRenderer
		{
			get { return (ToneAudioRenderer)GetValue(ToneAudioRendererProperty); }
			set { SetValue(ToneAudioRendererProperty, value); }
		}

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
            WindowState = WindowState.Normal;
            NotifyIcon.Visible = false;
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (MinimizeTray && WindowState == WindowState.Minimized)
            {
                ShowInTaskbar = false;
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
