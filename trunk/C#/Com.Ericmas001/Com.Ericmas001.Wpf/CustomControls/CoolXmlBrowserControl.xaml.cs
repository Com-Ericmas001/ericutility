using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using System.Xml.Xsl;
using System.Reflection;

namespace Com.Ericmas001.Wpf.CustomControls
{
    /// <summary>
    /// Interaction logic for XmlBrowserControl.xaml
    /// </summary>
    public partial class CoolXmlBrowserControl : UserControl
    {
        public CoolXmlBrowserControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty XmlDocProperty = 
            DependencyProperty.Register(
            "XmlDoc", 
            typeof(string), 
            typeof(CoolXmlBrowserControl), 
            new UIPropertyMetadata(null, OnXmlDocChanged));

        public string XmlDoc
        {
            get
            {
                return (string)GetValue(XmlDocProperty);
            }
            set
            {
                SetValue(XmlDocProperty, value);
            }
        }

        /// <summary>
        /// Executes when XmlDoc DP is changed, Loads the xml and tranforms it using XSL provided
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        public static void OnXmlDocChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var browserControl = d as CoolXmlBrowserControl;
            if (browserControl == null) return;
            var xmlString = e.NewValue as string;
            
            try
            {

                var xmlDocument = new XmlDocument();

                var xmlDocStyled = new StringBuilder(2500);
                // mark of web - to enable IE to force webpages to run in the security zone of the location the page was saved from
                // http://msdn.microsoft.com/en-us/library/ms537628(v=vs.85).aspx
                xmlDocStyled.Append("<!-- saved from url=(0014)about:internet -->");

                Assembly asm = Assembly.GetExecutingAssembly();
                UnmanagedMemoryStream stream = (UnmanagedMemoryStream)asm.GetManifestResourceStream(String.Format("{0}.Resources.xml-pretty-print.xsl", asm.GetName().Name));

                var xslt = new XslCompiledTransform();
                ////TODO: Do not forget to change the namespace, if you move the xsl sheet to your application
                //var xsltFileStream = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "xml-pretty-print.xsl");
                if (stream != null)
                {
                    //Load the xsltFile
                    var xmlReader = XmlReader.Create(stream);
                    xslt.Load(xmlReader);
                    var settings = new XmlWriterSettings();
                    // writer for transformation
                    var writer = XmlWriter.Create(xmlDocStyled, settings);
                    if (xmlString != null) xmlDocument.LoadXml(xmlString);
                    xslt.Transform(xmlDocument, writer);

                }
                browserControl.WebBrowser.NavigateToString(String.Format("<div style=\"white-space:nowrap; display:inline;\">{0}</div>",xmlDocStyled.ToString()));
            }
            catch (Exception ex)
            {
                browserControl.WebBrowser.NavigateToString("Unable to parse xml. Correct the following errors: " + ex.Message);
            }
        }
    }
}
