namespace BoardingAppAPI.Models.ActionResult
{
    public class RegisterResult : LoginResult
    {
    }

    public class RegisterError : ActionError<RegisterErrorCode>
    {
        public RegisterError(string error, RegisterErrorCode errorCode) : base(error, errorCode)
        {
        }

        public static RegisterError EmailIsTaken =>
            new(RegisterErrorMessages.EmailIsTaken, RegisterErrorCode.EmailIsAlreadyRegistered);

        // TODO: Add more possible errors
    }

    public enum RegisterErrorCode
    {

        /// <summary>
        /// No error.
        /// </summary>
        Ok = 0,
        /// <summary>
        /// Email is already registered.
        /// </summary>
        EmailIsAlreadyRegistered = 1,
        /// <summary>
        /// User name doesn't meet defined requrements.
        /// </summary>
        UserNameDoesNotMeetRequirements = 2
    }

    public static class RegisterErrorMessages
    {
        public static readonly string EmailIsTaken = "This e-mail is already registered.";
    }
}
