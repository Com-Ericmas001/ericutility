namespace Com.Ericmas001.Portable.Util
{
    public abstract class AbstractLoadingTuple
    {
        protected bool m_Loaded;
        private bool m_Loading;

        public bool Loaded { get { return m_Loaded; } }

        protected abstract void OnLoad();

        public void ForceLoad()
        {
            NeedToReload();
            LoadIfNeeded();
        }

        public void NeedToReload()
        {
            if (!m_Loading)
                m_Loaded = false;
        }

        public void LoadIfNeeded()
        {
            if (!m_Loaded && !m_Loading)
            {
                m_Loading = true;
                OnLoad();
                m_Loaded = true;
                m_Loading = false;
            }
        }
    }
}