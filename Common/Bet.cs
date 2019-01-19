using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class Bet : Entity
    {
        public string ResultType { get; set; }
        public string ResultValue { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Money { get; set; }
    }
}
