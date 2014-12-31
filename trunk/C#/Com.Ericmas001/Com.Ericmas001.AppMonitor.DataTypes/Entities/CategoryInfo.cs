using System;
using System.Windows;
using System.Windows.Media;
using Com.Ericmas001.AppMonitor.DataTypes.Attributes;
using Com.Ericmas001.Util;
using Com.Ericmas001.Util.Attributes;
using Com.Ericmas001.Wpf.Helpers;

namespace Com.Ericmas001.AppMonitor.DataTypes.Entities
{
    public class CategoryInfo<TCategory>
        where TCategory : struct
    {
        private readonly SolidColorBrush m_Background = Brushes.Gray;
        private readonly Color m_ButtonColor = Colors.DarkSlateGray;

        private readonly string m_IconSmallImageName = String.Empty;
        private readonly string m_IconBigImageName = String.Empty;
        private readonly string m_Description = String.Empty;
        private readonly int m_Priorite = 100;

        public virtual ImageSource IconImageSmall
        {
            get { return String.IsNullOrEmpty(m_IconSmallImageName) ? null : Application.Current.FindResource(m_IconSmallImageName) as ImageSource; }
        }
        public virtual ImageSource IconImageBig
        {
            get { return String.IsNullOrEmpty(m_IconBigImageName) ? null : Application.Current.FindResource(m_IconBigImageName) as ImageSource; }
        }
        public virtual string IconImageSmallName
        {
            get { return m_IconSmallImageName; }
        }
        public virtual string IconImageBigName
        {
            get { return m_IconBigImageName; }
        }
        public SolidColorBrush Background
        {
            get { return m_Background; }
        }
        public Color ButtonBrush
        {
            get { return m_ButtonColor; }
        }

        public SolidColorBrush HeaderForeground
        {
            get { return ColorHelper.GetForegroundFromBackground(Background); }
        }

        public string Description
        {
            get { return m_Description; }
        }

        public int Priorite
        {
            get { return m_Priorite; }
        }

        public CategoryInfo(TCategory cat)
        {
            var imgAtt = EnumFactory<TCategory>.GetAttribute<ImageSourceAttribute>(cat);
            if (imgAtt != null)
            {
                m_IconSmallImageName = imgAtt.ImageNameSmall;
                m_IconBigImageName = imgAtt.ImageNameBig;
            }

            var brushAtt = EnumFactory<TCategory>.GetAttribute<ColorAttribute>(cat);
            if (brushAtt != null)
            {
                try
                {
                    var bc = new BrushConverter();
                    m_Background = (SolidColorBrush)bc.ConvertFromString(brushAtt.Color);
                }
                catch { }
            }

            var bbrushAtt = EnumFactory<TCategory>.GetAttribute<ButtonColorAttribute>(cat);
            if (bbrushAtt != null)
            {
                try
                {
                    var bc = new BrushConverter();
                    m_ButtonColor = ((SolidColorBrush)bc.ConvertFromString(bbrushAtt.Color)).Color;
                }
                catch { }
            }

            m_Description = EnumFactory<TCategory>.ToString(cat);

            var prioAtt = EnumFactory<TCategory>.GetAttribute<PrioriteAttribute>(cat);
            if (prioAtt != null)
                m_Priorite = prioAtt.Priorite;
        }

    }
}
