namespace BoardingAppAPI
{
    public class AppSettings
    {
        /// <summary>
        /// JWT Private Key.
        /// </summary>
        public required string Secret { get; init; }

        /// <summary>
        /// Refresh token TTL in days.
        /// </summary>
        public required int RefreshTokenTtl { get; init; }

        /// <summary>
        /// Json Web Token TTL in minutes.
        /// </summary>
        public required int JwtTtl { get; init; }
    }
}
