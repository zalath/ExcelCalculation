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
            }
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
        }

        /// <summary>
        /// 按钮事件——确定选择月份，并开始运算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToCalculate_Commit_Click(object sender, RoutedEventArgs e)
        {
            DateTime dt = new DateTime();
            try
            {
                dt = Convert.ToDateTime(YearCB.Text + "-" + MonthCB.Text + "-01 01:01:01");
            }
            catch (Exception)
            {
                Anime_ErrorTip(MonthErrorLB, Convert.ToDouble(MonthErrorLB.GetValue(Canvas.LeftProperty)), Convert.ToDouble(MonthErrorLB.GetValue(Canvas.TopProperty)));
                return;
            }

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
            GetEquateList(EquateListPart, EquateEditListPart, EquateDeleteListPart);
            currentCVS = EditListCVS;
        }

        /// <summary>
        /// 按钮事件——转到算式详细内容界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EquateEditDetailBtn_Click(object sender, RoutedEventArgs e)
        {
            EquatePartList.Children.Clear();
            EquatePartEditList.Children.Clear();

            Button btn = sender as Button;
            string name = btn.Name.Substring(0,btn.Name.LastIndexOf("E"));
            string order = btn.Name.Substring(btn.Name.LastIndexOf("E") + 1);

            equateNameLB.Content = name + ":";
            equateNameLB.FontSize = 20;
            hideequateNameLB.Content = order;
            hideequateNameLB.Tag = btn.Tag;

            GetEquateParts(btn.Tag.ToString());

            Anime_CVSchange(EditListCVS, EditDetailCVS);
            Anime_Window_Resize(this);
            currentCVS = EditDetailCVS;
        }

        /// <summary>
        /// 删除指定的算式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EquateDeleteDetailBtn_Click(object sender, RoutedEventArgs e)
        {
            string name = (sender as Button).Name;
            DeleteEquateNameLB.Tag = name.Substring(name.LastIndexOf("D") + 1);
            Anime_CVSchange(EditListCVS,DeleteEquateCVS);
        }
        /// <summary>
        /// 确认删除算式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Confirm_Btn_Click(object sender, RoutedEventArgs e)
        {
            EquationConfig ec = new EquationConfig();
            ec.DeleteNode(DeleteEquateNameLB.Tag.ToString());
            Anime_CVSchangeBack(EditListCVS, DeleteEquateCVS);
            ShowIfSuccess("删除成功");
        }

        /// <summary>
        /// 取消删除算式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Cancel_Btn_Click(object sender, RoutedEventArgs e)
        {
            Anime_CVSchangeBack(EditListCVS, DeleteEquateCVS);
        }

        /// <summary>
        /// 添加新的算式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddQuateBtn_Click(object sender, RoutedEventArgs e)
        { }

        /// <summary>
        /// 显示删除算式的按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// 隐藏删除算式的按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        private void CancelChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            Anime_CVSchangeBack(EditListCVS, EditDetailCVS);
            Anime_Window_Resize_Back(this);
        }
        /// <summary>
        /// 确认修改算式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommitChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            //获取重新生成之后的算式。
            string equate = ReformEquate();
            if (Check_Reformed_Equate(equate))
            {
                EquationConfig ec = new EquationConfig();
                ec.ChangeNodeValue(hideequateNameLB.Content.ToString(), "equate", equate);
                Reload_Equate_List();
                Anime_Window_Resize_Back(this);
                Anime_CVSchangeBack(EditListCVS, EditDetailCVS);
                ShowIfSuccess("修改成功");
            }
        }
        /// <summary>
        /// 显示修改过程中出现的算式错误
        /// </summary>
        /// <param name="errorMsg"></param>
        private void Show_Equate_Change_Error(string errorMsg)
        {
            equateNameLB.Visibility = Visibility.Collapsed;
            equateWarnLB.Content = errorMsg;
            equateWarnLB.Visibility = Visibility.Visible;
            Anime_ErrorTip(equateWarnLB, Convert.ToDouble(equateWarnLB.GetValue(Canvas.LeftProperty)), Convert.ToDouble(equateWarnLB.GetValue(Canvas.TopProperty)));
        }
        #endregion

    }
}
