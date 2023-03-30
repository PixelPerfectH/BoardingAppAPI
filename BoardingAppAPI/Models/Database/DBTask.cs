namespace BoardingAppAPI.Models.Database
{
    public class DBTask
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public DBUser? User { get; set; }
    }
}
