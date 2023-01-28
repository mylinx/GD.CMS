using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FreeSql;
using GD.CMS.Common;
using GD.CMS.Entity;

namespace GD.CMS.Service
{
    public class LogService : BaseService<LogsEntity>, ILogService
    {
        private IFreeSql _freesql;
        public LogService(IFreeSql fsql) : base(fsql, null, null)
        {
            _freesql = fsql;
        }

        public async Task<AjaxResult> Del(int id)
        {
            AjaxResult result = new AjaxResult() { Success = false };
            if (id == 0)
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

        public async Task<LayResult> GetLayAsync(LogListCrud logListCrud)
        {
            LayResult result = new LayResult();
            long count = 0;


            var list = await Select.WhereIf(!logListCrud.IsNullOrEmpty(),x=>x.Logcontent.Contains(logListCrud.Content))
                .Page(logListCrud.Page, logListCrud.Limit)
                .Count(out count).ToListAsync(x => new
                {
                    ID = x.ID,
                    x.Logcontent,
                    x.Logtype,
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
         
    }
}
