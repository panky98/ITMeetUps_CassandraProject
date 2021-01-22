using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CassandraDataLayer.QueryEntities
{
    public class Prezentacija
    {
        public string naziv_prezentacije { get; set; }
        public string datum { get; set; }
        public string interesovanje { get; set; }
        public string predavac { get; set; }
    }
}
