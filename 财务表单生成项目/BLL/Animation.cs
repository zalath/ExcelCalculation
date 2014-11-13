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
        internal static DoubleAnimationUsingKeyFrames CreateAnimate(UIElement element, int starttime, double propertyValue, string propertyName)
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
        internal static void anime_virticleMove_show(UIElement element, int starttime, double propertyValue, double opacityValue)
        {
            Storyboard sb = new Storyboard();
            anime_virticleMove_middle(ref sb, element, starttime, propertyValue, opacityValue);
            sb.Begin();
        }

        internal static void anime_virticleMove_show(UIElement element1, double propertyValue1, UIElement element2, double propertyValue2)
        {
            Storyboard sb = new Storyboard();
            anime_virticleMove_middle(ref sb, element1, 0, propertyValue1, 0);
            anime_virticleMove_middle(ref sb, element2, 400, propertyValue2, 1);
            sb.Begin();
        }

        private static void anime_virticleMove_middle(ref Storyboard sb, UIElement element, int starttime, double propertyValue, double opacityValue)
        {
            if (propertyValue != double.NaN)
            {
                DoubleAnimationUsingKeyFrames da = Animation.CreateAnimate(element, starttime, propertyValue, "(Canvas.Left)");
                sb.Children.Add(da);
            }
            if (opacityValue != double.NaN)
            {
                DoubleAnimationUsingKeyFrames da1 = Animation.CreateAnimate(element, starttime, opacityValue, "(Opacity)");
                sb.Children.Add(da1);
            }
        }
    }
}
