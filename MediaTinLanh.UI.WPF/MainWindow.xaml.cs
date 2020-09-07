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
using System.Windows.Threading;

namespace MediaTinLanh.UI.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            StartCloseTimer();
        }

        private void StartCloseTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3d);
            timer.Tick += TimerTick;
            timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            try
            {
                DispatcherTimer timer = (DispatcherTimer)sender;
                if (timer.Interval == TimeSpan.Zero)
                {
                    timer.Stop();
                    TrinhChieuWindow trinhChieu = new TrinhChieuWindow();
                    trinhChieu.Owner = this;
                    this.Hide(); // not required if using the child events below
                    trinhChieu.ShowDialog();
                }

                timer.Interval = timer.Interval.Add(TimeSpan.FromSeconds(-1));
            }
            catch
            {

            }

        }
    }
}
