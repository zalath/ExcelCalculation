using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Diagnostics;
using System.Windows.Threading;

namespace SheetGenerator.BLL
{
    class MainPart
    {
        private MainWindow mainWindow;
        private object padPart;
        private Dispatcher dispatcher;
        private DateTime month;
        private List<string> bankList;
        private object bankResultList;

        public MainPart(MainWindow mainWindow, object padPart, Dispatcher dispatcher, DateTime month,List<string> bankList,object bankResultList)
        {
            // TODO: Complete member initialization
            this.mainWindow = mainWindow;
            this.padPart = padPart;
            this.dispatcher = dispatcher;
            this.month = month;
            this.bankList = bankList;
            this.bankResultList = bankResultList;
        }
        /// <summary>
        /// get bankIdentity list
        /// get equate list
        /// to each bankIdentity, calculate all the equtes.
        /// </summary>
        internal void Calculate()
        {
            StackPanel sp = (StackPanel)padPart;

            EquationConfig eConfig = new EquationConfig();
            List<DataRow> equateList = eConfig.GetEquation();

            Equation eq = new Equation();
            for (int i = 0; i < bankList.Count; i++)
            {
                for (int j = 0; j < equateList.Count; j++)
                {
                    try
                    {
                        eq.Calculate(equateList[j]["算式"].ToString(), bankList[i], month);
                        dispatcher.Invoke(DispatcherPriority.Normal,
                                         (Action)(() => { (bankResultList as StackPanel).Children.Add(CreatePadPart.CreateLabel("", "ok", Colors.White)); }));
                        //for (int k = 0; k < sp.Children.Count;k++ )
                        //{
                        //    if ((sp.Children[k] as Label).Name == bankList[i])
                        //    {
                        //        dispatcher.Invoke(DispatcherPriority.Normal,
                        //                         (Action)(() => { (sp.Children[k] as Label).Content += "..ok"; }));
                        //    }
                        //}
                    }
                    catch (Exception error)
                    {
                    }
                }
            }
        }

        internal void EditEquate()
        {
            /*
             * get the equation list
             * 
             */
        }
    }
}
