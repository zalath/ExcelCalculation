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
        #region 全局方法
        private void Anime_CVSchange(UIElement from, UIElement to)
        {
            Storyboard sb = new Storyboard();
            Animation.Anime_Move_Middle(ref sb, from, 0, -400, 0, 0);
            Animation.Anime_Move_Middle(ref sb, to, 100, 0, 1, 0);
            sb.Begin();
        }
        private void Anime_CVSchangeBack(UIElement from, UIElement to)
        {
            Storyboard sb = new Storyboard();
            Animation.Anime_Move_Middle(ref sb, from, 100, 0, 1, 0);
            Animation.Anime_Move_Middle(ref sb, to, 0, 400, 0, 0);
            sb.Begin();
        }
        private void Anime_ErrorTip(UIElement uError, double left,double height)
        {
            uError.Opacity = 1;
            Storyboard sb = new Storyboard();
            Animation.Anime_Move_Middle(ref sb, uError, 0, left, 1, height, 25);
            Animation.Anime_Move_Middle(ref sb, uError, 50, left - 10, 1, height, 25);
            Animation.Anime_Move_Middle(ref sb, uError, 100, left + 10, 1, height, 25);
            Animation.Anime_Move_Middle(ref sb, uError, 150, left - 10, 1, height, 25);
            Animation.Anime_Move_Middle(ref sb, uError, 200, left + 10, 1, height, 25);
            Animation.Anime_Move_Middle(ref sb, uError, 250, left - 10, 1, height, 25);
            Animation.Anime_Move_Middle(ref sb, uError, 300, left + 10, 1, height, 25);
            Animation.Anime_Move_Middle(ref sb, uError, 350, left, 1, height, 25);
            sb.Begin();
        }
        private void Anime_VCS_aside(UIElement u)
        {
            Storyboard sb = new Storyboard();
            Animation.Anime_Move_Middle(ref sb, u, 0, -205, 1, 0);
            sb.Begin();
        }
        private void Anime_VCS_aside_Back(UIElement u)
        {
            Storyboard sb = new Storyboard();
            Animation.Anime_Move_Middle(ref sb, u, 0, 0, 1, 0);
            sb.Begin();
        }
        private void Anime_Window_Resize(MainWindow mainWindow)
        {
            Storyboard sb = new Storyboard();
            Animation.Anime_Resize_Middle(ref sb, mainWindow, 0, 450, 380);
            sb.Begin();
        }
        private void Anime_Window_Resize_Back(MainWindow mainWindow)
        {
            Storyboard sb = new Storyboard();
            Animation.Anime_Resize_Middle(ref sb, mainWindow, 0, 450, 260);
            sb.Begin();
        }
        private void Anime_BackBtn_Show(Button BackButton)
        {
            BackButton.IsEnabled = true;
            Storyboard sb = new Storyboard();
            Animation.Anime_Move_Middle(ref sb, BackButton, 0, 20, 1, 10);
            sb.Begin();
        }

        private void Anime_BackBtn_Hide(Button BackButton)
        {
            Storyboard sb = new Storyboard();
            Animation.Anime_Move_Middle(ref sb, BackButton, 0, 20, 0, 10);
            sb.Begin();
        }

        private void anime_Show_ResultMsg()
        {
            Storyboard sb = new Storyboard();
            Animation.Anime_Move_Middle(ref sb, ResultMsgCVS, 0, 0, 1, 150, 200);
            Animation.Anime_Move_Middle(ref sb, ResultMsgCVS, 1500, -400, 0, 150, 200);
            sb.Begin();
        }
        #endregion
    }
}
