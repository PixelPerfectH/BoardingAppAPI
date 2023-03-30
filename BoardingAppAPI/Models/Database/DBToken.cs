namespace BoardingAppAPI.Models.Database
{
    public class DBToken
    {
        public DBToken() { }

        public DBToken(string token)
        {
            Token = token;
            CreatedAt = DateTime.Now;
        }

        public int Id { get; set; }
        public string? Token { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
