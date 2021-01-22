using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer1.QueryEntities
{
    public class Komentar
    {
        public string username { get; set; }
        public string nazivPrezentacije { get; set; }
        public string datum { get; set; }
        public int brojZvezdica { get; set; }

        public string komentar { get; set; }
    }
}
