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
using System.IO;

namespace SheetGenerator.BLL
{
    class AddUpAll
    {
        private MainWindow mainWindow;
        private Dispatcher dispatcher;
        private DateTime month;
        private object[] addupprocess_lb;

        public AddUpAll(MainWindow mainWindow, Dispatcher dispatcher, DateTime month, object[] AddUpProcess_lb)
        {
            // TODO: Complete member initialization
            this.mainWindow = mainWindow;
            this.dispatcher = dispatcher;
            this.month = month;
            this.addupprocess_lb = AddUpProcess_lb;
        }
        /// <summary>
        /// add all banks' data into one series of excel files
        /// </summary>
        internal void Addup_all()
        {
            /*
             * 获取所有的文件列表
             * 按照文件名特征进行分类计算
             * 获取所有的列名，逐列累加运算
             */
            //创建结果文件夹 。
            string addUpFilePath = AppDomain.CurrentDomain.BaseDirectory + "Excel\\累加结果";
            Directory.CreateDirectory(addUpFilePath);

            Iterate iterate = new Iterate( month, dispatcher, addupprocess_lb);
            iterate.Iterate_files("addup");
        }
    }
}
