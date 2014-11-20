using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SheetGenerator.BLL
{
    class Equation
    {
        /// <summary>
        /// Calculate the equte from config file
        /// </summary>
        /// <param name="equate">the expresstion of different Excel files</param>
        /// <param name="bankIdentity">bank name and last 6bit of bankID</param>
        /// <param name="month">the selected month</param>
        /// <returns>if success</returns>
        internal bool Calculate(string equate, string bankIdentity, DateTime month)//bankIdentity--银行名称加上备付金账号后六位
        {
            try
            {
                //format1:date(pre,now,next)_ifAddUp(yes,no)_SheetName_columnName%
                string[] equatePart = equate.Split('|');

                List<string> paramList = new List<string>();
                string paramResult = "";
                List<string> symbol = new List<string>();
                //将算式的部件拆分，分别进入参数数组，符号数组，结果参数。
                for (int i = 0; i < equatePart.Length; i++)
                {
                    if (equatePart[i].Contains("+") || equatePart[i].Contains("-")||equatePart[i].Contains("="))
                    {
                        symbol.Add(equatePart[i]);
                    }
                    else
                    {
                        paramList.Add(equatePart[i]);
                    }
                    if (equatePart[i].Contains("="))
                    {
                        paramResult = equatePart[i + 1];
                    }
                }
                //循环计算部分。每个参数的每个取值。
                List<List<double>> paramValueList = GetExcelValue(paramList, bankIdentity, month);
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
                SetExcelValue(paramResult, bankIdentity, month, resultList);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// Get Excel cell's values
        /// </summary>
        /// <param name="paramList">the element list of the equate</param>
        /// <param name="bankIdentity">bank name and last 6bit of bankID</param>
        /// <param name="month">the selected month</param>
        /// <returns>List of the column value lists</returns>
        private List<List<double>> GetExcelValue(List<string> paramList, string bankIdentity, DateTime month)
        {
            ExcelOperation eo = new ExcelOperation();
            List<List<double>> paramValueList = new List<List<double>>();

            for (int i = 0; i < paramList.Count; i++)
            {
                List<double> paramValue = new List<double>();
                string[] parts = paramList[i].Split('@');//表名_列名_是否累加(Y,N)_日期（pre,now,next）
                eo.AboutValue(parts, bankIdentity, month, ref paramValue, "read");
                paramValueList.Add(paramValue);
            }
            return paramValueList;
        }
        /// <summary>
        /// Write the result value list into a full column of Excel file
        /// </summary>
        /// <param name="paramResult">the details about the column which to put the result</param>
        /// <param name="bankIdentity">bank name and last 6bit of bankID</param>
        /// <param name="month">the selected month</param>
        /// <param name="resultList">list of the equte's result</param>
        /// <returns>if success</returns>
        private bool SetExcelValue(string paramResult, string bankIdentity, DateTime month, List<double> resultList)
        {
            try
            {
                ExcelOperation eo = new ExcelOperation();
                string[] parts = paramResult.Split('@');//表名@列名@是否累加(Y,N)@日期（pre,now,next）
                eo.AboutValue(parts, bankIdentity, month, ref resultList, "write");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// true Calculation
        /// </summary>
        /// <param name="val1">val1</param>
        /// <param name="val2">val2</param>
        /// <param name="symbol">+/-</param>
        /// <returns>val1 +/- val2</returns>
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
                else if(symbol == "=")
                {
                    resultList.Add(val1[i]);
                }
            }
            return resultList;
        }
    }
}
