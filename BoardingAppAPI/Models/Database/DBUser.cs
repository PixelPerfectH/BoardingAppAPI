using System.ComponentModel.DataAnnotations;

namespace BoardingAppAPI.Models.Database
{
    public class DBUser
    {
        public DBUser() { }


        [Key]
        public long Id { get; set; }

        /// <summary>
        /// UserName.
        /// </summary>
        [Required]
        public required string? UserName { get; set; }

        /// <summary>
        /// User's email.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// BCrypt hashed password.
        /// </summary>
        [Required]
        public required string? HashedPassword { get; set; }

        /// <summary>
        /// User's firstname.
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// User's lastname.
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// User's avatar.
        /// </summary>
        public byte[]? Avatar { get; set; }

        /// <summary>
        /// Account creation date.
        /// </summary>
        public required DateTime CreatedAt { get; set; }

        /// <summary>
        /// List of user's refresh tokens.
        /// </summary>
        public required List<RefreshToken> RefreshTokens { get; set; }
    }
}
