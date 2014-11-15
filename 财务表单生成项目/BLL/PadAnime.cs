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
using System.Windows.Media.Animation;
using SheetGenerator.BLL;
using System.IO;

namespace SheetGenerator
{
    public partial class MainWindow : Window
    {
        private void anime_CVSchange(UIElement u1,UIElement u2)
        {
            Storyboard sb = new Storyboard();
            Animation.anime_Move_middle(ref sb, u1, 0, -200, 0, 0);
            Animation.anime_Move_middle(ref sb, u2, 100, 0, 1, 0);
            sb.Begin();
        }
        private void anime_CVSchangeBack(UIElement u1, UIElement u2)
        {
            Storyboard sb = new Storyboard();
            Animation.anime_Move_middle(ref sb, u1, 0, 200, 0, 0);
            Animation.anime_Move_middle(ref sb, u2, 100, 0, 1, 0);
            sb.Begin();
        }
        private void anime_ErrorTip(UIElement uError)
        {
            uError.Opacity = 1;
            Storyboard sb = new Storyboard();
            Animation.anime_Move_middle(ref sb, uError, 0, 50, 70, 1, 260);
            Animation.anime_Move_middle(ref sb, uError, 50, 50, 80, 1, 260);
            Animation.anime_Move_middle(ref sb, uError, 100, 50, 60, 1, 260);
            Animation.anime_Move_middle(ref sb, uError, 150, 50, 80, 1, 260);
            Animation.anime_Move_middle(ref sb, uError, 200, 50, 60, 1, 260);
            Animation.anime_Move_middle(ref sb, uError, 250, 50, 80, 1, 260);
            Animation.anime_Move_middle(ref sb, uError, 300, 50, 60, 1, 260);
            Animation.anime_Move_middle(ref sb, uError, 350, 50, 70, 1, 260);
            sb.Begin();
        }
        private void anime_VCS_aside(UIElement u)
        {
            Storyboard sb = new Storyboard();
            Animation.anime_Move_middle(ref sb, u, 0, -205, 1, 0);
            sb.Begin();
        }
        private void anime_VCS_aside_Back(UIElement u)
        {
            Storyboard sb = new Storyboard();
            Animation.anime_Move_middle(ref sb, u, 0, 0, 1, 0);
            sb.Begin();
        }
    }
}
