using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SheetGenerator.BLL
{
    class Equation
    {
        /*
         * 计算算式的值。
         * 解析算式
         * 按照算式部件取值
         */
        internal int calculate(string equate,string bankIdentity,string month )//bankIdentity--银行名称加上备付金账号后六位
        {
            //format1:date(pre,now,next)_ifAddUp(yes,no)_SheetName_columnName%
            string[] equatePart = equate.Split('%');

            List<string> paramList = new List<string>();
            string paramResult = "";
            List<string> symbol = new List<string>();
            //将算式的部件拆分，分别进入参数数组，符号数组，结果参数。
            for (int i = 0; i < equatePart.Length; i++)
            {
                if (equatePart[i].Contains("+") || equatePart[i].Contains("-"))
                {
                    symbol.Add(equatePart[i]);
                }
                else if(equatePart[i].Contains("="))
                {
                    paramResult = equatePart[i + 1];
                    break;
                }
                else
                {
                    paramList.Add(equatePart[i]);
                }
            }

            return 1;
        }
        private List<double> GetValue(List<string> paramList,string bankIdentity,string month)
        {
            ExcelOperation eo = new ExcelOperation();
            List<double> param = new List<double>();
            double val1 = 0;
            double val2 = 0;
            month = GetMonth(month);
            for (int i = 0; i < paramList.Count; i++)
            {
                string[] parts = paramList[i].Split('_');//表名_列名_是否累加(Y,N)_日期（pre,now,next）
                string tablename = month + parts[0] + "[BJ0000004]北京钱袋宝支付技术有限公司_" + bankIdentity + ".xls";
                eo.GetValue(AppDomain.CurrentDomain.BaseDirectory + bankIdentity+tablename
            }
            //open Excel,get value,return.
        }
        /*
         * 按照符号运算两个参数
         */
        private double Colculate(double val1, double val2, string symbol) 
        { 
            if(symbol == "+")
            {
                return val1 + val2;
            }
            else if (symbol == "-")
            {
                return val1 - val2;
            }
            else
            {
                return 0;
            }
        }
        private string GetMonth(string month)
        {
            DateTime dt = Convert.ToDateTime(month);
            int days = DateTime.DaysInMonth(dt.Year, dt.Month);
            return dt.ToString("yyyyMM") + "01_" + dt.ToString("yyyyMM") + days.ToString();
        }
    }
}
