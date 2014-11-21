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
using System.Windows.Threading;
using SheetGenerator.BLL;

namespace SheetGenerator
{
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 显示修改算式排序部分
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowOrderQuateBtn_Click(object sender, RoutedEventArgs e)
        {
            EquateEditListPart.IsEnabled = false;
            EquateEditListPart.Visibility = Visibility.Collapsed;
            EquateDeleteListPart.IsEnabled = false;
            EquateDeleteListPart.Visibility = Visibility.Collapsed;
            EquateOrderListPart.IsEnabled = true;
            EquateOrderListPart.Visibility = Visibility.Visible;
            EquateListSV.Height = EquateListSV.Height - 30;
            Anime_Element_Jump(AddEquateButton);
            DeleteEquateButton.Visibility = Visibility.Collapsed;

            OrderEquateButton.Content = "取消";
            AddEquateButton.Content = "确定";

            OrderEquateButton.Click -= ShowOrderQuateBtn_Click;
            OrderEquateButton.Click += CancelOrderQuateBtn_Click;
            AddEquateButton.Click -= AddQuateBtn_Click;
            AddEquateButton.Click += CommitOrderQuateBtn_Click;
        }
        /// <summary>
        /// 取消排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelOrderQuateBtn_Click(object sender, RoutedEventArgs e)
        {
            Order_Commit_Or_Cancel();
        }
        private void Order_Commit_Or_Cancel()
        {
            EquateEditListPart.IsEnabled = true;
            EquateEditListPart.Visibility = Visibility.Visible;
            EquateDeleteListPart.IsEnabled = false;
            EquateDeleteListPart.Visibility = Visibility.Collapsed;
            EquateOrderListPart.IsEnabled = false;
            EquateOrderListPart.Visibility = Visibility.Collapsed;
            EquateListSV.Height = EquateListSV.Height + 30;
            OrderErrorLB.Visibility = Visibility.Collapsed;
            DeleteEquateButton.Visibility = Visibility.Visible;

            OrderEquateButton.Content = "排序";
            AddEquateButton.Content = "添加";

            OrderEquateButton.Click -= CancelOrderQuateBtn_Click;
            OrderEquateButton.Click += ShowOrderQuateBtn_Click;
            AddEquateButton.Click -= CommitOrderQuateBtn_Click;
            AddEquateButton.Click += AddQuateBtn_Click;
        }

        /// <summary>
        /// 确认新排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommitOrderQuateBtn_Click(object sender, RoutedEventArgs e)
        {

            if (SetNewOrder())
            {
                Reload_Equate_List();
                Order_Commit_Or_Cancel();
            }
        }
        /// <summary>
        /// 获取并保存算式的新排序
        /// </summary>
        private bool SetNewOrder()
        {
            EquationConfig ec = new EquationConfig();
            if (CheckOrder())
            {
                TextBox btn = new TextBox();
                for (int i = 0; i < EquateOrderListPart.Children.Count; i++)
                {
                    btn = EquateOrderListPart.Children[i] as TextBox;
                    string newOrder = btn.Text;
                    string name = btn.Name;
                    string uni = name.Substring(name.LastIndexOf("O") + 1);

                    ec.ChangeNodeValue(uni, "order", newOrder);
                }
                ShowIfSuccess("顺序已修改");
                return true;
            }
            return false;
        }
        /// <summary>
        /// 检查新的算式顺序是否有错误，重复序号，输入不是数字
        /// </summary>
        /// <returns></returns>
        private bool CheckOrder()
        {
            List<int> orders = new List<int>();
            //将所有新序号存入数组，判断是否有不是数字的。
            for (int i = 0; i < EquateOrderListPart.Children.Count; i++)
            {
                TextBox btn = (EquateOrderListPart.Children[i] as TextBox);
                if (ifConvertable(btn.Text))
                    orders.Add(Convert.ToInt32(btn.Text));
                else
                {
                    OrderErrorLB.Visibility = Visibility.Visible;
                    OrderErrorLB.Content = "请输入数字作为序号！";
                    Anime_ErrorTip(OrderErrorLB);
                    return false;
                }
            }
            //循环排序，判断是否有相同的序号。
            for (int i = 0; i < orders.Count - 1; i++)
            {
                for (int j = i + 1; j < orders.Count; j++)
                {
                    if (orders[i] == orders[j])
                    {
                        OrderErrorLB.Visibility = Visibility.Visible;
                        OrderErrorLB.Content = "有重复的序号！";
                        Anime_ErrorTip(OrderErrorLB);
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 是否能够转换成功
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool ifConvertable(object obj)
        {
            try
            {
                Convert.ToInt32(obj);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
