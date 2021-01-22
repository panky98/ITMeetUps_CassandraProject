using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer1.QueryEntities
{
    public class User
    {
        public string username { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        public IList<string> interesovanja { get; set; }
    }
}
