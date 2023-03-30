using System.ComponentModel.DataAnnotations;

namespace BoardingAppAPI.Models.Auth
{
    public class RegisterModel
    {
        [Required]
        public required string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
