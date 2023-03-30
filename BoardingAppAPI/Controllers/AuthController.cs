using BoardingAppAPI.Helpers;
using BoardingAppAPI.Models;
using BoardingAppAPI.Models.ActionResult;
using BoardingAppAPI.Models.Auth;
using BoardingAppAPI.Models.Database;
using BoardingAppAPI.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;

namespace BoardingAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private const string RefreshTokenKey = "RefreshToken";
        private readonly ILogger<AuthController> _logger;
        private readonly BoardingAppContext _context;
        private readonly IJwtUtils _jwtUtils;
        private readonly AppSettings _appSettings;

        public AuthController(BoardingAppContext context, IJwtUtils jwtUtils, AppSettings appSettings, ILogger<AuthController> logger)
        {
            _logger = logger;
            _context = context;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings;
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(LoginError))]
        public async Task<IActionResult> LoginByPasswordAsync([FromBody] LoginByPasswordModel model, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(user => user.UserName == model.UserName, cancellationToken);

            if (user is null || !BCrypt.Net.BCrypt.Verify(model.Password, user.HashedPassword))
            {
                return NotFound();
            }

            var jwtToken = _jwtUtils.GenerateJwtToken(user);
            var refreshToken = _jwtUtils.GenerateRefreshToken(GetIp());
            user.RefreshTokens.Add(refreshToken);

            user.RefreshTokens.RemoveAll(token => !token.IsActive);

            _context.Update(user);
            await _context.SaveChangesAsync(cancellationToken);

            SetTokenCookie(refreshToken.Token);

            return Ok(new LoginResult()
            {
                Jwt = jwtToken
            });
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(RefreshTokenLoginError))]
        public async Task<IActionResult> LoginByRefreshTokenAsync([FromBody] LoginByTokenModel model, CancellationToken cancellationToken)
        {
            var token = Request.Cookies[RefreshTokenKey];

            var user = await _context.Users.Include(u => u.RefreshTokens).FirstOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token), cancellationToken);

            if (user is null)
            {
                return BadRequest(RefreshTokenLoginError.InvalidToken);
            }

            var refreshToken = user.RefreshTokens.First(u => u.Token == token);

            if (refreshToken.IsRevoked)
            {
                RevokeDescendantRefreshTokens(refreshToken, user, GetIp(), $"Attempted reuse of revoked ancestor token: {token}");
                _context.Update(user);
                await _context.SaveChangesAsync(cancellationToken);
            }

            if (!refreshToken.IsActive)
            {
                return BadRequest(RefreshTokenLoginError.InvalidToken);
            }

            var newRefreshToken = RotateRefreshToken(refreshToken, GetIp());

            user.RefreshTokens.Add(newRefreshToken);
            user.RefreshTokens.RemoveAll(t => !t.IsActive);

            _context.Update(user);
            await _context.SaveChangesAsync(cancellationToken);

            var jwt = _jwtUtils.GenerateJwtToken(user);

            SetTokenCookie(newRefreshToken.Token);

            return Ok(new LoginResult
            {
                Jwt = jwt
            });
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model, CancellationToken cancellationToken)
        {
            var tokens = new List<RefreshToken>();
            var refreshToken = _jwtUtils.GenerateRefreshToken(GetIp());

            tokens.Add(refreshToken);

            var user = new DBUser()
            {
                UserName = model.UserName,
                RefreshTokens = tokens,
                HashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password),
                CreatedAt = DateTime.UtcNow
            };

            var jwt = _jwtUtils.GenerateJwtToken(user);

            await _context.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            SetTokenCookie(refreshToken.Token);

            return Ok(new RegisterResult()
            {
                Jwt = jwt
            });
        }

        private string GetIp()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                return Request.Headers["X-Forwarded-For"]!;
            }

            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

            if (ipAddress is null)
            {
                _logger.LogDebug("HttpContext.Connection.RemoteIpAddress is null");
            }

            return ipAddress ?? string.Empty;
        }

        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(_appSettings.RefreshTokenTtl),
                Secure = true,
                SameSite = SameSiteMode.None
            };
            Response.Cookies.Append(RefreshTokenKey, token, cookieOptions);
        }

        private void RevokeDescendantRefreshTokens(RefreshToken refreshToken, DBUser user, string ipAddress, string reason)
        {
            if (!string.IsNullOrEmpty(refreshToken.ReplacedByToken))
            {
                var childToken = user.RefreshTokens.Single(x => x.Token == refreshToken.ReplacedByToken);
                if (childToken.IsActive)
                {
                    RevokeRefreshToken(childToken, ipAddress, reason);
                }
                else
                {
                    RevokeDescendantRefreshTokens(childToken, user, ipAddress, reason);
                }
            }
        }

        private static void RevokeRefreshToken(RefreshToken token, string ipAddress, string? reason = null, string? replacedByToken = null)
        {
            token.Revoked = DateTime.UtcNow;
            token.RevokedByIp = ipAddress;
            token.ReasonRevoked = reason;
            token.ReplacedByToken = replacedByToken;
        }

        private RefreshToken RotateRefreshToken(RefreshToken refreshToken, string ipAddress)
        {
            var newRefreshToken = _jwtUtils.GenerateRefreshToken(ipAddress);
            RevokeRefreshToken(refreshToken, ipAddress, "Replaced by new token", newRefreshToken.Token);
            return newRefreshToken;
        }
    }
}
