using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace EricUtility2011.Data
{
    public enum ParamDir
    {
        Input,
        Output
    }
    public class SPParam
    {
        private SqlParameter m_Parm;

        public SqlParameter Parm { get { return m_Parm; } }

        public SPParam(SqlParameter parm)
            : this(parm, ParamDir.Input, null)
        {

        }
        public SPParam(SqlParameter parm, ParamDir dir)
            : this(parm, dir, null)
        {

        }
        public SPParam(SqlParameter parm, object val)
            : this(parm, ParamDir.Input, val)
        {

        }
        public SPParam(SqlParameter parm, ParamDir dir, object val)
        {
            m_Parm = parm;
            if (dir == ParamDir.Input)
                m_Parm.Direction = System.Data.ParameterDirection.Input;
            else
                m_Parm.Direction = System.Data.ParameterDirection.Output;
            m_Parm.Value = val;
        }
    }
}
