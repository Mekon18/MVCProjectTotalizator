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
    public class SportEvent : Entity
    {
        public DateTime DateTime { get; set; }
        public Team FirstTeam { get; set; }
        public Team SecondTeam { get; set; }
        public string KindOfSport { get; set; }
        public double FirstCoeficient { get; set; }
        public double SecondCoeficient { get; set; }
        public double ThirdCoeficient { get; set; }
        public double FourthCoeficient { get; set; }
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
        public string ResultType { get; set; }
        public string ResultValue { get; set; }
        public int Money { get; set; }
    }
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public int Money { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
