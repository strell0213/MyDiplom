using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication
{
    internal class Message
    {
        public int ID { get; set; }
        
        public int Nick2 { get; set; }
        private string Nick1, Title, Mes, ImMes;
        public string nick1 { get { return Nick1; } set { Nick1 = value; } }
        public string title { get { return Title; } set {Title = value; } }
        public string mes { get { return Mes; } set { Mes = value; } }
        public string imMes { get { return ImMes; } set { ImMes = value; } }

        public Message() { }
        public Message(string Nick1, int Nick2,string Title, string Mes, string imMes)
        {
            this.Nick1 = Nick1;
            this.Nick2 = Nick2;
            this.Title = Title;
            this.Mes = Mes;
            this.imMes = imMes;
        }
    }
}
