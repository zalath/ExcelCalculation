using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;

namespace SheetGenerator.BLL
{
    class ExcelOperation
    {
        /*
         * 从Excel中取值
         */
        internal List<double> GetValue(string[] parts, string bankIdentity, string month)//parts：表名_列名_是否累加(Y,N)_日期（pre,now,next）
        {
            FileConfig fc = new FileConfig();
            int daysCount = 0;
            month = GetMonth(month, ref daysCount);
            string tablename = month + parts[0] + "[BJ0000004]北京钱袋宝支付技术有限公司_" + bankIdentity + ".xls";
            List<string> columndetail = fc.GetColumnDetail(parts[0], parts[1]);

            List<double> values = new List<double>();
            Application app = new Application();
            Workbooks wbks = app.Workbooks;
            _Workbook _wbk = wbks.Add(AppDomain.CurrentDomain.BaseDirectory + bankIdentity + '/'+ tablename);
            Sheets shs = _wbk.Sheets;
            _Worksheet _wsh = (_Worksheet)shs.get_Item(0);
            for (int i = 0; i < daysCount; i++)
            {
                GetPosition(i, ref Vposition, ref Hposition, tabletype);
                double value = (double)_wsh.Cells[Vposition, Hposition];
                values.Add(value);
            }
            return values;
        }
        /*
         * 
         * parts：表名_列名_是否累加(Y,N)_日期（pre,now,next）
         * columnDetail：name,Vposision,Hposision,filename,tabletype
         */
        internal bool AboutValue(String[] parts, string bankIdentity, string month,ref List<double> paramValues, string type)
        {
            FileConfig fc = new FileConfig();
            List<string> columnDetail = fc.GetColumnDetail(parts[0], parts[1]);

            DateTime dtNow = Convert.ToDateTime(month);
            DateTime dtPre = dtNow.AddMonths(-1);
            DateTime dtNext = dtNow.AddMonths(1);
            int daysCountNow = 0;
            int daysCountPre = 0;
            int daysCountNext = 0;
            string tNpartNow = GetMonth(dtNow, ref daysCountNow);
            string tNpartPre = GetMonth(dtPre, ref daysCountPre);
            string tNpartNext = GetMonth(dtNext, ref daysCountNext);
            string fileNameNow = tNpartNow + parts[0] + "[BJ0000004]北京钱袋宝支付技术有限公司_" + bankIdentity + ".xls";
            string fileNamePre = tNpartPre + parts[0] + "[BJ0000004]北京钱袋宝支付技术有限公司_" + bankIdentity + ".xls";
            string fileNameNext = tNpartNext + parts[0] + "[BJ0000004]北京钱袋宝支付技术有限公司_" + bankIdentity + ".xls";

            if (parts[3] == "pre")
            {
            }
            else if (parts[3] == "next")
            {
            }
            else
            {
            }
        }
        private void AboutValue(ref List<double> paramValues,string bankIdentity, string fileName,string tableType,string whichDay,string type)
        {
            Application app = new Application();
            Workbooks wbks = app.Workbooks;
            _Workbook _wbk = wbks.Add(AppDomain.CurrentDomain.BaseDirectory + bankIdentity + '/' + fileName);
            Sheets shs = _wbk.Sheets;
            _Worksheet _wsh = (_Worksheet)shs.get_Item(0);
            if (whichDay == "pre_last")
            {
            }
            else if (whichDay == "pre_main")
            {
            }
            else if (whichDay == "next_first")
            {
            }
            else if (whichDay == "next_main")
            {
            }
            else if(whichDay == "now")
            {
            }
        }
        /*
         * 向Excel中放值
         */
        internal void SetValue(string path, int daysInMonth, string Vposition, string Hposition, string tabletype, List<double> value)
        {
            Application app = new Application();
            Workbooks wbks = app.Workbooks;
            _Workbook _wbk = wbks.Add(path);
            Sheets shs = _wbk.Sheets;
            _Worksheet _wsh = (_Worksheet)shs.get_Item(0);
            for (int i = 0; i < daysInMonth; i++)
            {
                GetPosition(i, ref Vposition, ref Hposition, tabletype);
                _wsh.Cells[Vposition, Hposition] = value[i];
            }
        }
        /*
         * 计算在Excel中的位置
         */
        private void GetPosition(int date, ref string Vposition, ref string Hposition, string tabletype)
        {
            if (tabletype == "v")
                Vposition += date - 1;
            else
            {
                	string param = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    int pPos = param.IndexOf(Hposition);
			    pPos = pPos + date - 1;
			    Hposition = GetHposition( pPos );
            }
        }
        /*
         * 将数字转化成Excel的横坐标
         */
        private string GetHposition(int index, int start = 65)
        {
            string str = "";
            if ((index / 26) > 0)
            {
                str += GetHposition((index / 26) - 1);
            }
            return str + Convert.ToChar(index % 26 + start);
        }
        /*
         * 获取当月的天数和表名中的组件
         */
        private string GetMonth(DateTime month, ref int daysCount)
        {
            int days = DateTime.DaysInMonth(month.Year, month.Month);
            daysCount = days;
            return month.ToString("yyyyMM") + "01_" + month.ToString("yyyyMM") + days.ToString();
        }
    }
}
