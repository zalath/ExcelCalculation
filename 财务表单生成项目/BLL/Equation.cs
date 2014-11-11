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
        internal int Calculate(string equate,string bankIdentity,string month )//bankIdentity--银行名称加上备付金账号后六位
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
            //循环计算部分。每个参数的每个取值。
            List<List<double>> paramValueList = GetValue(paramList, bankIdentity, month);
            List<double> resultList = new List<double>();
            for (int i = 0; i < paramList.Count - 1; i++)
            {
                if (i >= 1)
                {
                    resultList = Calculating(resultList, paramValueList[i + 1], symbol[i]);
                }
                else
                {
                    resultList = Calculating(paramValueList[i], paramValueList[i + 1], symbol[i]);
                }
            }
            //将结果保存如Excel文件中。
                return 1;
        }
        private List<List<double>> GetValue(List<string> paramList,string bankIdentity,string month)
        {
            ExcelOperation eo = new ExcelOperation();
            List<List<double>> paramValueList = new List<List<double>>();
            List<double> paramValue = new List<double>();
            for (int i = 0; i < paramList.Count; i++)
            {
                //param = eo.GetValue(AppDomain.CurrentDomain.BaseDirectory + bankIdentity + '/' + tablename, daysCount, columndetail[1], columndetail[2], columndetail[4]);
                string[] parts = paramList[i].Split('_');//表名_列名_是否累加(Y,N)_日期（pre,now,next）
                paramValue = eo.GetValue(parts,bankIdentity,month);
                paramValueList.Add(paramValue);
            }
            return paramValueList;
        }
        /*
         * 将计算出的结果放入Excel中
         */
        private bool SetValue(List<string> paramResult,string bankIdentity,string month,List<double> resultList)
        { 
        }
        /*
         * 按照符号运算两个参数
         */
        private List<double> Calculating(List<double> val1, List<double> val2, string symbol) 
        {
            List<double> resultList = new List<double>();
            for (int i = 0; i < val1.Count; i++)
            {
                if (symbol == "+")
                {
                    resultList.Add(val1[i] + val2[i]);
                }
                else if (symbol == "-")
                {
                    resultList.Add(val1[i] - val2[i]);
                }
            }
            return resultList;
        }
    }
}
