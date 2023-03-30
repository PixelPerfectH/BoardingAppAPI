using System.ComponentModel.DataAnnotations;

namespace BoardingAppAPI.Models.ActionResult
{
    public class LoginResult
    {
        /// <summary>
        /// JSON Web Token.
        /// </summary>
        [Required]
        public required string Jwt { get; set; }
    }

    public class LoginError : ActionError<LoginErrorCode>
    {
        public LoginError(string error, LoginErrorCode errorCode) : base(error, errorCode) { }

        public static LoginError InvalidCredentials => new(LoginErrorMessages.EmailOrPasswordInvalid, LoginErrorCode.EmailOrPasswordInvalid);

        // TODO: Add more possible errors.
    }

    public enum LoginErrorCode
    {

        /// <summary>
        /// No error.
        /// </summary>
        Ok = 0,
        /// <summary>
        /// Login or email was invalid.
        /// </summary>
        LoginOrEmailInvalid = 1,
        /// <summary>
        /// E-mail or password is invalid.
        /// </summary>
        EmailOrPasswordInvalid = 2
    }

    public static class LoginErrorMessages
    {
        public static readonly string LoginOrEmailInvalid = "E-mail or login is invalid.";

        public static readonly string EmailOrPasswordInvalid = "E-mail or password is invalid.";
    }
}
