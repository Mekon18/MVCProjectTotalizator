using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Common
{
    public class Rate : Entity
    {
        public int EventId { get; set; }
        public DateTime DateTime { get; set; }
        public string UserId { get; set; }
        public List<int> BetsId { get; set; }
    }
    public class SportEvent : Entity
    {
        public DateTime DateTime { get; set; }
        public Team FirstTeam { get; set; }
        public Team SecondTeam { get; set; }
        public string KindOfSport { get; set; }
        public double FirstCoeficient { get; set; }
        public double SecondCoeficient { get; set; }
    }
    public class Entity
    {
        public int Id { get; set; }
    }    
    public class Team : Entity
    {
        public string CountryName { get; set; }
        public string Name { get; set; }
    }
    public class Bet : Entity
    {
        public int TeamId { get; set; } // team you want to win
        public double Money { get; set; }
    }
}
