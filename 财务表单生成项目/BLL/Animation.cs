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
        private static DoubleAnimationUsingKeyFrames CreateAnimate(UIElement element, int starttime, double propertyValue, string propertyName)
        {
            SplineDoubleKeyFrame sdk = new SplineDoubleKeyFrame();
            sdk.KeySpline = new KeySpline(0.3, 0.5, 0, 1);
            sdk.Value = propertyValue;

            DoubleAnimationUsingKeyFrames das = new DoubleAnimationUsingKeyFrames();
            das.KeyFrames.Add(sdk);
            das.BeginTime = new TimeSpan(0, 0, 0, 0, starttime);
            das.Duration = new TimeSpan(0, 0, 0, 0, 400);

            Storyboard.SetTarget(das, element);
            Storyboard.SetTargetProperty(das, new PropertyPath(propertyName));
            return das;
        }

        internal static void anime_Move_middle(ref Storyboard sb, UIElement element, int starttime, double HmoveValue, double opacityValue, double VmoveValue)
        {
            if (element != null)
                if (HmoveValue != double.NaN)
                {
                    DoubleAnimationUsingKeyFrames da = Animation.CreateAnimate(element, starttime, HmoveValue, "(Canvas.Left)");
                    sb.Children.Add(da);
                }
            if (opacityValue != double.NaN)
            {
                DoubleAnimationUsingKeyFrames da1 = Animation.CreateAnimate(element, starttime, opacityValue, "(Opacity)");
                sb.Children.Add(da1);
            }
            if (VmoveValue != double.NaN)
            {
                DoubleAnimationUsingKeyFrames da2 = Animation.CreateAnimate(element, starttime, VmoveValue, "(Canvas.Top)");
                sb.Children.Add(da2);
            }
        }
        private static DoubleAnimationUsingKeyFrames CreateAnimate(UIElement element, int starttime, int duration, double propertyValue, string propertyName)
        {
            SplineDoubleKeyFrame sdk = new SplineDoubleKeyFrame();
            sdk.KeySpline = new KeySpline(0.3, 0.5, 0, 1);
            sdk.Value = propertyValue;

            DoubleAnimationUsingKeyFrames das = new DoubleAnimationUsingKeyFrames();
            das.KeyFrames.Add(sdk);
            das.BeginTime = new TimeSpan(0, 0, 0, 0, starttime);
            das.Duration = new TimeSpan(0, 0, 0, 0, duration);

            Storyboard.SetTarget(das, element);
            Storyboard.SetTargetProperty(das, new PropertyPath(propertyName));
            return das;
        }

        internal static void anime_Move_middle(ref Storyboard sb, UIElement element, int starttime, int duration, double HmoveValue, double opacityValue, double VmoveValue)
        {
            if (element != null)
                if (HmoveValue != double.NaN)
                {
                    DoubleAnimationUsingKeyFrames da = Animation.CreateAnimate(element, starttime, duration, HmoveValue, "(Canvas.Left)");
                    sb.Children.Add(da);
                }
            if (opacityValue != double.NaN)
            {
                DoubleAnimationUsingKeyFrames da1 = Animation.CreateAnimate(element, starttime, duration, opacityValue, "(Opacity)");
                sb.Children.Add(da1);
            }
            if (VmoveValue != double.NaN)
            {
                DoubleAnimationUsingKeyFrames da2 = Animation.CreateAnimate(element, starttime, duration, VmoveValue, "(Canvas.Top)");
                sb.Children.Add(da2);
            }
        }
    }
}
