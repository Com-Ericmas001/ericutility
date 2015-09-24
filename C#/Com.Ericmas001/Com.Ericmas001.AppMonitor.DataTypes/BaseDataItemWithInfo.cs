using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Com.Ericmas001.Portable.Util.Entities;

namespace Com.Ericmas001.AppMonitor.DataTypes
{
    public abstract class BaseDataItemWithInfo<TInfo> : IDataItem
        where TInfo : IDataItemInfo
    {
        public TInfo Info { get; private set; }

        public DateTime DateAndTimeValue { get { return Info.DateAndTime; } }

        public string Date { get { return Info.DateAndTime.ToString("yyyy-MM-dd"); } }
        public string DateWithHour { get { return Info.DateAndTime.ToString("yyyy-MM-dd HH"); } }
        public string Hour { get { return Info.DateAndTime.ToString("HH"); } }
        public string Time { get { return Info.DateAndTime.ToString("HH:mm:ss.fff"); } }
        public string DateAndTime { get { return Info.DateAndTime.ToString("yyyy-MM-dd HH:mm:ss.fff"); } }

        public BaseDataItemWithInfo(TInfo info)
        {
            Info = info;
        }


        public abstract string ObtainValue(string field);
        public abstract string ObtainFilterValue(string field);

        protected virtual Dictionary<string, string> ObtainAllFields()
        {
            var nfo = new Dictionary<string, string>();

            foreach (PropertyInfo field in GetAllPropertys())
            {
                if (CanDisplayField(field))
                {
                    string fieldValue = string.Empty;
                    string valOverride = GetFieldDisplayValue(field);
                    if (valOverride != null)
                        fieldValue = valOverride;
                    else
                    {
                        object val = field.GetValue(Info, null);
                        if (val != null)
                            fieldValue = val.ToString();
                    }

                    if (!string.IsNullOrEmpty(fieldValue) || !CanRemoveEmptyField(field))
                        nfo.Add(field.Name, fieldValue);
                }
            }

            return nfo;
        }

        public string TextDescription
        {
            get
            {
                StringWriter sw = new StringWriter();

                TextDescriptionBeforeMainInfo(sw);

                foreach (KeyValuePair<string, string> kvp in ObtainAllFields())
                    sw.WriteLine("{0}: {1}", kvp.Key, kvp.Value);

                TextDescriptionAfterMainInfo(sw);

                return sw.ToString();
            }
        }
        public string HtmlDescription
        {
            get
            {
                StringWriter sw = new StringWriter();
                sw.Write("<div style=\"font-family: Consolas; font-size:12px\">");

                HtmlDescriptionBeforeMainInfo(sw);

                foreach (KeyValuePair<string, string> kvp in ObtainAllFields())
                {
                    sw.WriteLine("<b>{0}: </b>{1}", kvp.Key, kvp.Value);
                }

                HtmlDescriptionAfterMainInfo(sw);

                sw.Write("</div>");

                return sw.ToString().Replace("\n", "<br />" + "\n").Replace("  ", "&nbsp;&nbsp;");
            }
        }
        protected virtual bool CanRemoveEmptyField(PropertyInfo field)
        {
            return false;
        }
        protected virtual bool CanDisplayField(PropertyInfo field)
        {
            return true;
        }

        protected virtual string GetFieldDisplayValue(PropertyInfo field)
        {
            return null;
        }

        protected virtual PropertyInfo[] GetAllPropertys()
        {
            return typeof(TInfo).GetProperties();
        }


        protected virtual void TextDescriptionBeforeMainInfo(StringWriter sw)
        {
        }


        protected virtual void TextDescriptionAfterMainInfo(StringWriter sw)
        {
        }


        protected virtual void HtmlDescriptionBeforeMainInfo(StringWriter sw)
        {
        }


        protected virtual void HtmlDescriptionAfterMainInfo(StringWriter sw)
        {
        }
    }
}
