using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using Com.Ericmas001.Util;

namespace Com.Ericmas001.Windows.Forms
{
    public partial class StepSplashForm : Form
    {
        private readonly List<Tuple<StepSplashInfo.BoolEmptyHandler, StatePictureBox>> m_InternalSteps = new List<Tuple<StepSplashInfo.BoolEmptyHandler, StatePictureBox>>();

        private readonly StepSplashInfo m_Info;

        public StepSplashForm(StepSplashInfo info)
        {
            DialogResult = DialogResult.Cancel;
            m_Info = info;

            InitializeComponent();

            Text = m_Info.Title;
            lblTitle.Text = m_Info.Title;

            for (var i = 0; i < m_Info.Steps.Length; ++i)
            {
                var step = m_Info.Steps[i];
                StatePictureBox spb;
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
                    spb.Etat = StatePictureBoxStates.None;
                    spb.Location = new Point(spb1Model.Left, spb1Model.Top + (i * spb1Model.Height) + (i * 15));
                    spb.Name = "spbStep" + i;
                    spb.Size = spb1Model.Size;
                    spb.TabStop = false;
                    panel2.Controls.Add(spb);

                    var lbl = new Label();
                    lbl.Font = lbl1Model.Font;
                    lbl.Location = new Point(lbl1Model.Left, spb1Model.Top + (i * spb1Model.Height) + (i * 15));
                    lbl.Name = "lblStep" + i;
                    lbl.Size = lbl1Model.Size;
                    lbl.Text = step.Item2;
                    lbl.TextAlign = lbl1Model.TextAlign;
                    panel2.Controls.Add(lbl);
                }

                m_InternalSteps.Add(new Tuple<StepSplashInfo.BoolEmptyHandler, StatePictureBox>(step.Item1, spb));
            }

            Size = new Size(Width, Height + ((m_Info.Steps.Length - 1) * (15 + spb1Model.Height)));

            new Thread(DoIt).Start();
        }

        private void DoIt()
        {
            m_Info.Init();

            if (ExecuteSteps())
            {
                DialogResult = DialogResult.OK;
                Quit();
            }
            else
                Error();
        }
        private bool ExecuteSteps()
        {
            var ok = true;
            foreach (var step in m_InternalSteps)
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
            if (InvokeRequired)
            {
                Invoke(new EmptyHandler(Error), new object[] { });
                return;
            }
            btnCancel.Enabled = true;
        }

        private void Quit()
        {
            if (InvokeRequired)
            {
                try
                {
                    Invoke(new EmptyHandler(Quit), new object[] { });
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
            DialogResult = DialogResult.Cancel;
            Quit();
        }
    }
}
