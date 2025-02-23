using ErrorOr;

namespace NetXUnit.Webapi.Services
{
    public interface IAuthService
    {
        /// <summary>
        /// Perform an account login
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>Valid token for authorization</returns>
        Task<ErrorOr<string>> LoginAsync(string email, string password);
        /// <summary>
        /// Do account logout
        /// </summary>
        /// <param name="token"></param>
        /// <returns>True for completed succesfully, false for failed</returns>
        Task<ErrorOr<bool>> LogoutAsync(string token);
        /// <summary>
        /// Refresh a token based on an expired token
        /// </summary>
        /// <param name="token"></param>
        /// <returns>New valid token</returns>
        Task<ErrorOr<string>> RefreshTokenAsync(string token);

    }
}
