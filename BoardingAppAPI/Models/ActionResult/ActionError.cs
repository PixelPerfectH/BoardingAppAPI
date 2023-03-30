using System.ComponentModel.DataAnnotations;

namespace BoardingAppAPI.Models.ActionResult
{
    /// <summary>
    /// Represent an action error.
    /// </summary>
    /// <typeparam name="TErrorCodeEnum">Error codes enum.</typeparam>
    public class ActionError<TErrorCodeEnum> where TErrorCodeEnum : Enum
    {
        public ActionError(string error, TErrorCodeEnum errorCode)
        {
            Error = error;
            ErrorCode = errorCode;
        }

        [Required]
        public string Error { get; init; }

        [Required]
        public TErrorCodeEnum ErrorCode { get; init; }
    }
}
