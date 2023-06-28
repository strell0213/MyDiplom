using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication
{
    internal class Online
    {
        public int ID { get; set; }
        private string OnlineStatus;
        public string onlineStatus { get { return OnlineStatus; } set { value = OnlineStatus; } }
        public Online() { }
        public Online(string OnlineStatus)
        {
            this.OnlineStatus = OnlineStatus;
        }
    }
}
