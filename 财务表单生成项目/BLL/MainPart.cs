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
        private void GetEquateList(StackPanel u, StackPanel uedit)
        {
            EquationConfig ec = new EquationConfig();
            List<DataRow> equates = ec.GetEquation();
            for (int i = 0; i < equates.Count; i++)
            {
                Label lb = CreatePadPart.CreateLabel(equates[i][1].ToString(), equates[i][0] + " : " + equates[i][1].ToString(), Colors.White);
                Label lbedit = CreatePadPart.CreateLabel(equates[i][1].ToString(), "修改", Colors.White);
                lbedit.Tag = equates[i][2].ToString();
                lbedit.MouseLeftButtonDown += EquateEditDetailBtn_Click;
                u.Children.Add(lb);
                uedit.Children.Add(lbedit);
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
                spHead.Width = 200;

                Button btnTableName = CreateButton("", partlist[0], 120, EquatePartChange_Click,"part");
                Button btnIfAddup = CreateButton("", partlist[2], 40, EquatePartChange_Click, "part");
                Button btnIftoday = CreateButton("", partlist[3], 40, EquatePartChange_Click, "part");
                spHead.Children.Add(btnTableName);
                spHead.Children.Add(btnIfAddup);
                spHead.Children.Add(btnIftoday);
                EquatePartList.Children.Add(spHead);
                Button btnColumnName = CreateButton("",partlist[1],200,EquatePartChange_Click,"part");
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

        private Button CreateButton(string name, string content, double width, System.Windows.RoutedEventHandler method,string type)
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
    }
}
