using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SheetGenerator.BLL
{
    class AlterDate
    {
        ExcelOperation eo;
        public AlterDate()
        {
            this.eo = new ExcelOperation();
        }
        /// <summary>
        /// alter target date cell to special value
        /// </summary>
        /// <param name="datadt">source data's date</param>
        /// <param name="date">taget date</param>
        /// <param name="columnDetail">the param details of datecolumn</param>
        /// <param name="bankIdentity">the identity of the bank</param>
        /// <param name="tablename">like '表N_N'</param>
        /// <returns></returns>
        internal bool Alter(DateTime datadt, string date, List<string> columnDetail, string bankIdentity,string tablename)
        {
            try
            {
                List<double> result = new List<double>();
                DateTime dtNow = datadt;
                result.Add(Convert.ToDouble(date)); 

                int daysCountNow = 0;
                string tNpartNow = eo.GetMonth(dtNow, ref daysCountNow);
                string fileName = AppDomain.CurrentDomain.BaseDirectory + "Excel/" + bankIdentity + '/' + tNpartNow + tablename + "[BJ0000004]北京钱袋宝支付技术有限公司_" + bankIdentity + ".xls";

                Application app = new Application();
                Workbooks wbks = app.Workbooks;
                _Workbook _wbk = wbks.Open(fileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                Sheets shs = _wbk.Sheets;
                _Worksheet _wsh = (_Worksheet)shs.get_Item(1);//[0];//}
                app.AlertBeforeOverwriting = false; //屏蔽掉系统跳出的Alert

                eo.AboutValue(ref result, columnDetail, _wsh, 1, 1, "write");
                eo.CloseWorksheet(_wbk, wbks, app);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
