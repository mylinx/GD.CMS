using FreeSql;
using GD.CMS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.CMS.Service
{
    public interface IBaseService<T>: IBaseRepository<T, int>, IBaseRepository<T> where T : class
    {

    }
}
