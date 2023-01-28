using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FreeSql;
using GD.CMS.Common;
using GD.CMS.Entity;
namespace GD.CMS.Service
{
    public interface IUsersService : IBaseService<UsersEntity>
    {

        #region 后台管理

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
         Task<LayResult> GetLayAsync(UsersListParams searchParams);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="usersEntity"></param>
        /// <returns></returns>
        Task<AjaxResult> InserOrUpdate(UsersEntity usersEntity);

        /// <summary>
        ///  删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AjaxResult> Del(int id);


        Task<AjaxResult> GetById(int id);
        #endregion
    }
}
