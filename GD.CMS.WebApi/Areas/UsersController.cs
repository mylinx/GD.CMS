using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GD.CMS.Service;
using GD.CMS.Entity;
using GD.CMS.Common;
using System.Threading.Tasks;

namespace GD.CMS.WebApi.Areas
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    public class UsersController : BaseController
    {
        readonly IUsersService _usersService;
        public UsersController(IUsersService baseService) 
        {
            _usersService = baseService;
        }
        /// <summary>
        ///  数据
        /// </summary>
        /// <returns></returns>
        public async Task<LayResult> GetList() 
        {
            var list = await _usersService.GetLayAsync(new UsersListParams() { Page = 1, Limit = 10 });

            return list;
        }



        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult> Get(int id)
        {
            return await _usersService.GetById(id);
        }
    }
}
