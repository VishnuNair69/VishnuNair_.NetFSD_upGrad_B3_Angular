using System.ComponentModel.DataAnnotations;

namespace WebApplication15.Models
{
    public class UserInfo
    {
        [Key]
        public int Id { get; set; }

        public string EmailId { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }
}
