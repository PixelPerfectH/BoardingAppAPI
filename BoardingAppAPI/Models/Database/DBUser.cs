using System.ComponentModel.DataAnnotations;

namespace BoardingAppAPI.Models.Database
{
    public class DBUser
    {
        public DBUser() { }

        [Key]
        public long Id { get; set; }

        [Required]
        public required string? UserName { get; set; }

        public string? Email { get; set; }

        [Required]
        public required string? HashedPassword { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public byte[]? Avatar { get; set; }

        public required DateTime CreatedAt { get; set; }
    }
}
