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
        /// 将新加入的部分移动到指定的位置
        /// </summary>
        /// <param name="no"></param>
        private void ExchangePosition(int no)
        {
            for (int i = EquateElementList.Count - 8; i < EquateElementList.Count; i++)
            {
                for (int j = i; j > no; j--)
                {
                    UIElement uTemp = EquateElementList[j];
                    EquateElementList[j] = EquateElementList[j - 1];
                    EquateElementList[j - 1] = uTemp;
                }
                no++;
            }
        }

        /// <summary>
        /// 删除算式中的符号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SymbolPartDelete_Click(object sender, RoutedEventArgs e)
        {
            //删除按钮
            ((sender as Button).Tag as Button).Visibility = Visibility.Collapsed;
            ((sender as Button).Tag as Button).IsEnabled = false;

            ((sender as Button).Parent as StackPanel).IsEnabled = false;
            ((sender as Button).Parent as StackPanel).Visibility = Visibility.Collapsed;
            //删除在element数组中的对应对象
            int no = Convert.ToInt32((sender as Button).Name.Replace("E", ""));
            EquateElementList[no - 1] = null;
            EquateElementList[no] = null;
            EquateElementList[no + 1] = null;
        }
        
        /// <summary>
        /// 删除算式某一参数部分
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EquatePartDelete_Click(object sender, RoutedEventArgs e)
        {
            //删除界面上的空间
            ((sender as Button).Tag as StackPanel).Children.Clear();             //..
            (sender as Button).IsEnabled = false;                                //表名
            (sender as Button).Visibility = Visibility.Collapsed;                //列名
            //删除数组中对应的对象                                               //是否累加
            int no = Convert.ToInt32((sender as Button).Name.Replace("E", ""));  //是否当日
            EquateElementList[no - 4] = null;                                    //删除按钮
            EquateElementList[no - 3] = null;                                    //运算符号
            EquateElementList[no - 2] = null;                                    //删除按钮
            EquateElementList[no - 1] = null;                                    //添加按钮
            EquateElementList[no] = null;                                        //..
        }

        /// <summary>
        /// 修改算式的某个参数。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EquatePartChange_Click(object sender, RoutedEventArgs e)
        {
            ElementToChange = (sender as Button);
            if ((sender as Button).Tag != null)
            {
                FileConfig fc = new FileConfig();
                string tag = (sender as Button).Tag.ToString();
                if (tag == "Add")
                {
                    Anime_CVSchange(EditDetailCVS, EditDetail_ChoseAddupCVS);
                    currentCVS = EditDetail_ChoseAddupCVS;
                }
                else if (tag == "Day")
                {
                    Anime_CVSchange(EditDetailCVS, EditDetail_ChoseDayCVS);
                    currentCVS = EditDetail_ChoseDayCVS;
                }
                else
                {
                    PartNameList.Children.Clear();
                    if ((sender as Button).Name.Substring(0,6) == "Column")
                    {
                        PartName.Content = "选择列：";
                        //获取tag中文件名下的所有列名
                        List<string> columnList = fc.GetFileColumns(((sender as Button).Tag as Button).Content.ToString());
                        for (int i = 0; i < columnList.Count; i++)
                        {
                            Button btn = CreateButton("Column" + i, columnList[i], 300, BtnContentChanged_Click, "part");
                            PartNameList.Children.Add(btn);
                        }
                    }
                    else
                    {
                        ((sender as Button).Tag as Button).Content = "**";
                        PartName.Content = "选择表：";
                        //获取文件列表
                        List<string> fileList = fc.GetFileList();
                        for (int i = 0; i < fileList.Count; i++)
                        {
                            Button btn = CreateButton("Table" + i, fileList[i], 200, BtnContentChanged_Click, "part");
                            PartNameList.Children.Add(btn);
                        }
                    }
                    Anime_CVSchange(EditDetailCVS, EditDetail_ChosePartCVS);
                    currentCVS = EditDetail_ChosePartCVS;
                }
            }
        }

        /// <summary>
        /// 修改算式中运算符号。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SymbolBtnChange_Click(object sender, RoutedEventArgs e)
        {
            ElementToChange = (sender as Button);
            currentCVS = EditDetail_ChoseSymbolCVS;
            Anime_CVSchange(EditDetailCVS, EditDetail_ChoseSymbolCVS);
        }

        /// <summary>
        /// 选择算式组件的内容后，修改算式中的相应组件的内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnContentChanged_Click(object sender, RoutedEventArgs e)
        {
            equateNameLB.Visibility = Visibility.Visible;
            equateWarnLB.Visibility = Visibility.Collapsed;
            (ElementToChange as Button).Content = (sender as Button).Content;
            Anime_CVSchangeBack(EditDetailCVS, currentCVS);
            currentCVS = EditDetailCVS;
        }
    }
}
