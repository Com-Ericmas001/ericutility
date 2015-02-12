using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;
using System.Diagnostics;
using mshtml;

namespace Com.Ericmas001.Wpf.CustomControls
{
    /// <summary>
    /// Interaction logic for XmlBrowserControl.xaml
    /// </summary>
    public partial class CoolHtmlBrowserControl : UserControl
    {
        public CoolHtmlBrowserControl()
        {
            InitializeComponent(); 
            WebBrowser.Navigating += new System.Windows.Navigation.NavigatingCancelEventHandler(WebBrowser_Navigating);
        }
        
        public static readonly DependencyProperty HtmlDocProperty = 
            DependencyProperty.Register(
            "HtmlDoc", 
            typeof(string), 
            typeof(CoolHtmlBrowserControl),
            new UIPropertyMetadata(null, OnHtmlDocChanged));

        public static readonly DependencyProperty CurrentAnchorProperty = 
            DependencyProperty.Register(
            "CurrentAnchor", 
            typeof(string), 
            typeof(CoolHtmlBrowserControl),
            new UIPropertyMetadata(null, OnCurrentAnchorChanged));
        
        public string HtmlDoc
        {
            get
            {
                return (string)GetValue(HtmlDocProperty);
            }
            set
            {
                SetValue(HtmlDocProperty, value);
            }
        }
        public string CurrentAnchor
        {
            get
            {
                return (string)GetValue(CurrentAnchorProperty);
            }
            set
            {
                SetValue(CurrentAnchorProperty, value);
            }
        }

        /// <summary>
        /// Executes when XmlDoc DP is changed, Loads the xml and tranforms it using XSL provided
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        public static void OnHtmlDocChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var browserControl = d as CoolHtmlBrowserControl;
            var htmlString = "<meta http-equiv='Content-Type' content='text/html;charset=UTF-8'>" + e.NewValue;
            if (browserControl == null || String.IsNullOrEmpty(htmlString)) return;
            browserControl.WebBrowser.NavigateToString(htmlString);
        }

        public static void OnCurrentAnchorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var browserControl = d as CoolHtmlBrowserControl;
            var anchor = (string)e.NewValue;
            if (browserControl == null || string.IsNullOrEmpty(anchor)) return;
            HTMLDocument htmlDoc = (HTMLDocument)browserControl.WebBrowser.Document;
            IHTMLElementCollection elements = htmlDoc.getElementsByTagName("A");
            foreach (IHTMLElement element in elements)
            {
                if (element.getAttribute("Name") == anchor)
                {
                    element.scrollIntoView(true);
                    return;
                }
            }
        }

        static void WebBrowser_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            if (e.Uri != null)
            {
                // cancel navigation to the clicked link in the webBrowser control
                e.Cancel = true;

                var startInfo = new ProcessStartInfo
                {
                    FileName = e.Uri.ToString()
                };

                Process.Start(startInfo);
            }
        }
    }
}
