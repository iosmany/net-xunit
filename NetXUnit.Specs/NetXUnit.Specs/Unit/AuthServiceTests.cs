
using FluentAssertions;
using NetXUnit.Webapi.Services;
using NetXUnit.Webapi.Services.Implementations;

namespace NetXUnit.Specs.Unit
{
    public class AuthServiceTests
    {
        readonly private IAuthService _authService;

        public AuthServiceTests()
        {
            _authService = new AuthService(new JwtTokenManager());
        }

        [Fact]
        public async Task Login_ValidEmailAndPassword_ReturnsToken()
        {
            // Arrange
            const string email = "test@test.com";
            const string password = "fluent1Assertion";
            // Act
            var result = await _authService.LoginAsync(email, password);
            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Login_InvalidEmailAndPassword_ReturnsError()
        {
            // Arrange
            const string email = "";
            const string password = "";
            // Act
            var result = await _authService.LoginAsync(email, password);
            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.Should().NotBeEmpty();
            result.Errors.Should().Contain(e => e.Description == "Email and password are required");
        }
    }
}
