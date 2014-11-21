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
        /// 添加新的算式组件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SymbolPartAdd_Click(object sender, RoutedEventArgs e)
        {
            CreateEquateSymbol("?");
            CreateEquateElement("**@**@N@now");
            int no = Convert.ToInt32((sender as Button).Name.Replace("E", ""));
            ExchangePosition(no);
            string equate = ReformEquate();

            EquatePartList.Children.Clear();
            EquatePartEditList.Children.Clear();
            GetEquateParts(equate);
        }
       
        /// <summary>
        /// 获取是否需要累加的按钮
        /// </summary>
        /// <param name="partlist"></param>
        /// <returns></returns>
        private Button GetIfAddupBtn(string[] partlist)
        {
            string addupTxt = "";
            switch (partlist[2])
            {
                case "Y":
                    addupTxt = "累加";
                    break;
                case "N":
                    addupTxt = "每日";
                    break;
            }
            return CreateButton("E" + EquateElementList.Count.ToString(), addupTxt, 40, EquatePartChange_Click, "part","Add");
        }
       
        /// <summary>
        /// 获取是否当日的按钮
        /// </summary>
        /// <param name="partlist"></param>
        /// <returns></returns>
        private Button GetIftodayBtn(string[] partlist)
        {
            string IftodayTxt = "";
            switch (partlist[3])
            {
                case "now":
                    IftodayTxt = "当日";
                    break;
                case "pre":
                    IftodayTxt = "前日";
                    break;
                case "next":
                    IftodayTxt = "后日";
                    break;
            }
            return CreateButton("E" + EquateElementList.Count.ToString(), IftodayTxt, 40, EquatePartChange_Click, "part","Day");
        }

        #region 通用创建控件部分

        /// <summary>
        /// 公用创建按钮。
        /// </summary>
        /// <param name="name"></param>
        /// <param name="content"></param>
        /// <param name="width"></param>
        /// <param name="method"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private Button CreateButton(string name, string content, double width, System.Windows.RoutedEventHandler method, string type, object tag = null)
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
            btn.Margin = new Thickness(0, 0, 0, 10);
            btn.Tag = tag;
            if (content == "**" || content == "?")
                btn.Foreground = new SolidColorBrush(Colors.Red);
            return btn;
        }

        /// <summary>
        /// 公用创建Label
        /// </summary>
        /// <param name="name"></param>
        /// <param name="content"></param>
        /// <param name="color"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        internal static Label CreateLabel(string name, string content, Color color, double height = 25)
        {
            Label lb = new Label();
            lb.Foreground = new SolidColorBrush(color);
            lb.Name = name;
            lb.Content = content;
            lb.Height = height;
            lb.Margin = new Thickness(0, 0, 0, 10);
            return lb;
        }

        /// <summary>
        /// 公用创建TextBox
        /// </summary>
        /// <param name="name"></param>
        /// <param name="content"></param>
        /// <param name="color"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        internal static TextBox CreateTextBox(string name, string content, Color color, double height = 25)
        {
            TextBox lb = new TextBox();
            lb.Foreground = new SolidColorBrush(color);
            lb.Name = name;
            lb.Text = content;
            lb.Height = height;
            lb.Margin = new Thickness(0, 0, 0, 10);
            return lb;
        }
        #endregion

    }
}
