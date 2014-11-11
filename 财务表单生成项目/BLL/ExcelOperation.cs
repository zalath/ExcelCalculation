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
        internal double GetValue(string path, string date, string Vposition, string Hposition, string tabletype)
        {
            GetPosition(date, ref Vposition, ref Hposition, tabletype);
            Application app = new Application();
            Workbooks wbks = app.Workbooks;
            _Workbook _wbk = wbks.Add(path);
            Sheets shs = _wbk.Sheets;
            _Worksheet _wsh = (_Worksheet)shs.get_Item(0);
            double value = (double)_wsh.Cells[Vposition, Hposition];
            return value;
        }
        /*
         * 向Excel中放值
         */
        internal void SetValue(string path, string date, string Vposition, string Hposition, string tabletype, double value)
        {
            GetPosition(date, ref Vposition, ref Hposition, tabletype);
            Application app = new Application();
            Workbooks wbks = app.Workbooks;
            _Workbook _wbk = wbks.Add(path);
            Sheets shs = _wbk.Sheets;
            _Worksheet _wsh = (_Worksheet)shs.get_Item(0);
            _wsh.Cells[Vposition, Hposition] = value;
        }
        /*
         * 计算在Excel中的位置
         */
        private void GetPosition(string date, ref string Vposition, ref string Hposition, string tabletype)
        {
            DateTime dt = Convert.ToDateTime(date);
            if (tabletype == "v")
                Vposition += Convert.ToInt32(dt.Date) - 1;
            else
            {
                	string param = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    int pPos = param.IndexOf(Hposition);
			    pPos = pPos + Convert.ToInt32(dt.Date) - 1;
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
    }
}
