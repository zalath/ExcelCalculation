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
        /// 删除指定的算式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EquateDeleteDetailBtn_Click(object sender, RoutedEventArgs e)
        {
            string name = (sender as Button).Name;
            DeleteEquateNameLB.Content = "是否删除" + name.Substring(0,name.LastIndexOf("D") )+ "?";
            DeleteEquateNameLB.Tag = name.Substring(name.LastIndexOf("D") + 1);
            Anime_CVSchange(EditListCVS, DeleteEquateCVS);
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
            Reload_Equate_List();
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
    }
}
