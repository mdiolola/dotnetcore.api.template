using dotnetcore.api.template.Data;
using dotnetcore.api.entity.Users;
using Xunit;
using dotnetcore.api.model.Common;
using dotnetcore.api.template.Services;
using dotnetcore.api.template.Services.Interface;
using dotnetcore.api.template.Settings;
using Microsoft.Extensions.Options;

namespace dotnetcore.api.test.Services
{
    [Collection("Sequential")]
    public class AuthenticateServiceTest : BaseContextSetup
    {
        private readonly string passphrase = "0987654321";

        private UserEntity user;

        protected void SetupInitialData(ApiContext context)
        {

            user = new UserEntity
            {
                Username = "something",
                Password = "testing"
            };
            user.Password.Encrypt(passphrase);
        }

        private IAuthenticateService SetupService(ApiContext context)
        {

            var authenticate = Options.Create(new Authenticate() { Issuer = "https://localhost:44320/", Audience = "", ExpireMinutes = 300, SecurityKey = "this is unique Key" });
            var security = Options.Create(new Security() { PassPhrase = "123456" });
            var target = new AuthenticateService(context, authenticate, security);
            return target;
        }

        [Trait("Category", "Unit")]
        [Fact]
        public void ShouldDecodeThePassword()
        {
            using (var context = new ApiContext(_options))
            {
                SetupInitialData(context);
                var service = SetupService(context);

                var response = service.Decode(user.Password);
                //response.

            }
        }
    }
}
