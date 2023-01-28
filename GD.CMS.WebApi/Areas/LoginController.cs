using GD.CMS.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FreeSql;
using GD.CMS.Service;
using GD.CMS.Common;

namespace GD.CMS.WebApi.Areas
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class LoginController : ControllerBase
    {
        readonly ILoginService _loginService;
        public LoginController(ILoginService loginService) 
        {
            _loginService = loginService;
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        public async Task<AjaxResult> Login(LoginDto loginDto) 
        {
            return await _loginService.LoginByAccount(loginDto);
        }
    }
}
