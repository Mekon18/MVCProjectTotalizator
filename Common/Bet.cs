using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class Bet : Entity
    {
        public string ResultType { get; set; }
        public string ResultValue { get; set; }
        public int Money { get; set; }

    }
}
