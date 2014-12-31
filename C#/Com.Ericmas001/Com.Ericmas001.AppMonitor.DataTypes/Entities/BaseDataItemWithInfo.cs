using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Com.Ericmas001.AppMonitor.DataTypes.Entities
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
        public Dictionary<string, string> Information
        {
            get
            {
                Dictionary<string, string> nfo = new Dictionary<string, string>();

                string[] mainInfo = ObtenirToutesLesPropsDeBase();
                foreach (PropertyInfo field in ObtenirToutesLesProps())
                {
                    if (PeutAfficher(field))
                    {
                        string valeur = string.Empty;
                        string valOverride = ObtenirValeur(field);
                        if (valOverride != null)
                        {
                            valeur = valOverride;
                        }
                        else
                        {
                            object val = field.GetValue(Info, null);
                            if (val != null)
                            {
                                valeur = val.ToString();
                            }
                        }
                        if (!string.IsNullOrEmpty(valeur) || mainInfo.Contains(field.Name))
                        {
                            nfo.Add(field.Name, valeur);
                        }
                    }
                }

                return nfo;
            }
        }
        public string TextDescription
        {
            get
            {
                StringWriter sw = new StringWriter();

                TextDescriptionBeforeMainInfo(sw);

                foreach (KeyValuePair<string, string> kvp in Information)
                {
                    sw.WriteLine("{0}: {1}", kvp.Key, kvp.Value);
                }

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

                foreach (KeyValuePair<string, string> kvp in Information)
                {
                    sw.WriteLine("<b>{0}: </b>{1}", kvp.Key, kvp.Value);
                }

                HtmlDescriptionAfterMainInfo(sw);

                sw.Write("</div>");

                return sw.ToString().Replace("\n", "<br />" + "\n").Replace("  ", "&nbsp;&nbsp;");
            }
        }
        protected virtual bool PeutAfficher(PropertyInfo field)
        {
            return true;
        }

        protected virtual string ObtenirValeur(PropertyInfo field)
        {
            return null;
        }

        protected virtual PropertyInfo[] ObtenirToutesLesProps()
        {
            return typeof(TInfo).GetProperties();
        }

        protected virtual string[] ObtenirToutesLesPropsDeBase()
        {
            return typeof(TInfo).GetProperties().Select(x => x.Name).ToArray();
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
