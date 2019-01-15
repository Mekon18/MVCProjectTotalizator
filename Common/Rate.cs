using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Rate : Entity
    {
        public SportEvent Event { get; set; }
        public DateTime DateTime { get; set; }
        public string UserId { get; set; }
        public List<Bet> Bets { get; set; }
    }
}
