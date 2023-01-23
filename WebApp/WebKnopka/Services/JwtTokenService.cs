using Google.Apis.Auth;
using WebKnopka.Models;

namespace WebKnopka.Services
{
    public interface IJwtTokenService
    {
        Task<GoogleJsonWebSignature.Payload> VerifyGoogelToken(ExternalLoginViewModel model);
    }
    public class JwtTokenService : IJwtTokenService
    {
        public async Task<GoogleJsonWebSignature.Payload> VerifyGoogelToken(ExternalLoginViewModel model)
        {
            string clientId = "1023020461333-q2vicrpm2rnjreik8qcotc3s8e6af59p.apps.googleusercontent.com";
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { clientId }
            };
            var payload = await GoogleJsonWebSignature.ValidateAsync(model.Token, settings);
            return payload;
        }
    }
}
