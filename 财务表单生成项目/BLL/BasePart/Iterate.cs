using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Threading;
using System.Windows.Controls;

namespace SheetGenerator.BLL
{
    class Iterate
    {
        private DateTime month;
        private Dispatcher dispatcher;
        private object AddUpProcessBankName_LB;
        private object AddUpProcessTableName_LB;
        private object AddUpProcessColumnName_LB;
        private object AddUpProcessType_LB;

        private DateTime datadt;
        private DateTime datadtnew;
        private DateTime reportdt;
        /// <summary>
        /// init iteration for addup
        /// </summary>
        /// <param name="monthr"></param>
        /// <param name="dispatcherr"></param>
        /// <param name="AddUpProcess_lb"></param>
        public Iterate(DateTime monthr, Dispatcher dispatcherr, object[] AddUpProcess_lb)
        {
            this.month = monthr;
            this.dispatcher = dispatcherr;
            this.AddUpProcessBankName_LB = AddUpProcess_lb[0];
            this.AddUpProcessTableName_LB = AddUpProcess_lb[1];
            this.AddUpProcessColumnName_LB = AddUpProcess_lb[2];
            this.AddUpProcessType_LB = AddUpProcess_lb[3];
        }
        /// <summary>
        /// overload of the init method ,with date params, for alterdate
        /// </summary>
        /// <param name="monthr"></param>
        /// <param name="dispatcherr"></param>
        /// <param name="AddUpProcess_lb"></param>
        /// <param name="datadtr"></param>
        /// <param name="datadtnewr"></param>
        /// <param name="reportdtr"></param>
        public Iterate(DateTime monthr, Dispatcher dispatcherr, object[] AddUpProcess_lb,DateTime datadtr, DateTime datadtnewr,DateTime reportdtr)
        {
            this.month = monthr;
            this.dispatcher = dispatcherr;
            this.AddUpProcessBankName_LB = AddUpProcess_lb[0];
            this.AddUpProcessTableName_LB = AddUpProcess_lb[1];
            this.AddUpProcessColumnName_LB = AddUpProcess_lb[2];
            this.AddUpProcessType_LB = AddUpProcess_lb[3];
            this.datadt = datadtr;
            this.datadtnew = datadtnewr;
            this.reportdt = reportdtr;
        }
        /// <summary>
        /// main iteration
        /// </summary>
        /// <param name="type">for addup or for alterdate</param>
        internal void Iterate_files(string type)
        {
            DirectoryInfo di = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "Excel");
            DirectoryInfo[] dis = di.GetDirectories();
            FileConfig fconfig = new FileConfig();
            List<string> fis = fconfig.GetFileList();
            Equation eq = new Equation();
            AlterDate ad = new AlterDate();
            for (int i = 0; i < dis.Count(); i++)//遍历银行
            {
                if (type == "addup")//累加时不操作累加结果，更改日期时需要更改累加结果
                {
                    if (dis[i].Name == "模板" || dis[i].Name == "累加结果")
                        continue;
                }
                else if(type == "alterdate")
                    if (dis[i].Name == "模板")
                        continue;
                
                showProcess(AddUpProcessBankName_LB, dis[i].Name);
                for (int j = 0; j < fis.Count(); j++)//遍历文件
                {
                    showProcess(AddUpProcessTableName_LB, fis[j]);
                    //判断文件类型和匹配的配置文件
                    string tablename = fis[j];
                    string bankIdentity = dis[i].Name;

                    switch (type)
                    {
                        case "addup":
                            List<string> columName = fconfig.GetFileColumns(tablename);
                            for (int z = 0; z < columName.Count; z++)
                            {
                                showProcess(AddUpProcessColumnName_LB, columName[z]);
                                AddUpAll(tablename, columName[z], bankIdentity, eq);
                            }
                            break;
                        case "alterdate":
                            List<List<string>> dateColumDetail = fconfig.GetDateDetail(tablename);
                            showProcess(AddUpProcessColumnName_LB, "...");
                            for (int z = 0; z < dateColumDetail.Count; z++)
                            {
                                AlterDate(tablename, dateColumDetail[z], bankIdentity, ad);
                            }
                            break;
                    }
                
                }
            }
            showProcess(AddUpProcessBankName_LB, "完成");
            showProcess(AddUpProcessTableName_LB, "完成");
            showProcess(AddUpProcessColumnName_LB, "完成");
            showProcess(AddUpProcessType_LB, "完成");
        }

        /// <summary>
        /// to alter the date of the sourcedata creat time and data's upload time
        /// </summary>
        /// <param name="tablename">table's name</param>
        /// <param name="Datedetail">date columns detail params</param>
        /// <param name="bankIdentity">bank's unique identity</param>
        /// <param name="ad">alterdate class' new object</param>
        private void AlterDate(string tablename, List<string> Datedetail, string bankIdentity, AlterDate ad)
        {
            switch (Datedetail[0])
            {
                case "上报时间":
                    ad.Alter(datadt, reportdt.ToString("yyyyMMdd"), Datedetail, bankIdentity, tablename);
                    break;
                case "数据时间":
                    ad.Alter(datadt, datadtnew.ToString("yyyyMM"), Datedetail, bankIdentity, tablename);
                    break;
            }
        }
        /// <summary>
        /// for add up all data from all banks
        /// </summary>
        /// <param name="tablename">one table's name</param>
        /// <param name="columnname">column's name</param>
        /// <param name="bankIdentity">bank's unique identity</param>
        /// <param name="eq">equotion class' new object</param>
        private void AddUpAll(string tablename, string columnname, string bankIdentity,Equation eq)
        {
            string partName = tablename + "@" + columnname + "@N@now";
            string equotion = partName + "|+|" + partName + "|=|" + partName;
            eq.Calculate(equotion, bankIdentity, month, "累加结果");
        }

        /// <summary>
        /// display the current process on the foreground
        /// </summary>
        /// <param name="Object">label</param>
        /// <param name="txt">display content</param>
        private void showProcess(object Object, string txt)
        {
            dispatcher.Invoke(DispatcherPriority.Normal,
                                       (Action)(() => { (Object as Label).Content = txt; }));
        }
    }
}
