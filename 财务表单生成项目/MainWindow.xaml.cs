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
        #region 主窗口调用部分

        private Canvas currentCVS = new Canvas();
        private void GenerateYearList()
        {
            for (int i = DateTime.Now.Year - 3; i <= DateTime.Now.Year + 3; i++)
            {
                YearCB.Items.Add(CreateLabel("", i.ToString(),Colors.Black));
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
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Anime_CVSchangeBack(currentCVS, stCVS);
            Anime_Window_Resize_Back(this);
            Anime_BackBtn_Hide(BackButton);
            BackButton.IsEnabled = false;
        }
        #endregion

        #region 计算面板部分
        private void ToCalculate_Click(object sender, RoutedEventArgs e)
        {
            Anime_BackBtn_Show(BackButton);
            Anime_CVSchange(stCVS, calGetDateCVS);
            currentCVS = calGetDateCVS;
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
                Anime_ErrorTip(MonthErrorLB);
                return;
            }

            FileConfig fConfig = new FileConfig();
            List<string> bankList = fConfig.GetBankList();

            for (int i = 0; i < bankList.Count; i++)
            {
                BankListPart.Children.Add(CreateLabel(bankList[i], (i + 1) + " : " + bankList[i], Colors.White));
            }
            CalPart mp = new CalPart(this, BankListPart, this.Dispatcher,dt,bankList,BankResultPart);
            Thread calculate = new Thread(new ThreadStart(mp.Calculate));
            calculate.SetApartmentState(ApartmentState.STA);
            calculate.Start();
            Anime_CVSchange(calGetDateCVS, calculateCVS);
            currentCVS = calculateCVS;
        }

        private void ShowErrorBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            Anime_VCS_aside(calculateCVS);
            ShowErrorBtn.Content = "结果";
            ShowErrorBtn.MouseEnter -= ShowErrorBtn_MouseEnter;
            ShowErrorBtn.MouseEnter += ShowErrorBtn_MouseEnter_Back;
        }
        private void ShowErrorBtn_MouseEnter_Back(object sender, MouseEventArgs e)
        {
            Anime_VCS_aside_Back(calculateCVS);
            ShowErrorBtn.Content = "错误";
            ShowErrorBtn.MouseEnter -= ShowErrorBtn_MouseEnter_Back;
            ShowErrorBtn.MouseEnter += ShowErrorBtn_MouseEnter;
        }
        #endregion

        #region 编辑算式部分
        private void EditEquate_Click(object sender, RoutedEventArgs e)
        {
            Anime_BackBtn_Show(BackButton);

            EquateListPart.Children.Clear();
            EquateEditListPart.Children.Clear();
            GetEquateList(EquateListPart, EquateEditListPart, EquateDeleteListPart);
            
            Anime_CVSchange(stCVS, EditListCVS);
            currentCVS = EditListCVS;
        }

        private void EquateEditDetailBtn_Click(object sender, RoutedEventArgs e)
        {
            EquatePartList.Children.Clear();

            Button btn = sender as Button;
            equateNameLB.Content = btn.Name + ":";
            equateNameLB.FontSize = 20;
            hideequateNameLB.Content = btn.Name;
            
            GetEquateParts(btn.Tag.ToString());

            Anime_CVSchange(EditListCVS, EditDetailCVS);
            Anime_Window_Resize(this);
            currentCVS = EditDetailCVS;
        }
        private void AddQuateBtn_Click(object sender, RoutedEventArgs e)
        { }
        private void ShowDeleteQuateBtn_Click(object sender, RoutedEventArgs e)
        {
            EquateEditListPart.IsEnabled = false;
            EquateEditListPart.Visibility = Visibility.Collapsed;
            EquateDeleteListPart.IsEnabled = true;
            EquateDeleteListPart.Visibility = Visibility.Visible;
            DeleteEquateButton.Content = "修改";
            DeleteEquateButton.Click -= ShowDeleteQuateBtn_Click;
            DeleteEquateButton.Click += HideDeleteQuateBtn_Click;
        }
        private void HideDeleteQuateBtn_Click(object sender, RoutedEventArgs e)
        {
            EquateDeleteListPart.IsEnabled = false;
            EquateDeleteListPart.Visibility = Visibility.Collapsed;
            EquateEditListPart.IsEnabled = true;
            EquateEditListPart.Visibility = Visibility.Visible;
            DeleteEquateButton.Content = "删除";
            DeleteEquateButton.Click -= HideDeleteQuateBtn_Click;
            DeleteEquateButton.Click += ShowDeleteQuateBtn_Click;
        }
        private void DeleteQuateFinalBtn_Click(object sender, RoutedEventArgs e)
        {
            string equateName = (sender as Button).Name;
        }
        #endregion
    }
}
