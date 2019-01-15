using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Common
{
    public class Bet : Entity
    {
        public string ResultType { get; set; }
        public string ResultValue { get; set; }
        public int Money { get; set; }
    }    
}
