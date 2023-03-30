namespace BoardingAppAPI.Helpers
{
    public class TokenHelper
    {
        private static Random random = new Random();

        public static string GenerateToken()
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 128)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
