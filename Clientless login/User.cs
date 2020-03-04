using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Clientless_login
{
    public class User
    {
        public string Username;
        public string Password;
        public string CharName16;
        public User(string u, string p, string c) {
            this.setUsername(u);
            this.setPassword(p);
            this.setCharName16(c);
        }

        public void setUsername(String username)
        {
            this.Username = username;
        }

        public void setPassword(String password)
        {
            this.Password = password;
        }

        public void setCharName16(String charname)
        {
            this.CharName16 = charname;
        }
    }
}
