using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GD.CMS.Common;
using GD.CMS.Entity;
using GD.CMS.Service;

namespace GD.CMS.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        IJwtService _jwtService;
        ILoginService _loginService;
        public LoginController(IJwtService jwtService, ILoginService loginService) 
        {
            _jwtService = jwtService;
            _loginService = loginService;   
        }

     

        [HttpPost]
        public async Task<AjaxResult> Login(LoginDto model)
        {
          return await  _loginService.LoginByAccount(model);
        }
    }

}
