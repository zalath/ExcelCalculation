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

        public MainPart(MainWindow mainWindow, object padPart, Dispatcher dispatcher)
        {
            // TODO: Complete member initialization
            this.mainWindow = mainWindow;
            this.padPart = padPart;
            this.dispatcher = dispatcher;
        }
        internal void Calculate()
        {
            /*
             * get bank list
             * show on the pad
             * for each bank get the config file and calculate the equates
             */
            StackPanel sp = (StackPanel)padPart;

            FileConfig fc = new FileConfig();
            List<string> bankList = fc.GetBankList();

            for (int i = 0; i < bankList.Count; i++)
            {
                dispatcher.Invoke(DispatcherPriority.Normal, 
                                  (Action)(() => { sp.Children.Add(CreatePadPart.CreateLabel(bankList[i], (i + 1) + " : " + bankList[i])); }));
            }
            for (int i = 0; i < bankList.Count; i++)
            {
                
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
