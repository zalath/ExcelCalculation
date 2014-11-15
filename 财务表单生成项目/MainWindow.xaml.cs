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
using SheetGenerator.BLL;


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
            GenerateYearList();
        }
        private void GenerateYearList()
        {
            for (int i = DateTime.Now.Year - 3; i <= DateTime.Now.Year + 3; i++)
            {
                YearCB.Items.Add(CreatePadPart.CreateLabel("", i.ToString(),Colors.Black));
            }
            MonthCB.Text = DateTime.Now.Month.ToString();
            YearCB.Text = DateTime.Now.Year.ToString();
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
        //计算部分
        private void ToCalculate_Click(object sender, RoutedEventArgs e)
        {
            anime_CVSchange(stCVS, calGetDateCVS);
        }
        private void CalCancel_Click(object sender, RoutedEventArgs e)
        {
            anime_CVSchangeBack(calGetDateCVS,stCVS);
        }
        private void ToCalculate_Commit_Click(object sender, RoutedEventArgs e)
        {
            DateTime dt = new DateTime();
            try
            {
                dt = Convert.ToDateTime(YearCB.Text + "-" +MonthCB.Text + "-01 01:01:01");
            }
            catch (Exception)
            {
                anime_ErrorTip(MonthErrorLB);
                return;
            }

            FileConfig fConfig = new FileConfig();
            List<string> bankList = fConfig.GetBankList();

            for (int i = 0; i < bankList.Count; i++)
            {
                BankListPart.Children.Add(CreatePadPart.CreateLabel(bankList[i], (i + 1) + " : " + bankList[i], Colors.White));
            }
            MainPart mp = new MainPart(this, BankListPart, this.Dispatcher,dt,bankList,BankResultPart);
            Thread calculate = new Thread(new ThreadStart(mp.Calculate));
            calculate.SetApartmentState(ApartmentState.STA);
            calculate.Start();
            anime_CVSchange(calGetDateCVS, calculateCVS);
        }

        //编辑算式部分
        private void EditEquate_Click(object sender, RoutedEventArgs e)
        {
            anime_CVSchange(stCVS, calculateCVS);
            Thread waiting = new Thread(new ThreadStart(wait));
            waiting.Start();
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
                this.Dispatcher.BeginInvoke((ThreadStart)delegate() { });
                Thread.Sleep(500);
                wrd += ".";
            }
        }

        private void ShowErrorBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            anime_VCS_aside(calculateCVS);
            ShowErrorBtn.Content = "结果";
            ShowErrorBtn.MouseEnter -= ShowErrorBtn_MouseEnter;
            ShowErrorBtn.MouseEnter += ShowErrorBtn_MouseEnter_Back;
        }
        private void ShowErrorBtn_MouseEnter_Back(object sender, MouseEventArgs e)
        {
            anime_VCS_aside_Back(calculateCVS);
            ShowErrorBtn.Content = "错误";
            ShowErrorBtn.MouseEnter -= ShowErrorBtn_MouseEnter_Back;
            ShowErrorBtn.MouseEnter += ShowErrorBtn_MouseEnter;
        }
    }
}
