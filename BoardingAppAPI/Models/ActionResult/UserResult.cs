using BoardingAppAPI.Models.Database;

namespace BoardingAppAPI.Models.ActionResult
{
    public class UserResult
    {
        public UserResult() { }

        public UserResult(DBUser user)
        {
            UserName = user.UserName;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Avatar = user.Avatar;
        }

        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public byte[]? Avatar { get; set; }
    }
}
