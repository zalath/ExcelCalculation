using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Diagnostics;


namespace SheetGenerator
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
            this.Close();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void firstLvAccountBalancing_Click(object sender, RoutedEventArgs e)
        {
            Animation.anime_virticleMove_show(stCVS, -200, waitCVS, 0);
            firstBtn.Click -= firstLvAccountBalancing_Click;
            Thread waiting = new Thread(new ThreadStart(wait));
            //Thread ththird = new Thread(new ThreadStart(firstLv));
            waiting.Start();
            //ththird.Start();
        }
        private void wait()
        {
            string wrd = ".";
            for (int i = 0; ; i++)
            {
                if (i % 6 == 0)
                {
                    wrd = "";
                }
                this.Dispatcher.BeginInvoke((ThreadStart)delegate() { waitlabel.Content = wrd; });
                Thread.Sleep(500);
                wrd += ".";
            }
        }
    }
}
