using Com.Ericmas001.Util;
using System;
using System.Windows.Forms;
using Com.Ericmas001.Windows.Forms.Properties;

namespace Com.Ericmas001.Windows.Forms
{
    public enum StatePictureBoxStates
    {
        None,
        Waiting,
        Bad,
        Ok
    }

    public class StatePictureBox : PictureBox
    {
        private Timer m_WaitingTimer;
        private int m_WaitingCounter;
        private StatePictureBoxStates m_Etat = StatePictureBoxStates.None;

        public StatePictureBoxStates Etat
        {
            get
            {
                return m_Etat;
            }
            set
            {
                if (m_Etat != value)
                {
                    m_Etat = value;
                    UpdateBackgroundImage();
                }
            }
        }

        public StatePictureBox()
        {
            BackgroundImageLayout = ImageLayout.Stretch;
        }

        protected virtual void ChooseImage(StatePictureBoxStates state)
        {
            switch (state)
            {
                case StatePictureBoxStates.None:
                    BackgroundImage = null;
                    break;

                case StatePictureBoxStates.Waiting:
                    if (m_WaitingTimer == null)
                    {
                        m_WaitingTimer = new Timer();
                        m_WaitingTimer.Interval = 100;
                        m_WaitingTimer.Tick += waitingTimer_Tick;
                        m_WaitingTimer.Start();
                        BackgroundImage = Resources.waiting0;
                    }
                    break;

                case StatePictureBoxStates.Bad:
                    BackgroundImage = Resources.bad;
                    break;

                case StatePictureBoxStates.Ok:
                    BackgroundImage = Resources.OK;
                    break;
            }
        }

        private void UpdateBackgroundImage()
        {
            if (InvokeRequired)
            {
                Invoke(new EmptyHandler(UpdateBackgroundImage), new object[] { });
                return;
            }
            ChooseImage(m_Etat);
            Invalidate();
        }

        private void waitingTimer_Tick(object sender, EventArgs e)
        {
            if (m_Etat == StatePictureBoxStates.Waiting)
            {
                if (InvokeRequired)
                {
                    Invoke(new EventHandler(waitingTimer_Tick), new[] { sender, e });
                    return;
                }
                m_WaitingCounter++;
                m_WaitingCounter %= 8;
                switch (m_WaitingCounter)
                {
                    case 0:
                        BackgroundImage = Resources.waiting0;
                        break;

                    case 1:
                        BackgroundImage = Resources.waiting1;
                        break;

                    case 2:
                        BackgroundImage = Resources.waiting2;
                        break;

                    case 3:
                        BackgroundImage = Resources.waiting3;
                        break;

                    case 4:
                        BackgroundImage = Resources.waiting4;
                        break;

                    case 5:
                        BackgroundImage = Resources.waiting5;
                        break;

                    case 6:
                        BackgroundImage = Resources.waiting6;
                        break;

                    case 7:
                        BackgroundImage = Resources.waiting7;
                        break;
                }
            }
            else
            {
                m_WaitingTimer.Stop();
                m_WaitingTimer = null;
            }
        }
    }
}