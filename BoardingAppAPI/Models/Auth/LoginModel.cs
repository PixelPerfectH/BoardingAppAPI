using System.ComponentModel.DataAnnotations;

namespace BoardingAppAPI.Models.Auth
{
    public class LoginModel
    {
        [Required]
        public required string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
