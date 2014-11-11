﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace SheetGenerator.BLL
{
    class FileConfig
    {
        /*
         * 
         */
        private XmlDocument xd = new XmlDocument();
        private XmlNode xe = null;
        public void FileConfig()
        {
            xd.Load(AppDomain.CurrentDomain.BaseDirectory + "FileConfig.xml");
            xe = xd.DocumentElement;
        }
        /*
         * 获取银行标识的列表
         */
        internal List<string> GetBankList()
        {
            List<string> bankIdentity = new List<string>();
            XmlNodeList xnl = xe.SelectNodes("//@CHname");
            for (int i = 0; i < xnl.Count; i++)
            {
                bankIdentity.Add(xnl[i].SelectSingleNode("@Identity").Value);
            }
            return bankIdentity;
        }
        /*
         * 获取文件名列表
         */
        internal List<string> GetFileList()
        {
            List<string> files = new List<string>();
            XmlNodeList xnl = xe.SelectSingleNode("/FileList").ChildNodes;
            for(int i=0;i<xnl.Count;i++)
            {
                files.Add(xnl[i].SelectSingleNode("@filename").Value);
            }
            return files;
        }
        /*
         * 获取对应文件下的所有列列表
         */
        internal List<string> GetFileColumns(string filename)
        {
            List<string> columns = new List<string>();
            XmlNodeList xnl = xe.SelectSingleNode("//[@filename='"+filename+"']").ChildNodes;
            for (int i = 0; i < xnl.Count; i++)
            {
                columns.Add(xnl[i].SelectSingleNode("@CHname").Value);
            }
            return columns;
        }
        /*
         * 返回指定文件下的指定列的相关信息。
         */
        internal List<string> GetColumnDetail(string filename, string columnName)
        {
            List<string> colParamDetail = new List<string>();
            XmlNode xnFile = xe.SelectSingleNode("//[@filename='" + filename + "']");
            XmlNode xnCol = xnFile.SelectSingleNode("//[@CHname='" + columnName + "']");
            colParamDetail.Add(xnCol.SelectSingleNode("@name").Value);
            colParamDetail.Add(xnCol.SelectSingleNode("@Vposision").Value);
            colParamDetail.Add(xnCol.SelectSingleNode("@Hposision").Value);
            colParamDetail.Add(xnFile.SelectSingleNode("@filename").Value);
            colParamDetail.Add(xnFile.SelectSingleNode("@tabletype").Value);
            return colParamDetail;
        }

        internal bool ChangeFile()
        {
            return false;
        }
        internal bool ChangeBank()
        {
            return false;
        }
        internal bool AddFile()
        {
            return false;
        }
        internal bool AddBank()
        {
            return false;
        }
        internal bool DeleteFile()
        {
            return false;
        }
        internal bool DeleteBank()
        {
            return false;
        }
    }
}
