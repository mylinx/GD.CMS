using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FreeSql;
using GD.CMS.Common;
using System.Linq;
using GD.CMS.Entity;

namespace GD.CMS.Service
{
    public class RolesService:BaseService<RoleEntity>, IRolesService
    {
        private IFreeSql _freesql;
        public RolesService(IFreeSql fsql) : base(fsql, null, null)
        {
            _freesql = fsql;
        }

         
    }
}
