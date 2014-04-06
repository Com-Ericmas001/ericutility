using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Com.Ericmas001.Windows.Forms;
using Com.Ericmas001.Util;

namespace Com.Ericmas001.Windows.Forms
{
    public partial class StepSplashForm : Form
    {
        private List<Tuple<StepSplashInfo.BoolEmptyHandler, StatePictureBox>> m_InternalSteps = new List<Tuple<StepSplashInfo.BoolEmptyHandler, StatePictureBox>>();

        private StepSplashInfo m_Info;

        public StepSplashForm(StepSplashInfo info)
        {
            this.DialogResult = DialogResult.Cancel;
            m_Info = info;

            InitializeComponent();

            this.Text = m_Info.Title;
            lblTitle.Text = m_Info.Title;

            for (int i = 0; i < m_Info.Steps.Length; ++i)
            {
                Tuple<StepSplashInfo.BoolEmptyHandler, string> step = m_Info.Steps[i];
                StatePictureBox spb = null;
                if( i == 0 )
                {
                    spb1Model.Name = "spbStep" + i;
                    lbl1Model.Name = "lblStep" + i;
                    spb = spb1Model;
                }
                else
                {
                    spb = new StatePictureBox();
                    spb.BackgroundImageLayout = spb1Model.BackgroundImageLayout;
                    spb.Etat = Com.Ericmas001.Windows.Forms.StatePictureBoxStates.None;
                    spb.Location = new System.Drawing.Point(spb1Model.Left, spb1Model.Top + (i * spb1Model.Height) + (i * 15));
                    spb.Name = "spbStep" + i;
                    spb.Size = spb1Model.Size;
                    spb.TabStop = false;
                    panel2.Controls.Add(spb);

                    Label lbl = new Label();
                    lbl.Font = lbl1Model.Font;
                    lbl.Location = new System.Drawing.Point(lbl1Model.Left, spb1Model.Top + (i * spb1Model.Height) + (i * 15));
                    lbl.Name = "lblStep" + i;
                    lbl.Size = lbl1Model.Size;
                    lbl.Text = step.Item2;
                    lbl.TextAlign = lbl1Model.TextAlign;
                    panel2.Controls.Add(lbl);
                }

                m_InternalSteps.Add(new Tuple<StepSplashInfo.BoolEmptyHandler, StatePictureBox>(step.Item1, spb));
            }

            this.Size = new Size(this.Width, this.Height + ((m_Info.Steps.Length - 1) * (15 + spb1Model.Height)));

            new Thread(new ThreadStart(DoIt)).Start();
        }

        private void DoIt()
        {
            m_Info.Init();

            if (ExecuteSteps())
            {
                this.DialogResult = DialogResult.OK;
                Quit();
            }
            else
                Error();
        }
        private bool ExecuteSteps()
        {
            bool ok = true;
            foreach (Tuple<StepSplashInfo.BoolEmptyHandler, StatePictureBox> step in m_InternalSteps)
            {
                if (ok)
                {
                    step.Item2.Etat = StatePictureBoxStates.Waiting;
                    if (step.Item1.Invoke())
                        step.Item2.Etat = StatePictureBoxStates.Ok;
                    else
                    {
                        step.Item2.Etat = StatePictureBoxStates.Bad;
                        ok = false;
                    }
                }
            }
            return ok;
        }

        private void Error()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new EmptyHandler(Error), new object[] { });
                return;
            }
            btnCancel.Enabled = true;
        }

        private void Quit()
        {
            if (this.InvokeRequired)
            {
                try
                {
                    this.Invoke(new EmptyHandler(Quit), new object[] { });
                }
                catch
                {

                }
                return;
            }
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Quit();
        }
    }
}
