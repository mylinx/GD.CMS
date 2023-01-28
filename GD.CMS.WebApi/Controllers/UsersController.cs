using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GD.CMS.Common;
using GD.CMS.Service;
using GD.CMS.Entity;

namespace GD.CMS.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IJwtService _jwtService;
        IRolesService _rolesService;
        IUsersService _usersService;
        IPermissionService _permissionService;

        public UsersController(IJwtService jwtService,
            IRolesService rolesService,
             IUsersService usersService,
             IPermissionService permissionService) 
        {
            _jwtService = jwtService;
            _rolesService = rolesService;
            _usersService = usersService;
            _permissionService = permissionService;
        }

        public IActionResult Test() 
        {
             var u=  _usersService.Select.ToList();
            var r = _rolesService.Select.ToList();
            var p = _permissionService.Select.ToList();

            return null;
        }




        /// <summary>
        /// 返回用户列表
        /// </summary>
        /// <param name="searchParams"></param>
        /// <returns></returns>
        public async Task<LayResult> GetList(UsersListParams searchParams) 
        {
            return await _usersService.GetLayAsync(searchParams);
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<AjaxResult> Add(UsersEntity users) 
        {
            return await _usersService.InserOrUpdate(users);
        }



       [HttpDelete]
        public async Task<AjaxResult> Delete(int id)
        {
            AjaxResult model = new AjaxResult() 
            {
               ErrorCode=0,
                Success=false
            };
            if (id == 0)
            {
                model.Msg = "参数错误！";
                return model;
            }
            int count = await _usersService.DeleteAsync(id);
            if (count >0)
            {
                model.Success = true;
                model.Msg = "删除成功！";
            }
            else
            {
                model.Msg = "数据不存在或者删除失败！";
                return model;
            }

            return model;
        }
    }

}
