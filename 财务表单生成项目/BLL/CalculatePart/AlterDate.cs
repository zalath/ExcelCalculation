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
using SheetGenerator.BLL;

namespace SheetGenerator
{
    class AlterDate
    {
        private Dispatcher dispatcher;
        private DateTime Datadt = DateTime.Now;
        private DateTime Datadtnew = DateTime.Now;
        private DateTime Reportdt = DateTime.Now; 
        private object[] addupprocess_lb;
        public AlterDate(Dispatcher dispatcher, DateTime datadt, DateTime datadtnew, DateTime reportdt, object[] AddUpProcess_lb)
        {
            this.Datadt = datadt;
            this.Datadtnew = datadtnew;
            this.Reportdt = reportdt;
            this.dispatcher = dispatcher;
            this.addupprocess_lb = AddUpProcess_lb;
        }
        /// <summary>
        /// start alter thread
        /// </summary>
        internal void Alterdate()
        {
            Iterate iterate = new Iterate(Datadt, dispatcher, addupprocess_lb, Datadt,Datadtnew,Reportdt);
            iterate.Iterate_files("alterdate");
        }
    }
}
