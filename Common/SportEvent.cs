using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class SportEvent : Entity
    {
        public DateTime DateTime { get; set; }
        public Team FirstTeam { get; set; }
        public Team SecondTeam { get; set; }
        public KindOfSport KindOfSport { get; set; }
        public double FirstCoeficient { get; set; }
        public double SecondCoeficient { get; set; }
        public double ThirdCoeficient { get; set; }
        public double FourthCoeficient { get; set; }
        public string Status { get; set; }
    }
}
