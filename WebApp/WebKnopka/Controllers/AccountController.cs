using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebKnopka.Data.Entities;
using WebKnopka.Models;
using WebKnopka.Services;

namespace WebKnopka.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly UserManager<AppUserEntity> _userManager;
        public AccountController(IJwtTokenService jwtTokenService, 
            UserManager<AppUserEntity> userManager)
        {
            _jwtTokenService = jwtTokenService;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> GoogleExternalLoginAsync(ExternalLoginViewModel model)
        {
            var payload = await _jwtTokenService.VerifyGoogelToken(model);
            if (payload == null)
            {
                return BadRequest(new { error = "Invalid verifi user" });
            }
            var info = new UserLoginInfo(model.Provider, payload.Subject, model.Provider);
            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(payload.Email);
                if(user==null)
                {
                    user = new AppUserEntity
                    {
                        Email = payload.Email,
                        UserName = payload.Email,
                        FirstName = payload.GivenName,
                        SecondName = payload.FamilyName
                    };
                    var resultCreate = await _userManager.CreateAsync(user);
                    if (!resultCreate.Succeeded)
                    {
                        return BadRequest(new { error = "Помилка створення користувача" });
                    }
                }
                var resultLOgin = await _userManager.AddLoginAsync(user, info);
                if(!resultLOgin.Succeeded)
                {
                    return BadRequest(new { error = "Створення входу через гугл" });
                }
            }
            return Ok(new { success = "Лови токен" });
        }
    }
}
