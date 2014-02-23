using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EricUtility.Windows.Forms
{
    public class StepSplashForm : Form
    {
        protected delegate bool BoolEmptyHandler();
        protected Tuple<BoolEmptyHandler, StatePictureBox>[] m_Steps;

        protected bool ExecuteSteps()
        {
            bool ok = true;
            foreach (Tuple<BoolEmptyHandler, StatePictureBox> step in m_Steps)
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
    }
}
