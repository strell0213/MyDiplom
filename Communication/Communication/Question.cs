using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication
{
    internal class Question
    {
        public int ID { get; set; }
        private string questione, answer;
        public string Questione { get { return questione; } set { questione = value; } }
        public string Answer { get { return answer; } set { answer = value; } }

        public Question() { }

        public Question(string questione, string answer)
        {
            this.questione = questione;
            this.answer = answer;
        }
    }
}
