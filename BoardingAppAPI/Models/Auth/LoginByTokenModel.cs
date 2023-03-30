using System.ComponentModel.DataAnnotations;

namespace BoardingAppAPI.Models.Auth
{
    public class LoginByTokenModel
    {
        [Required]
        public required string Token { get; set; }
    }
}
