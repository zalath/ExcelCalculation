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
        internal List<DataRow> GetEquation()
        {
            List<DataRow> equations = new List<DataRow>();
            foreach (XmlNode eNode in xe.SelectNodes("equation"))
            {
                equations.Add(GetEquationdetail(eNode));
            }
            for (int i = 0; i < equations.Count; i++)
            {
                for (int j = i + 1; j < equations.Count; j++)
                {
                    if (Convert.ToInt32(equations[i][0]) > Convert.ToInt32(equations[j][0]))
                    {
                        DataRow drTemp = equations[i];
                        equations[i] = equations[j];
                        equations[j] = drTemp;
                    }
                }
            }
            return equations;
        }
        private DataRow GetEquationdetail(XmlNode eNode)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("序号");
            dt.Columns.Add("名称");
            dt.Columns.Add("算式");
            DataRow dr = dt.NewRow();
            dr["序号"] = eNode.SelectSingleNode("order").InnerText;
            dr["名称"] = eNode.SelectSingleNode("name").InnerText;
            dr["算式"] = eNode.SelectSingleNode("equate").InnerText;
            return dr;
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
                XmlNodeList xn = xe.SelectNodes("equation/*["+nodeName+"="+oldValue+"]");
                xn[0].SelectSingleNode(nodeName).InnerText = newValue;
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
                XmlNode xn = xe.SelectSingleNode("equation/*[name=" + name + "]");
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
    }
}
