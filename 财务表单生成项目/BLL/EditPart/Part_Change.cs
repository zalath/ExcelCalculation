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
        /// 取消修改算式按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            equateWarnLB.Visibility = Visibility.Collapsed;
            NewNameLB.Visibility = Visibility.Visible;
            equateNameTB.Visibility = Visibility.Visible;
            Anime_CVSchangeBack(EditListCVS, EditDetailCVS);
            Anime_Window_Resize_Back(this);
            currentCVS = EditListCVS;
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
                ec.ChangeNodeValue(hideequateNameLB.Content.ToString(), "name", equateNameTB.Text);
                ec.ChangeNodeValue(hideequateNameLB.Content.ToString(), "equate", equate);
                Reload_Equate_List();
                Anime_Window_Resize_Back(this);
                Anime_CVSchangeBack(EditListCVS, EditDetailCVS);
                ShowIfSuccess("修改成功");
            }
        }
    }
}
