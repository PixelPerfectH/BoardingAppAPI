using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BoardingAppAPI.Models.Database
{
    public class DBUser
    {
        [Key]
        [JsonIgnore]
        public long Id { get; set; }

        public required string? UserName { get; set; }

        [JsonIgnore]
        public string? Email { get; set; }

        [JsonIgnore]
        public string? HashedPassword { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public byte[]? Avatar { get; set; }

        [JsonIgnore]
        public DateTime CreatedAt { get; set; }

        public List<DBTask> Tasks { get; set; }
    }
}
