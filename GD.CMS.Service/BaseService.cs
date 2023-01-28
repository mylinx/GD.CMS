using FreeSql;
using GD.CMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GD.CMS.Service
{
    public class BaseService<T> : BaseRepository<T, int>, IBaseService<T> where T : class
    {
        public BaseService(IFreeSql fsql, Expression<Func<T, bool>> filter, Func<string, string> asTable = null) : base(fsql, filter, asTable)
        {
        }
    }
}
