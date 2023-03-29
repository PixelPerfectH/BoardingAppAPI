namespace BoardingAppAPI.Models.Database
{
    public class DBActivity
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
