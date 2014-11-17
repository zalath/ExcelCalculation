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
        #region 修改算式部分
        private void GetEquateList(StackPanel u, StackPanel uedit, StackPanel udelete)
        {
            EquationConfig ec = new EquationConfig();
            List<DataRow> equates = ec.GetEquation();
            for (int i = 0; i < equates.Count; i++)
            {
                Label lbEmpty = CreateLabel("E" + i, "", Colors.White, 10);
                Label lbEmptyEdit = CreateLabel("Ee" + i, "", Colors.White, 10);
                Label lbEmptyDelete = CreateLabel("Ed" + i, "", Colors.White, 10);

                Label lb = CreateLabel(equates[i][1].ToString(), equates[i][0] + " : " + equates[i][1].ToString(), Colors.White);

                Button btnEdit = CreateButton(equates[i][1].ToString(), "修改", 30, EquateEditDetailBtn_Click, "part");
                btnEdit.Height = 25;
                btnEdit.Tag = equates[i][2].ToString();

                Button btnDelete = CreateButton(equates[i][1].ToString(), "删除", 30, EquateEditDetailBtn_Click, "part");
                btnDelete.Height = 25;
                btnDelete.Tag = equates[i][2].ToString();
                
                u.Children.Add(lb);
                u.Children.Add(lbEmpty);
                
                uedit.Children.Add(btnEdit);
                uedit.Children.Add(lbEmptyEdit);

                udelete.Children.Add(btnDelete);
                udelete.Children.Add(lbEmptyDelete);
            }
        }
        private void GetEquateParts(string equate)
        {
            string[] eParts = equate.Split('|');
            for (int i = 0; i < eParts.Count(); i++)
            {
                CreateEquatePart(eParts[i]);
            }
        }

        private void CreateEquatePart(string equatePart)
        {
            if (equatePart.Contains("+") || equatePart.Contains("-") || equatePart.Contains("="))
            {
                EquatePartList.Children.Add(CreateButton("", equatePart, 30, SymbolBtnChange_Click, "symbol"));
            }
            else
            {
                string[] partlist = equatePart.Split('@');//tablename_columnname_ifaddup_iftoday

                StackPanel spHead = new StackPanel();
                spHead.Orientation = Orientation.Horizontal;
                spHead.Width = 240;

                Button btnTableName = CreateButton("", partlist[0], 160, EquatePartChange_Click, "part");

                string addup = "";
                if (partlist[2] == "Y")
                    addup = "累加";
                else
                    addup = "每日";
                string Iftoday = "";
                if (partlist[3] == "now")
                    Iftoday = "当日";
                else if (partlist[3] == "pre")
                    Iftoday = "前日";
                else if (partlist[3] == "next")
                    Iftoday = "后日";

                Button btnIfAddup = CreateButton("", addup, 40, EquatePartChange_Click, "part");
                Button btnIftoday = CreateButton("", Iftoday, 40, EquatePartChange_Click, "part");
                spHead.Children.Add(btnTableName);
                spHead.Children.Add(btnIfAddup);
                spHead.Children.Add(btnIftoday);
                EquatePartList.Children.Add(spHead);

                Button btnColumnName = CreateButton("", partlist[1], 240, EquatePartChange_Click, "part");
                EquatePartList.Children.Add(btnColumnName);
            }
        }

        private void EquatePartChange_Click(object sender, RoutedEventArgs e)
        {
        }
        private void SymbolBtnChange_Click(object sender, RoutedEventArgs e)
        {
            //记录控件name并跳转到符号选择界面。
        }
        #endregion

        #region 通用创建控件部分
        private Button CreateButton(string name, string content, double width, System.Windows.RoutedEventHandler method, string type)
        {
            Button btn = new Button();
            if (type == "part")
                btn.Style = this.FindResource("RecbuttonTemplate") as Style;
            else if (type == "symbol")
                btn.Style = this.FindResource("LittlebuttonTemplate") as Style;
            btn.Width = width;
            btn.Height = 30;
            btn.Content = content;
            btn.Foreground = new SolidColorBrush(Colors.White);
            btn.Name = name;
            btn.Click += method;
            return btn;
        }
        internal static Label CreateLabel(string name, string content, Color color, double height = 25)
        {
            Label lb = new Label();
            lb.Foreground = new SolidColorBrush(color);
            lb.Name = name;
            lb.Content = content;
            lb.Height = height;
            return lb;
        }
        #endregion
    }
}
