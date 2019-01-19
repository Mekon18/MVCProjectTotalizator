using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class User
    {
        public string Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public int Money { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
