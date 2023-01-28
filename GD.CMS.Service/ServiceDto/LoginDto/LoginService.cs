using GD.CMS.Common;
using GD.CMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.CMS.Service
{
    public class  LoginService : ILoginService
    {
            IBaseService<UsersEntity> _usersService;
            IJwtService _jwtService;
          public LoginService(IBaseService<UsersEntity> usersService, IJwtService jwtService)
        {
            _usersService = usersService;
            _jwtService = jwtService;
        }


        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="dto">登录实体</param>
        /// <returns></returns>
        public async Task<AjaxResult> LoginByAccount(LoginDto dto)
        {
            AjaxResult result = new AjaxResult() {   Success=false };
            
            if(dto==null)
            { 
                result.Msg = "参数不正确";
                return result;
            }

            if (dto.Account.IsNullOrEmpty() || dto.Password.IsNullOrEmpty())
            {
                result.Msg = "账号和密码不能为空";
                return result;
            }


            //vcode 需要校验
            if (dto.Vcode.IsNullOrEmpty())
            {
                result.Msg = "验证码不能为空";
                return result;
            }

            try
            {
                string pwd = dto.Password.ToMD5String();

                var entity = await _usersService.Select
                    .Include(x => x.roleEntity).Where(x => x.Account == dto.Account && x.Password == pwd && !x.IsDeleted).FirstAsync();

                if (entity != null)
                {
                    //生成token

                    var token = _jwtService.GetToken(new UsersIndentityModel()
                    {
                        ID = entity.ID.ToString(),
                        Account = entity.Account,
                        RoleID = entity.RoleID
                    });

                    result.Data = token;
                    result.Success = true;
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            result.Msg = "数据不存在，或者已经被禁用！";

            return result;
        }

    }
}
