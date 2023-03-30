namespace BoardingAppAPI.Models.ActionResult
{
    public class RefreshTokenLoginResult
    {
    }

    public class RefreshTokenLoginError : ActionError<RefreshTokenLoginErrorCode>
    {
        public RefreshTokenLoginError(string error, RefreshTokenLoginErrorCode errorCode) : base(error, errorCode)
        {
        }

        public static readonly RefreshTokenLoginError EmptyToken = new(RefreshTokenLoginErrorMessages.EmptyToken, RefreshTokenLoginErrorCode.EmptyToken);

        public static readonly RefreshTokenLoginError InvalidToken = new(RefreshTokenLoginErrorMessages.InvalidToken, RefreshTokenLoginErrorCode.InvalidToken);
    }

    public enum RefreshTokenLoginErrorCode
    {
        Ok = 0,
        EmptyToken = 1,
        InvalidToken = 2
    }

    public class RefreshTokenLoginErrorMessages
    {
        public static readonly string EmptyToken = "Token is empty";

        public static readonly string InvalidToken = "Token is invalid";
    }
}
