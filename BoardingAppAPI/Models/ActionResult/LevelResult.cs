using BoardingAppAPI.Models.Database;

namespace BoardingAppAPI.Models.ActionResult
{
    public class LevelResult
    {
        public long Level { get; set; }
        public string? Name { get; set; }
        public List<DBTask>? Tasks { get; set; }
    }
}
