namespace BoardingAppAPI.Models.Database
{
    public class DBUser
    {
        public DBUser() { }

        public DBUser(string userName, string password)
        {
            UserName = userName;
            Password = BCrypt.Net.BCrypt.HashPassword(password);
            CreatedAt= DateTime.Now;
        }

        public int Id { get; set; }

        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public byte[]? Avatar { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
