using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FreeSql;
using GD.CMS.Entity;
using GD.CMS.Common;
using System.Linq;
using GD.CMS.Entity.Dto;

namespace GD.CMS.Service
{
    /// <summary>
    /// mylinx: 2021-4-29 15:49:57
    /// </summary>
    public class UsersService : BaseService<UsersEntity>,IUsersService
    {
        private IFreeSql _freesql;
        public UsersService(IFreeSql fsql) : base(fsql, null, null)
        {
            _freesql = fsql;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="searchParams"></param>
        /// <returns></returns>
        public async Task<LayResult> GetLayAsync(UsersListParams searchParams)
        {
            LayResult result = new LayResult();
                long count = 0;


                var list = await Select.Where(x => !x.IsDeleted).Include(x=>x.roleEntity)
                    .WhereIf(!searchParams.Account.IsNullOrEmpty(), x => x.Account.Contains(searchParams.Account.Trim()))
                    .WhereIf(searchParams.StartTime != null, x => x.CreateTime >= searchParams.StartTime)
                    .WhereIf(searchParams.EndTime != null, x => x.CreateTime <= searchParams.EndTime)
                    .WhereIf(searchParams.RoleID != 0, x => x.RoleID == searchParams.RoleID)
                    .WhereIf(!searchParams.Status.IsNullOrEmpty(), x => x.Status == searchParams.Status).OrderByDescending(x => x.CreateTime)
                    .Page(searchParams.Page, searchParams.Limit)
                    .Count(out count).ToListAsync(x => new
                    {
                        ID = x.ID,
                        Rolename = x.roleEntity == null ? "" : x.roleEntity.RoleName,
                        CreateTime = x.CreateTime,
                        IP = x.IP,
                        Account = x.Account,
                        Status = x.Status,
                        Sort = x.Sort,
                    });

                if (count > 0)
                {
                    result.Data = list;
                    result.Count = (int)count;
                    result.Code = 0;
                    result.Msg = "获取成功！";
                }
                else
                {
                    result.Code = -1;
                    result.Msg = "暂无数据！";
                }

                return result;
        }



        /// <summary>
        /// 新增/编辑
        /// </summary>
        /// <param name="usersEntity"></param>
        /// <returns></returns>
        public async Task<AjaxResult> InserOrUpdate(UsersEntity usersEntity)
        {
            AjaxResult result = new AjaxResult() { Success = false };
            if (usersEntity.Account.IsNullOrEmpty())
            {
                result.Msg = "请输入账号";
                return result;
            }

            if (usersEntity.Password.IsNullOrEmpty())
            {
                result.Msg = "请输入密码";
                return result;
            }
            if (usersEntity.ID != 0)
            {
                var entity = Find(usersEntity.ID);
                if (entity != null && !entity.IsDeleted)
                {
                    if (entity.Password != usersEntity.Password)
                        usersEntity.Password = usersEntity.Password.ToMD5String();
                }
                else
                {
                    result.Msg = "数据不存在或者已经删除";
                    return result;
                }
            }
            else
            {
                usersEntity.Password = usersEntity.Password.ToMD5String();
            }


            if ((await InsertOrUpdateAsync(usersEntity)) != null)
            {
                result.Success = true;
                result.Msg = "操作成功！";
            }
            else
            {
                result.Msg = "操作失败！";
            }
            return result;
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AjaxResult> Del(int id)
        {
            AjaxResult result = new AjaxResult() { Success=false };
            if (id == 0 )
            {
                result.Msg = "参数不正确";
                return result;
            }

            int count = await DeleteAsync(id);

            if (count > 0)
            {
                result.Msg = "删除成功";
                result.Success = true;
            }
            else
            {
                result.Msg = "删除失败";
            }
            return result;
        }

        public async Task<AjaxResult> GetById(int id)
        {
            AjaxResult result = new AjaxResult() { Success = false };
            if (id == 0)
            {
                result.Msg = "参数不正确";
                return result;
            }

            var entity = await FindAsync(id);

            if (entity!=null)
            {
                var entityDto = _freesql.Select<RoleEntity>(new { id = entity.RoleID }).First();
                UserEntityDto dto = new UserEntityDto()
                {
                    Id = entity.ID,
                    Name = entity.Account,
                    RoleName = entityDto==null?"": entityDto.RoleName
                };
                result.Msg = "获取成功";
                result.Data = dto;
                result.Success = true;
            }
            else
            {
                result.Msg = "获取失败";
            }
            return result;

        }
    }
}
