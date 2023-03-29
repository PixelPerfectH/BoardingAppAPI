namespace BoardingAppAPI.Models.Database
{
    public class DBUser
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public byte[]? Avatar { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
