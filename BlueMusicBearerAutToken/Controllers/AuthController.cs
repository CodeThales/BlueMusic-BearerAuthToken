using BlueMusicBearerAutToken.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;


namespace BlueMusicBearerAutToken.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ApiBaseController
    {
        IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }


        /// <summary>
        /// Creates a new user for the API
        /// </summary>
        /// <param name="identityUser"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public IActionResult Register([FromBody] IdentityUser identityUser)
        {
            IdentityResult result = _service.Create(identityUser).Result;
            identityUser.PasswordHash = null;
            return result.Succeeded ?
                ApiOk(identityUser) :
                ApiBadRequest("Erro ao tentar criar usuário!");
        }


        /// <summary>
        /// Get an access token for an authorized user.
        /// </summary>
        /// <param name="identityUser"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Token")]
        [AllowAnonymous]
        public IActionResult Token([FromBody] IdentityUser identityUser)
        {
            try
            {
                return ApiOk(_service.GenerateToken(identityUser));
            }
            catch (Exception e)
            {
                return ApiBadRequest(e, e.Message);
            }
        }

    }
}
