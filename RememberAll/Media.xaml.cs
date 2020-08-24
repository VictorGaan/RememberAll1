using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace RememberAll
{
    /// <summary>
    /// Логика взаимодействия для Media.xaml
    /// </summary>
    public partial class Media : Window
    {
        DispatcherTimer timer;

        public Media()
        {
            InitializeComponent();
            media.LoadedBehavior = MediaState.Manual;
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += Timer_Tick;

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (media.Source != null)
            {
                if (media.NaturalDuration.HasTimeSpan)
                {
                    TbTime.Text = $"{media.Position:mm\\:ss}/{media.NaturalDuration.TimeSpan:mm\\:ss}";
                }
            }
        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                media.Source = new Uri(openFileDialog.FileName);
                mediaSlider.Value = media.Volume;
            }
        }

        private void BtnPlay_Click(object sender, RoutedEventArgs e)
        {
            media.Play();
            timer.Start();
        }

        private void BtnPause_Click(object sender, RoutedEventArgs e)
        {
            if (media.CanPause)
            {
                media.Pause();
                timer.Stop();
            }
        }

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            media.Stop();
            timer.Stop();
        }

        private void mediaSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (media.Source != null)
            {
                media.Volume = e.NewValue;
            }
        }
    }
}
