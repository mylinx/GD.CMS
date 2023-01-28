using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FreeSql;
using GD.CMS.Common;
using GD.CMS.Entity;

namespace GD.CMS.Service
{
    public interface ILogService : IBaseService<LogsEntity> 
    {

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        Task<LayResult> GetLayAsync(LogListCrud logListCrud);


        Task<AjaxResult> Del(int id);
    }
}
