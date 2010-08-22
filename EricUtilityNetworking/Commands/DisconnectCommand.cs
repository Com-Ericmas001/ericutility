using System;
using System.Collections.Generic;
using System.Text;
using EricUtility;

namespace EricUtility.Networking.Commands
{
    public class DisconnectCommand : AbstractCommand
    {
        protected override string CommandName
        {
            get { return DisconnectCommand.COMMAND_NAME; }
        }
        public static string COMMAND_NAME = "DISCONNECT";


        public DisconnectCommand(StringTokenizer argsToken)
        {
        }

        public DisconnectCommand()
        {
        }
    }
}
