using System;
using System.Windows;
using System.Windows.Media;
using Com.Ericmas001.AppMonitor.DataTypes.Attributes;
using Com.Ericmas001.Util;
using Com.Ericmas001.Util.Entities.Attributes;
using Com.Ericmas001.Wpf.Entities.Attributes;
using Com.Ericmas001.Wpf.Entities;
using Com.Ericmas001.Wpf.Helpers;

namespace Com.Ericmas001.AppMonitor.DataTypes
{
    public class CategoryInfo<TCategory> : TabSectionInfo
        where TCategory : struct
    {

        public CategoryInfo(TCategory cat)
        {
            var imgAtt = EnumFactory<TCategory>.GetAttribute<ImageSourceAttribute>(cat);
            if (imgAtt != null)
            {
                IconImageSmallName = imgAtt.ImageNameSmall;
                IconImageBigName = imgAtt.ImageNameBig;
            }

            var brushAtt = EnumFactory<TCategory>.GetAttribute<ColorAttribute>(cat);
            if (brushAtt != null)
            {
                try
                {
                    var bc = new BrushConverter();
                    Background = (SolidColorBrush)bc.ConvertFromString(brushAtt.Color);
                }
                catch { }
            }

            var bbrushAtt = EnumFactory<TCategory>.GetAttribute<ButtonColorAttribute>(cat);
            if (bbrushAtt != null)
            {
                try
                {
                    var bc = new BrushConverter();
                    ButtonBrush = ((SolidColorBrush)bc.ConvertFromString(bbrushAtt.Color)).Color;
                }
                catch { }
            }

            Description = EnumFactory<TCategory>.ToString(cat);

            var prioAtt = EnumFactory<TCategory>.GetAttribute<PriorityAttribute>(cat);
            if (prioAtt != null)
                Priorite = prioAtt.Priority;
        }

    }
}
