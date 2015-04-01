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

        /// <summary>
        /// 定义当前正在限时的面板
        /// </summary>
        private Canvas currentCVS = new Canvas();
        /// <summary>
        /// 生成年份列表
        /// 前后3年
        /// </summary>
        private void GenerateYearList()
        {
            for (int i = DateTime.Now.Year - 3; i <= DateTime.Now.Year + 3; i++)
            {
                YearCB.Items.Add(CreateLabel("", i.ToString(), Colors.Black));
                DataYearCB.Items.Add(CreateLabel("", i.ToString(), Colors.Black));
            }
            DataYearCB.Text = DateTime.Now.Year.ToString();
            DataMonthCB.Text = DateTime.Now.Month.ToString();
            DataReportDate.Text = DateTime.Now.ToString();
            MonthCB.Text = DateTime.Now.Month.ToString();
            YearCB.Text = DateTime.Now.Year.ToString();
        }

        /// <summary>
        /// 关闭按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
            this.Close();
        }

        /// <summary>
        /// 拖拽窗口事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        /// <summary>
        /// 回到主页按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Anime_CVSchangeBack(stCVS, currentCVS);
            Anime_Window_Resize_Back(this);
            Anime_BackBtn_Hide(BackButton);
            BackButton.IsEnabled = false;
        }

        /// <summary>
        /// 显示执行结果是否成功
        /// </summary>
        /// <param name="msg"></param>
        private void ShowIfSuccess(string msg)
        {
            ResultMsgLB.Content = msg;
            Anime_Show_ResultMsg();
        }
        #endregion

        #region 计算面板部分
        /// <summary>
        /// 转到运算界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToCalculate_Click(object sender, RoutedEventArgs e)
        {
            Anime_BackBtn_Show(BackButton);
            Anime_CVSchange(stCVS, calGetDateCVS);
            currentCVS = calGetDateCVS;
            Manage_onclick("tocalculate");
        }

        /// <summary>
        /// 按钮事件——确定选择月份，并开始运算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToCalculate_Commit_Click(object sender, RoutedEventArgs e)
        {
            DateTime dt = GetChosenDate(YearCB.Text, MonthCB.Text);

            FileConfig fConfig = new FileConfig();
            List<string> bankList = fConfig.GetBankList();

            for (int i = 0; i < bankList.Count; i++)
            {
                BankListPart.Children.Add(CreateLabel(bankList[i], (i + 1) + " : " + bankList[i], Colors.White));
            }
            CalPart mp = new CalPart(this, BankListPart, this.Dispatcher, dt, bankList, BankResultPart);
            Thread calculate = new Thread(new ThreadStart(mp.Calculate));
            calculate.SetApartmentState(ApartmentState.STA);
            calculate.Start();
            Anime_CVSchange(calGetDateCVS, calculateCVS);
            currentCVS = calculateCVS;
        }

        /// <summary>
        /// 侧滑——显示运算结果部分
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowErrorBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            Anime_VCS_aside(calculateCVS);
            ShowErrorBtn.Content = "结果";
            ShowErrorBtn.MouseEnter -= ShowErrorBtn_MouseEnter;
            ShowErrorBtn.MouseEnter += ShowErrorBtn_MouseEnter_Back;
        }

        /// <summary>
        /// 侧滑——显示运算问题部分
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowErrorBtn_MouseEnter_Back(object sender, MouseEventArgs e)
        {
            Anime_VCS_aside_Back(calculateCVS);
            ShowErrorBtn.Content = "错误";
            ShowErrorBtn.MouseEnter -= ShowErrorBtn_MouseEnter_Back;
            ShowErrorBtn.MouseEnter += ShowErrorBtn_MouseEnter;
        }
        #endregion

        #region 编辑算式部分

        /// <summary>
        /// 按钮事件——转到编辑算式界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditEquate_Click(object sender, RoutedEventArgs e)
        {
            Anime_BackBtn_Show(BackButton);

            Reload_Equate_List();

            Anime_CVSchange(stCVS, EditListCVS);
        }

        private void Reload_Equate_List()
        {
            EquateListPart.Children.Clear();
            EquateEditListPart.Children.Clear();
            EquateDeleteListPart.Children.Clear();
            EquateOrderListPart.Children.Clear();
            GetEquateList();
            currentCVS = EditListCVS;
        }

        /// <summary>
        /// 按钮事件——转到算式详细内容界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EquateEditDetailBtn_Click(object sender, RoutedEventArgs e)
        {
            ShowEquateDetail_Edit(sender as Button);
        }
        private void ShowEquateDetail_Edit(Button btn)
        {
            EquatePartList.Children.Clear();
            EquatePartEditList.Children.Clear();

            string name = btn.Name.Substring(0, btn.Name.LastIndexOf("E"));
            string uni = btn.Name.Substring(btn.Name.LastIndexOf("E") + 1);

            if (uni == "N")
            {
                equateNameTB.Text = "";

                CommitChangeBtn.Click -= CommitChangeBtn_Click;
                CommitChangeBtn.Click += CommitAddBtn_Click;
                CommitChangeBtn.Content = "添加";
            }
            else
            {
                equateNameTB.Text = name;
                hideequateNameLB.Content = uni;
                hideequateNameLB.Tag = btn.Tag;
            }

            GetEquateParts(btn.Tag.ToString());

            Anime_CVSchange(EditListCVS, EditDetailCVS);
            Anime_Window_Resize(this);
            currentCVS = EditDetailCVS;
        }
        /// <summary>
        /// 显示修改过程中出现的算式错误
        /// </summary>
        /// <param name="errorMsg"></param>
        private void Show_Equate_Change_Error(string errorMsg)
        {
            equateWarnLB.Content = errorMsg;
            equateWarnLB.Visibility = Visibility.Visible;
            Anime_ErrorTip(equateWarnLB);
        }
        #endregion

        #region 累加部分
        /// <summary>
        /// 将制定文件夹下的所有同类文件进行累加。放入累加文件夹中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddUpGetdate_Click(object sender,RoutedEventArgs e)
        {
            Anime_BackBtn_Show(BackButton);
            Anime_CVSchange(stCVS, calGetDateCVS);
            currentCVS = calGetDateCVS;
            Manage_onclick("toaddup");
        }
        private void AddUp_Click(object sender, RoutedEventArgs e)
        {
            Anime_BackBtn_Show(BackButton);
            Anime_CVSchange(currentCVS, AddupCVS);
            currentCVS = AddupCVS;
            
            //getdate
            DateTime dt = GetChosenDate(YearCB.Text, MonthCB.Text);

            //get Config
            FileConfig fConfig = new FileConfig();
            List<string> bankList = fConfig.GetFileList();

            //start to add up all
            object[] obbtns = { AddUpProcessBankName_LB, AddUpProcessTableName_LB, AddUpProcessColumnName_LB, AddUpProcessType_LB };
            AddUpAll mp = new AddUpAll(this, this.Dispatcher, dt, obbtns);
            Thread calculate = new Thread(new ThreadStart(mp.Addup_all));
            calculate.SetApartmentState(ApartmentState.STA);
            calculate.Start();
        }
        #endregion

        #region 修改文件中的数据时间和上报时间

        private void AlterdateGetdate_Click(object sender, RoutedEventArgs e)
        {
            Anime_BackBtn_Show(BackButton);
            Anime_CVSchange(stCVS, calGetDateCVS);
            currentCVS = calGetDateCVS;
            Manage_onclick("toalterdate");
        }
        /// <summary>
        /// 选定文件所属的时间段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Alterdate_Click(object sender,RoutedEventArgs e)
        {
            Anime_BackBtn_Show(BackButton);
            Anime_CVSchange(currentCVS, AlterDateCVS);
            currentCVS = AlterDateCVS;
        }
        private void Alterdate_Commit_Click(object sender, RoutedEventArgs e)
        {
            Anime_BackBtn_Show(BackButton);
            Anime_CVSchange(currentCVS, AddupCVS);
            currentCVS = AddupCVS;

            //getdate
            DateTime datadt = GetChosenDate(YearCB.Text, MonthCB.Text);
            DateTime datadtnew = GetChosenDate(DataYearCB.Text, DataMonthCB.Text);
            DateTime reportdt = Convert.ToDateTime(DataReportDate.Text);
            object[] obbtns = { AddUpProcessBankName_LB, AddUpProcessTableName_LB, AddUpProcessColumnName_LB, AddUpProcessType_LB };

            AlterDate ad = new AlterDate(this.Dispatcher, datadt, datadtnew, reportdt, obbtns);
            Thread th = new Thread(new ThreadStart(ad.Alterdate));
            th.Start();
        }
        #endregion

        #region 公用部分

        /// <summary>
        /// 修改获取时间页面的提交按钮事件
        /// </summary>
        /// <param name="partName"></param>
        private void Manage_onclick(string partName)
        {
            switch (partName)
            {
                case "tocalculate":
                    CalCommitBtn.Click -= ToCalculate_Commit_Click;
                    CalCommitBtn.Click += AddUp_Click;
                    CalCommitBtn.Click -= Alterdate_Click;
                    break;
                case "toaddup":
                    CalCommitBtn.Click += ToCalculate_Commit_Click;
                    CalCommitBtn.Click -= AddUp_Click;
                    CalCommitBtn.Click -= Alterdate_Click;
                    break;
                case "toalterdate":
                    CalCommitBtn.Click -= ToCalculate_Commit_Click;
                    CalCommitBtn.Click -= AddUp_Click;
                    CalCommitBtn.Click += Alterdate_Click;
                    break;
            }
        }
        /// <summary>
        /// 获取页面指定的时间。。。
        /// </summary>
        /// <returns>时间</returns>
        private DateTime GetChosenDate(string year,string month) 
        {
            //getdate
            DateTime dt = new DateTime();
            try
            {
                dt = Convert.ToDateTime(year + "-" + month + "-01 01:01:01");
                return dt;
            }
            catch (Exception)
            {
                Anime_ErrorTip(MonthErrorLB);
                return DateTime.Now;
            }
        }
        #endregion
    }
}
