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

        /// <summary>
        /// 获得所有算式列表
        /// </summary>
        /// <returns></returns>
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
                    if (Convert.ToInt32(equations[i]["序号"]) > Convert.ToInt32(equations[j]["序号"]))
                    {
                        DataRow drTemp = equations[i];
                        equations[i] = equations[j];
                        equations[j] = drTemp;
                    }
                }
            }
            return equations;
        }

        /// <summary>
        /// 获得指定算式的细节
        /// </summary>
        /// <param name="eNode"></param>
        /// <returns></returns>
        private DataRow GetEquationdetail(XmlNode eNode)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("uni");
            dt.Columns.Add("序号");
            dt.Columns.Add("名称");
            dt.Columns.Add("算式");
            DataRow dr = dt.NewRow();
            dr["uni"] = eNode.SelectSingleNode("uni").InnerText;
            dr["序号"] = eNode.SelectSingleNode("order").InnerText;
            dr["名称"] = eNode.SelectSingleNode("name").InnerText;
            dr["算式"] = eNode.SelectSingleNode("equate").InnerText;
            return dr;
        }

        /// <summary>
        /// 创建新的算式
        /// </summary>
        /// <param name="order"></param>
        /// <param name="name"></param>
        /// <param name="equate"></param>
        /// <returns></returns>
        internal bool CreateEquation(string name, string equate)
        {
            try
            {
                XmlNode xn = xd.CreateElement("equation");
                xn.AppendChild(CreateNode("uni", GetUni()));
                xn.AppendChild(CreateNode("order", GetUni()));
                xn.AppendChild(CreateNode("name", name));
                xn.AppendChild(CreateNode("equate", equate));
                xe.AppendChild(xn);
                xd.Save(AppDomain.CurrentDomain.BaseDirectory + "EquationList.xml");
                SetUni((Convert.ToInt32(GetUni()) + 1).ToString());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 获取当前一个算式的编号。递增不重复
        /// </summary>
        /// <returns></returns>
        private string GetUni()
        {
            return xe.SelectSingleNode("Uni").InnerText;
        }
        /// <summary>
        /// 给配置文件中的最大编号赋值
        /// </summary>
        /// <param name="uni"></param>
        private void SetUni(string uni)
        {
            xe.SelectSingleNode("Uni").InnerText = uni;
            xd.Save(AppDomain.CurrentDomain.BaseDirectory + "EquationList.xml");
        }

        /// <summary>
        /// 修改某个节点的属性
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        internal bool ChangeNodeValue(string uni, string nodeName, string newValue)
        {
            try
            {
                XmlNodeList xn = xe.SelectNodes("equation[uni=" + uni + "]");
                xn[0].SelectSingleNode(nodeName).InnerText = newValue;
                xd.Save(AppDomain.CurrentDomain.BaseDirectory + "EquationList.xml");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 删除配置表中的某个算式
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal bool DeleteNode(string uni)
        {
            try
            {
                XmlNodeList xn = xe.SelectNodes("equation[uni=" + uni + "]");
                xe.RemoveChild(xn[0]);
                xd.Save(AppDomain.CurrentDomain.BaseDirectory + "EquationList.xml");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 创建新的xml节点
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private XmlNode CreateNode(string name, string value)
        {
            XmlNode xn = xd.CreateElement(name);
            xn.InnerText = value;
            return xn;
        }
    }
}
