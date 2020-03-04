using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SilkroadSecurityApi;

namespace Clientless_login
{
    class Globals
    {
        public static Form1 MainWindow;
        public static ServerEnum Server = ServerEnum.None;
        public static Security security;  
        public enum ServerEnum
        {
            None,
            Gateway,
            Agent
        }
    }
}
