using ErrorOr;

namespace NetXUnit.Webapi.Services.Implementations
{
    sealed class AuthService : IAuthService
    {
        readonly JwtTokenManager _jwtTokenManager;
        public AuthService(JwtTokenManager jwtTokenManager)
        {
            _jwtTokenManager = jwtTokenManager;
        }

        public Task<ErrorOr<string>> LoginAsync(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return Task.FromResult<ErrorOr<string>>(Error.Validation(description: "Email and password are required").ToMultiple());

            var hasDigit = password.Any(char.IsDigit);
            var hasUpper = password.Any(char.IsUpper);
            var hasNumber = password.Any(char.IsNumber);

            if (!hasDigit || !hasUpper || !hasNumber)
                return Task.FromResult<ErrorOr<string>>(Error.Validation(description: "Password must contain at least one digit, one uppercase letter and one number").ToMultiple());

             // JWT token generation
            var tokenString = _jwtTokenManager.Generate(email);
            return Task.FromResult(tokenString);
        }

        public Task<ErrorOr<bool>> LogoutAsync(string token)
        {
            throw new NotImplementedException();
        }

        public Task<ErrorOr<string>> RefreshTokenAsync(string token)
        {
            throw new NotImplementedException();
        }
    }
}
