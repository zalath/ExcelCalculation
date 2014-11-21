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
        #region 修改算式列表页
        /// <summary>
        /// 获取算式列表 
        /// </summary>
        /// <param name="EquateListPart"></param>
        /// <param name="EquateEditListPart"></param>
        /// <param name="EquateDeleteListPart"></param>
        private void GetEquateList()
        {
            EquationConfig ec = new EquationConfig();
            List<DataRow> equates = ec.GetEquation();
            for (int i = 0; i < equates.Count; i++)
            {
                Label lb = CreateLabel(equates[i]["名称"].ToString(), equates[i]["序号"] + " : " + equates[i]["名称"].ToString(), Colors.White);

                Button btnEdit = CreateButton(equates[i]["名称"].ToString() + "E" + equates[i]["uni"].ToString(), "修改", 30, EquateEditDetailBtn_Click, "part");
                btnEdit.Height = 25;
                btnEdit.Tag = equates[i]["算式"].ToString();

                Button btnDelete = CreateButton(equates[i]["名称"].ToString() + "D" + equates[i]["uni"].ToString(), "删除", 30, EquateDeleteDetailBtn_Click, "part");
                btnDelete.Height = 25;
                btnDelete.Tag = equates[i]["算式"].ToString();

                TextBox textOrder = CreateTextBox(equates[i]["名称"].ToString() + "O" + equates[i]["uni"].ToString(), (i + 1).ToString(), Colors.Black);

                EquateListPart.Children.Add(lb);
                EquateEditListPart.Children.Add(btnEdit);
                EquateDeleteListPart.Children.Add(btnDelete);
                EquateOrderListPart.Children.Add(textOrder);
            }
        }

        #endregion

        #region 修改算式的详细页
        /// <summary>
        /// 将算式分解
        /// </summary>
        /// <param name="equate"></param>
        private void GetEquateParts(string equate)
        {
            string[] eParts = equate.Split('|');
            EquateElementList = new List<UIElement>();
            for (int i = 0; i < eParts.Count(); i++)
            {
                CreateEquatePart(eParts[i]);
            }
        }

        private List<UIElement> EquateElementList = new List<UIElement>();
        private UIElement ElementToChange = new UIElement();

        /// <summary>
        /// 按照算式的部件，生成相应的部件控件
        /// </summary>
        /// <param name="equatePart"></param>
        private void CreateEquatePart(string equatePart)
        {
            if (equatePart.Contains("+") || equatePart.Contains("-") || equatePart.Contains("=") || equatePart.Contains("?"))
            {
                CreateEquateSymbol(equatePart);
            }
            else
            {
                CreateEquateElement(equatePart);
            }
        }

        /// <summary>
        /// 构建算式的参量部分
        /// </summary>
        /// <param name="equatePart"></param>
        private void CreateEquateElement(string equatePart)
        {
            string[] partlist = equatePart.Split('@');//tablename_columnname_ifaddup_iftoday

            StackPanel EquatePartPanel = new StackPanel();

            StackPanel spHead = new StackPanel();
            spHead.Orientation = Orientation.Horizontal;
            spHead.Width = 240;

            //创建所有element中的按钮
            Button btnIfAddup = GetIfAddupBtn(partlist);
            Button btnIftoday = GetIftodayBtn(partlist);
            Button btnColumnName = CreateButton("Column" + EquateElementList.Count.ToString(), partlist[1], 240, EquatePartChange_Click, "part");
            Button btnTableName = CreateButton("TableName" + EquateElementList.Count.ToString(), partlist[0], 160, EquatePartChange_Click, "part", btnColumnName);
            btnColumnName.Tag = btnTableName;

            //将所有的按钮添加到面板中
            spHead.Children.Add(btnTableName);
            spHead.Children.Add(btnIfAddup);
            spHead.Children.Add(btnIftoday);
            EquatePartPanel.Children.Add(spHead);
            EquatePartPanel.Children.Add(btnColumnName);
            EquatePartList.Children.Add(EquatePartPanel);

            //将与算式有关的部分，加入到组件数组中。
            EquateElementList.Add(btnTableName);
            EquateElementList.Add(btnColumnName);
            EquateElementList.Add(btnIfAddup);
            EquateElementList.Add(btnIftoday);

            //创建操作扭
            Button deleteEquatePart = CreateButton("E" + EquateElementList.Count.ToString(), "删除", 30, EquatePartDelete_Click, "symbol");
            //添加入界面
            StackPanel st = new StackPanel();
            deleteEquatePart.Margin = new Thickness(-30, 15, 0, 35);
            deleteEquatePart.Tag = EquatePartPanel;
            EquatePartEditList.Children.Add(deleteEquatePart);
            //添加入组件数组中。
            EquateElementList.Add(deleteEquatePart);
        }

        /// <summary>
        /// 构建算式的符号部分
        /// </summary>
        /// <param name="equatePart"></param>
        private void CreateEquateSymbol(string equatePart)
        {
            Button symbolBtn = CreateButton("E" + EquateElementList.Count.ToString(), equatePart, 30, SymbolBtnChange_Click, "symbol");
            EquatePartList.Children.Add(symbolBtn);

            //创建操作扭
            Button deleteSymbolPart = CreateButton("E" + EquateElementList.Count.ToString(), "删除", 30, SymbolPartDelete_Click, "symbol");
            Button addSymbolPart = CreateButton("E" + EquateElementList.Count.ToString(), "添加", 30, SymbolPartAdd_Click, "symbol");
            //添加入界面
            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Horizontal;
            sp.Children.Add(deleteSymbolPart);
            sp.Children.Add(addSymbolPart);
            deleteSymbolPart.Tag = symbolBtn;
            EquatePartEditList.Children.Add(sp);
            //添加入组件数组中。
            EquateElementList.Add(symbolBtn);
            EquateElementList.Add(deleteSymbolPart);
            EquateElementList.Add(addSymbolPart);
        }

        /// <summary>
        /// 重新构建算式
        /// </summary>
        /// <returns></returns>
        private string ReformEquate()
        {
            string equate = "";
            for (int i = 0; i < EquateElementList.Count; i++)
            {
                if (EquateElementList[i] != null)
                {
                    string btnContent = (EquateElementList[i] as Button).Content.ToString();
                    if (btnContent != "删除" && btnContent != "添加")
                    {
                        if (btnContent == "+" || btnContent == "-" || btnContent == "=" || btnContent == "?")
                        {
                            equate += "|" + btnContent + "|";
                        }
                        else
                        {
                            switch (btnContent)
                            {
                                case "每日":
                                    btnContent = "N";
                                    break;
                                case "累加":
                                    btnContent = "Y";
                                    break;
                                case "当日":
                                    btnContent = "now";
                                    break;
                                case "前日":
                                    btnContent = "pre";
                                    break;
                                case "后日":
                                    btnContent = "next";
                                    break;
                            }
                            equate += btnContent + "@";
                        }
                    }
                    else
                        continue;
                }
                else
                    continue;
            }
            equate = equate.Replace("@|", "|");
            return equate.Substring(0, equate.Length - 1);
        }

        /// <summary>
        /// 检测生成的算式是否正确
        /// </summary>
        /// <param name="equate"></param>
        /// <returns></returns>
        private bool Check_Reformed_Equate(string equate)
        {
            if (equate.Contains("**") || equate.Contains("?"))
            {
                //not all the red element was selected.
                Show_Equate_Change_Error("有未选择的红色部分!");
            }
            else if (equate.Substring(equate.LastIndexOf("|") - 1, 1) != "=")
            {
                //the last symbol is not "=".
                Show_Equate_Change_Error("请改正等号位置!");
            }
            else if (equate.Contains("now@") || equate.Contains("pre@") || equate.Contains("next@"))
            {
                Show_Equate_Change_Error("算式缺少某个符号");
            }
            else if (equate.Contains("||"))
            {
                Show_Equate_Change_Error("算式缺少某个参数");
            }
            else if (equate.IndexOf("=") != equate.LastIndexOf("="))
            {
                Show_Equate_Change_Error("算式中有多个等号");
            }
            else
            {
                return true;
            }
            return false;
        }
        #endregion

    }
}






















