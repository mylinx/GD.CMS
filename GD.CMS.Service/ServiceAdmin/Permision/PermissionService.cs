using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FreeSql;
using GD.CMS.Common;
using GD.CMS.Entity;
using System.Linq;

namespace GD.CMS.Service
{
    public class PermissionService:BaseService<PermissionEntity>, IPermissionService
    {
        private IFreeSql _freesql; 
        public PermissionService(IFreeSql fsql) : base(fsql, null, null)
        {
            _freesql = fsql;
        }

         
    }
}
