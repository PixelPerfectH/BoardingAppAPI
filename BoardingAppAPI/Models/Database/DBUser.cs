namespace BoardingAppAPI.Models.Database
{
    public class DBUser
    {
        public DBUser() { }

        public DBUser(string userName, string password, string token)
        {
            UserName = userName;
            Password = BCrypt.Net.BCrypt.HashPassword(password);
            Avatar = Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAEAAAABACAYAAACqaXHeAAAAZElEQVR42u3QAQ0AAAQAMMLJJTo5zB/hWRMdj6UAAQIECBAgQIAAAQIECBAgQIAAAQIECBAgQIAAAQIECBAgQIAAAQIECBAgQIAAAQIECBAgQIAAAQIECBAgQIAAAQIECBBw3wLg+nEBahIYwAAAAABJRU5ErkJggg==");
            CreatedAt = DateTime.Now;
            Tokens = new()
            {
                new(token)
            };
        }

        public int Id { get; set; }

        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public byte[]? Avatar { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<DBToken> Tokens { get; set; }
    }
}
