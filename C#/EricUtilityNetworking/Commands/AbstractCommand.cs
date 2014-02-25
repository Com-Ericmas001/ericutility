﻿using System;
using System.Text;

namespace EricUtility.Networking.Commands
{
    public abstract class AbstractCommand
    {
        public static string CommandNameField { get { return "COMMAND_NAME"; } }

        public string CommandName { get { return (string)this.GetType().GetField(CommandNameField).GetValue(null); } }

        public abstract string Encode();

        public AbstractCommand()
        {
            if (this.GetType().GetField(CommandNameField) == null)
                throw new Exception("You need a public static field named '" + CommandNameField + "' that gives the name of the command!");
        }
    }
}