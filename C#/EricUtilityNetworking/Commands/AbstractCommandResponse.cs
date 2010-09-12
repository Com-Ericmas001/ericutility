﻿using System;
using System.Collections.Generic;
using System.Text;
using EricUtility;

namespace EricUtility.Networking.Commands
{
    public abstract class AbstractCommandResponse<T>: AbstractCommand
        where T : AbstractCommand
    {
        private readonly T m_Command;

        public AbstractCommandResponse(T command)
        {
            m_Command = command;
        }
        public T Command
        {
            get { return m_Command; }
        } 


        public override void Encode(StringBuilder sb) 
        {
            m_Command.Encode(sb);
        }

    }
}
