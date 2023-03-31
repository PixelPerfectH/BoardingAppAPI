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
                Description = "Description 1",
                IsActive = true
            };
            var task2 = new DBTask()
            {
                Name = "Task2",
                Description = "Description 2",
                IsActive = true
            };
            var task3 = new DBTask()
            {
                Name = "Task3",
                Description = "Description 3",
                IsActive = true
            };
            var task4 = new DBTask()
            {
                Name = "Task4",
                Description = "Description 4",
                IsActive = true
            };
            var task5 = new DBTask()
            {
                Name = "Task5",
                Description = "Description 5",
                IsActive = true
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
        public DateTime CreatedAt { get; set; }

        public List<DBTask> Tasks { get; set; }
    }
}
