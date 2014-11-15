using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace SheetGenerator.BLL
{
    class CreatePadPart
    {
        internal static Label CreateLabel(string name,string content,Color color)
        {
            Label lb = new Label();
            lb.Foreground = new SolidColorBrush(color);
            lb.Name = name;
            lb.Content = content;
            return lb;
        }
    }
}
