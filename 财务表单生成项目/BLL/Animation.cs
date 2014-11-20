using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Animation;

namespace SheetGenerator
{
    class Animation
    {
        private static DoubleAnimationUsingKeyFrames CreateAnimate(object element, int starttime, double propertyValue, string propertyName, int duration = 400)
        {
            SplineDoubleKeyFrame sdk = new SplineDoubleKeyFrame();
            sdk.KeySpline = new KeySpline(0.3, 0.5, 0, 1);
            sdk.Value = propertyValue;

            DoubleAnimationUsingKeyFrames das = new DoubleAnimationUsingKeyFrames();
            das.KeyFrames.Add(sdk);
            das.BeginTime = new TimeSpan(0, 0, 0, 0, starttime);
            das.Duration = new TimeSpan(0, 0, 0, 0, duration);

            if (element.GetType().ToString() == "SheetGenerator.MainWindow")
                Storyboard.SetTarget(das, (element as MainWindow));
            else if (element.GetType().ToString().Contains("System.Windows.Controls"))
                Storyboard.SetTarget(das, (element as UIElement));

            Storyboard.SetTargetProperty(das, new PropertyPath(propertyName));
            return das;
        }
        /// <summary>
        /// anime with a select duration
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="element"></param>
        /// <param name="starttime"></param>
        /// <param name="duration"></param>
        /// <param name="HmoveValue"></param>
        /// <param name="opacityValue"></param>
        /// <param name="VmoveValue"></param>
        internal static void Anime_Move_Middle(ref Storyboard sb, UIElement element, int starttime, double HmoveValue, double opacityValue, double VmoveValue, int duration = 400)
        {
            if (element != null)
            {
                if (HmoveValue != double.NaN)
                {
                    DoubleAnimationUsingKeyFrames da = Animation.CreateAnimate(element, starttime, HmoveValue, "(Canvas.Left)", duration);
                    sb.Children.Add(da);
                }
                if (opacityValue != double.NaN)
                {
                    DoubleAnimationUsingKeyFrames da1 = Animation.CreateAnimate(element, starttime, opacityValue, "(Opacity)", duration);
                    sb.Children.Add(da1);
                }
                if (VmoveValue != double.NaN)
                {
                    DoubleAnimationUsingKeyFrames da2 = Animation.CreateAnimate(element, starttime, VmoveValue, "(Canvas.Top)", duration);
                    sb.Children.Add(da2);
                }
            }
        }
        internal static void Anime_Resize_Middle(ref Storyboard sb, object mainWindow, int starttime, double height, double width, int duration = 400)
        {
            if (height != double.NaN)
            {
                DoubleAnimationUsingKeyFrames da = Animation.CreateAnimate(mainWindow, starttime, width, "(Width)", duration);
                sb.Children.Add(da);
            }
            if (width != double.NaN)
            {
                DoubleAnimationUsingKeyFrames da1 = Animation.CreateAnimate(mainWindow, starttime, height, "(Height)", duration);
                sb.Children.Add(da1);
            }
        }
    }
}
