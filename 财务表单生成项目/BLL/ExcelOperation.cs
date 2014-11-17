using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace SheetGenerator.BLL
{
    class ExcelOperation
    {
        /// <summary>
        /// get or set Excel values. Interface
        /// </summary>
        /// <param name="parts">tablename_columnName_if need add up(Y,N)_datePosition（pre,now,next）</param>
        /// <param name="bankIdentity">bank's unique identity(bank name and the last 6 bit of the bankID)</param>
        /// <param name="month">format：2014-08-10,only get to the month part</param>
        /// <param name="paramValues">the values list to handle</param>
        /// <param name="type">read/write</param>
        /// <returns>bool:success or not</returns>
        internal bool AboutValue(String[] parts, string bankIdentity, DateTime month, ref List<double> paramValues, string type)
        {
            try
            {
                FileConfig fc = new FileConfig();
                List<string> columnDetail = fc.GetColumnDetail(parts[0], parts[1]);//format:name,Vposision,Hposision,filename,tabletype

                DateTime dtNow = month;
                DateTime dtPre = dtNow.AddMonths(-1);
                DateTime dtNext = dtNow.AddMonths(1);
                int daysCountNow = 0;
                int daysCountPre = 0;
                int daysCountNext = 0;
                string tNpartNow = GetMonth(dtNow, ref daysCountNow);
                string tNpartPre = GetMonth(dtPre, ref daysCountPre);
                string tNpartNext = GetMonth(dtNext, ref daysCountNext);
                string fileNameNow = AppDomain.CurrentDomain.BaseDirectory + "Excel/" + bankIdentity + '/' + tNpartNow + parts[0] + "[BJ0000004]北京钱袋宝支付技术有限公司_" + bankIdentity + ".xls";
                string fileNamePre = AppDomain.CurrentDomain.BaseDirectory + "Excel/" + bankIdentity + '/' + tNpartPre + parts[0] + "[BJ0000004]北京钱袋宝支付技术有限公司_" + bankIdentity + ".xls";
                string fileNameNext = AppDomain.CurrentDomain.BaseDirectory + "Excel/" + bankIdentity + '/' + tNpartNext + parts[0] + "[BJ0000004]北京钱袋宝支付技术有限公司_" + bankIdentity + ".xls";

                string tempFileName = AppDomain.CurrentDomain.BaseDirectory + "Excel/模板/" + parts[0] + "[BJ0000004]北京钱袋宝支付技术有限公司_模板.xls";

                if (parts[2] == "Y")
                {
                    TestFile(fileNameNow, tempFileName);
                    AboutValue(ref paramValues, daysCountNow, fileNameNow, columnDetail, "All_Days", type);
                }
                else
                {
                    if (parts[3] == "pre")
                    {
                        TestFile(fileNameNow, tempFileName);
                        TestFile(fileNamePre, tempFileName);
                        AboutValue(ref paramValues, daysCountNow, fileNamePre, columnDetail, "pre_last", type);
                        AboutValue(ref paramValues, daysCountNow, fileNameNow, columnDetail, "pre_main", type);
                    }
                    else if (parts[3] == "next")
                    {
                        TestFile(fileNameNow, tempFileName);
                        TestFile(fileNameNext, tempFileName);
                        AboutValue(ref paramValues, daysCountNow, fileNameNow, columnDetail, "next_main", type);
                        AboutValue(ref paramValues, daysCountNow, fileNameNext, columnDetail, "next_first", type);
                    }
                    else
                    {
                        TestFile(fileNameNow, tempFileName);
                        AboutValue(ref paramValues, daysCountNow, fileNameNow, columnDetail, "now", type);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                string errorMessage = e.Message;
                return false;
            }
        }
        /// <summary>
        /// test if the file is exist,if not copy the temp one to the destiny place.
        /// </summary>
        /// <param name="fileName">destiny file</param>
        /// <param name="tempFileName">template file</param>
        private void TestFile(string fileName, string tempFileName)
        {
            if (!File.Exists(fileName))
            {
                File.Copy(tempFileName, fileName, true);
            }
        }
        /// <summary>
        /// get or set Excel values,step1
        /// </summary>
        /// <param name="paramValues">values to set or get</param>
        /// <param name="daysCount">day's count of the selected month</param>
        /// <param name="fileName">full filename</param>
        /// <param name="columnDetail">name,Vposision,Hposision,filename,tabletype</param>
        /// <param name="whichDay">pre/now/next</param>
        /// <param name="type">read/write</param>
        private void AboutValue(ref List<double> paramValues, int daysCount, string fileName, List<string> columnDetail, string whichDay, string type)
        {
            try
            {
                string tableType = columnDetail[4];

                Application app = new Application();
                Workbooks wbks = app.Workbooks;
                _Workbook _wbk = wbks.Open(fileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                Sheets shs = _wbk.Sheets;
                _Worksheet _wsh = (_Worksheet)shs.get_Item(1);//[0];//}
                app.AlertBeforeOverwriting = false; //屏蔽掉系统跳出的Alert

                if (paramValues.Count != daysCount)
                {
                    for (int i = 0; i < daysCount; i++)
                    {
                        paramValues.Add(0.0);
                    }
                }

                if (whichDay == "pre_last")
                {
                    AboutValue(ref paramValues, columnDetail, _wsh, daysCount, 1, type);
                }
                else if (whichDay == "pre_main")
                {
                    for (int i = 1; i < daysCount; i++)
                    {
                        AboutValue(ref paramValues, columnDetail, _wsh, i, i + 1, type);
                    }
                }
                else if (whichDay == "next_first")
                {
                    AboutValue(ref paramValues, columnDetail, _wsh, 1, daysCount, type);
                }
                else if (whichDay == "next_main")
                {
                    for (int i = 1; i <= daysCount; i++)
                    {
                        AboutValue(ref paramValues, columnDetail, _wsh, i + 1, i, type);
                    }
                }
                else if (whichDay == "now")
                {
                    for (int i = 1; i <= daysCount; i++)
                    {
                        AboutValue(ref paramValues, columnDetail, _wsh, i, i, type);
                    }
                }
                else if (whichDay == "All_Days")
                {
                    if (type == "read")
                    {
                        for (int i = 1; i <= daysCount; i++)
                        {
                            AboutValue(ref paramValues, columnDetail, _wsh, i, i, type);
                        }
                        for (int i = 1; i < paramValues.Count; i++)
                        {
                            paramValues[0] += paramValues[i];
                            paramValues.Remove(paramValues[i]);
                        }
                    }
                    else if (type == "write")
                    {
                        AboutValue(ref paramValues, columnDetail, _wsh, 1, 1, type);
                    }
                }
                if (type == "write")
                {
                    _wbk.Save();
                }
                //app.AlertBeforeOverwriting = false; //屏蔽掉系统跳出的Alert
                _wbk.Close();
                wbks.Close();
                app.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                app = null;

                Process[] ExcelPList = Process.GetProcessesByName("Excel");
                foreach (Process p in ExcelPList)
                {
                    p.Kill();
                }
            }
            catch (Exception)
            { }
        }
        /// <summary>
        /// get or set Excel values,step2
        /// </summary>
        /// <param name="paramValues">values to handle</param>
        /// <param name="columnDetail">Excel columns details in config files</param>
        /// <param name="_wsh">file to operate</param>
        /// <param name="dayNo">the day number in Excel</param>
        /// <param name="valueNo">the number of the value in the list</param>
        /// <param name="type">read/write</param>
        private void AboutValue(ref List<double> paramValues, List<string> columnDetail, _Worksheet _wsh, int dayNo, int valueNo, string type)
        {
            try
            {
                string vposition = columnDetail[1];
                string hposition = columnDetail[2];
                string tableType = columnDetail[4];
                GetPosition(dayNo, ref vposition, ref hposition, tableType, type);

                double value = paramValues[valueNo - 1];
                AboutValue(ref value, vposition, hposition, _wsh, type);
                paramValues[valueNo - 1] = value;
            }
            catch (Exception)
            { }
        }
        /// <summary>
        /// get or set Excel values,step3
        /// </summary>
        /// <param name="value">the value to set or get</param>
        /// <param name="vposition">Excel virtical position</param>
        /// <param name="hposition">Excel horizon position</param>
        /// <param name="_wsh">file to operate</param>
        /// <param name="type">read/write</param>
        private void AboutValue(ref double value, string vposition, string hposition, _Worksheet _wsh, string type)
        {
            try
            {
                if (type == "write")
                {
                    _wsh.get_Range(hposition + vposition, Missing.Value).Value2 = value;
                    //((Range)_wsh.Cells[hposition, vposition]).Value2 = value;
                }
                else if (type == "read")
                {
                    if (_wsh.get_Range(hposition + vposition, Missing.Value) != null || _wsh.get_Range(hposition + vposition, Missing.Value).Value2 != "" || _wsh.get_Range(hposition + vposition, Missing.Value).Value2 != null)
                    {
                        value = Convert.ToDouble(_wsh.get_Range(hposition + vposition, Missing.Value).Value2);
                    }
                    else
                        value = 0;
                }
            }
            catch (Exception)
            { }
        }
        /// <summary>
        /// get the Position params in the Excel
        /// </summary>
        /// <param name="date">number of the day</param>
        /// <param name="Vposition">vertical position number</param>
        /// <param name="Hposition">horizon position number</param>
        /// <param name="tabletype">v/h</param>
        private void GetPosition(int date, ref string Vposition, ref string Hposition, string tabletype, string type)
        {
            try
            {
                if (tabletype == "v")
                {
                    int v = Convert.ToInt32(Vposition);
                    v += date - 1;
                    Vposition = v.ToString();
                }
                else
                {
                    string param = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    int pPos = param.IndexOf(Hposition);
                    Hposition = GetHposition(pPos + date - 1);
                }
            }
            catch (Exception)
            { }
        }

        private string GetHposition(int index, int start = 65)
        {
            string str = "";
            if ((index / 26) > 0)
            {
                str += GetHposition((index / 26) - 1);
            }
            return str + Convert.ToChar(index % 26 + start);
        }

        /// <summary>
        /// get the amount of day of selected month, also form the part of tablename 
        /// </summary>
        /// <param name="month">the seleted month</param>
        /// <param name="daysCount">amount of day of the selected month,return</param>
        /// <returns>month part of the tablename</returns>
        private string GetMonth(DateTime month, ref int daysCount)
        {
            int days = DateTime.DaysInMonth(month.Year, month.Month);
            daysCount = days;
            return month.ToString("yyyyMM") + "01_" + month.ToString("yyyyMM") + days.ToString();
        }
    }
}
