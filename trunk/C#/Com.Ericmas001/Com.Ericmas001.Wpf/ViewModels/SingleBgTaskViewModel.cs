using System;
using System.ComponentModel;
using System.Windows;
using Com.Ericmas001.Util;

namespace Com.Ericmas001.Wpf.ViewModels
{
    public class SingleBgTaskViewModel
    {
        public event EventHandler OnRequestClose = delegate { };

        private BackgroundWorker m_Bw;
        public bool Success { get; private set; }
        public IWorkInBackground Task { get; set; }

        public SingleBgTaskViewModel()
        {
            if (!DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                Success = false;
                m_Bw = new BackgroundWorker();
                m_Bw.RunWorkerCompleted += m_Bw_RunWorkerCompleted;
                m_Bw.DoWork += m_Bw_DoWork;
            }
        }

        public void Start()
        {
            m_Bw.RunWorkerAsync(Task);
        }

        void m_Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            ((IWorkInBackground)e.Argument).Work(sender as BackgroundWorker, e);
        }

        void m_Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Success = e.Error == null && !e.Cancelled;
            OnRequestClose(this, new EventArgs());
        }
    }
}
