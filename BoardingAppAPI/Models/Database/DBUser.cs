using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BoardingAppAPI.Models.Database
{
    public class DBUser
    {
        public DBUser()
        {
            var task1 = new DBTask()
            {
                Name = "Task1",
                Description = "Description 1"
            };
            var task2 = new DBTask()
            {
                Name = "Task1",
                Description = "Description 1"
            };
            var task3 = new DBTask()
            {
                Name = "Task1",
                Description = "Description 1"
            };
            var task4 = new DBTask()
            {
                Name = "Task1",
                Description = "Description 1"
            };
            var task5 = new DBTask()
            {
                Name = "Task1",
                Description = "Description 1"
            };
            Tasks = new()
            {
                task1,
                task2,
                task3,
                task4,
                task5
            };
        }

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
        public required DateTime CreatedAt { get; set; }

        public List<DBTask> Tasks { get; set; }
    }
}
