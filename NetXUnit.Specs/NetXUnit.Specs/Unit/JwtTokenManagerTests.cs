using FluentAssertions;
using NetXUnit.Webapi.Services.Implementations;

namespace NetXUnit.Specs.Unit
{
    public class JwtTokenManagerTests
    {
        readonly private JwtTokenManager _jwtTokenManager;
        public JwtTokenManagerTests() 
        {
            _jwtTokenManager = new JwtTokenManager();
        }

        [Fact]
        public void Generate_ValidEmail_ReturnsToken()
        {
            // act
            var response =_jwtTokenManager.Generate("test@test.com");
            // assert
            response.IsError.Should().BeFalse();
            response.Value.Should().NotBeNullOrEmpty();
        }
    }
}
