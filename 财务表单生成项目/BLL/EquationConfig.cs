using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data;

namespace SheetGenerator.BLL
{
    class EquationConfig
    {
        private XmlDocument xd = new XmlDocument();
        private XmlNode xe = null;
        public EquationConfig()
        {
            xd.Load(AppDomain.CurrentDomain.BaseDirectory + "EquationList.xml");
            xe = xd.DocumentElement;
        }
        /*
         * 获取所有的算式列表
         */
        internal List<DataTable> GetEquation()
        {
            List<DataTable> equations = new List<DataTable>();
            foreach (XmlNode eNode in xe.ChildNodes)
            {
                equations.Add(GetEquationdetail(eNode));
            }
            return equations;
        }

        /*
         * 创建新的算式
         */
        internal bool CreateEquation(string order, string name, string equate)
        {
            try
            {
                XmlNode xn = xd.CreateElement("equation");
                xn.AppendChild(CreateNode("order", order));
                xn.AppendChild(CreateNode("name", name));
                xn.AppendChild(CreateNode("equate", equate));
                xe.AppendChild(xn);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /*
         * 修改某个节点的属性 
         */
        internal bool ChangeNodeValue(string nodeName, string oldValue, string newValue)
        {
            try
            {
                XmlNode xn = xe.SelectSingleNode("/equation/["+nodeName+"="+oldValue+"]");
                xn.SelectSingleNode(nodeName).InnerText = newValue;
                xd.Save(AppDomain.CurrentDomain.BaseDirectory + "EquationList.xml");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /*
         * 删除配置表中的某个算式。 
         */
        internal bool DeleteNode(string name)
        {
            try
            {
                XmlNode xn = xe.SelectSingleNode("/equation/[name=" + name + "]");
                xe.RemoveChild(xn);
                xd.Save(AppDomain.CurrentDomain.BaseDirectory + "EquationList.xml");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private XmlNode CreateNode(string name,string value)
        {
            XmlNode xn = xd.CreateElement(name);
            xn.Value = value;
            return xn;
        }
        private DataTable GetEquationdetail(XmlNode eNode)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("序号");
            dt.Columns.Add("名称");
            dt.Columns.Add("算式");
            DataRow dr = dt.NewRow();
            dr["序号"] = eNode.SelectSingleNode("/order");
            dr["名称"] = eNode.SelectSingleNode("/name");
            dr["算式"] = eNode.SelectSingleNode("/equate");
            dt.Rows.Add(dr);
            return dt;
        }
    }
}
