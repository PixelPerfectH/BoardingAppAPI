using BoardingAppAPI.Models;
using BoardingAppAPI.Models.Database;

namespace BoardingAppAPI.Tokens
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(DBUser user);

        /// <summary>
        /// Validate Jwt.
        /// </summary>
        /// <param name="token">JWT</param>
        /// <returns>User Id</returns>
        public int? ValidateJwtToken(string token);

        public RefreshToken GenerateRefreshToken(string ipAddress);
    }
}
