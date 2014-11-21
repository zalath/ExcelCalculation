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
        /// 添加新的算式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddQuateBtn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = new Button();
            btn.Name = "XEN";
            btn.Tag = "**@**@N@now|=|**@**@N@now";
            ShowEquateDetail_Edit(btn);//添加显示新增算式名称的textbox。修改按钮事件为保存。
        }
        private void CommitAddBtn_Click(object sender, RoutedEventArgs e)
        {
            string name = equateNameTB.Text;
            string equate = ReformEquate();
            if (name == "")
                Show_Equate_Change_Error("请填写算式名称!");
            else if (Check_Reformed_Equate(equate))
            {
                EquationConfig ec = new EquationConfig();
                if (ec.CreateEquation(name, equate))
                {
                    Reload_Equate_List();
                    Anime_Window_Resize_Back(this);
                    Anime_CVSchangeBack(EditListCVS, EditDetailCVS);
                    ShowIfSuccess("添加成功");
                }
                else
                {
                    ShowIfSuccess("添加失败");
                }
            }
        }
    }
}
