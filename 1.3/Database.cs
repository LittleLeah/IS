using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1._3
{
    class Database
    {
        private int Number { get; set; }
        private string Sentence { get; set; }
        private DateTime Date { get; set; }

        public Database(int number, string sentence, DateTime date) {
            this.Number = number;
            this.Sentence = sentence;
            this.Date = date;
        }

        public string[] getData()
        {
            string[] data = { Number.ToString(), Sentence, Date.ToString() };
            return data;
        }

       
    }
}
