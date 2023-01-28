using System;
using System.Collections.Generic;
using System.Text;
using FreeSql;
using GD.CMS.Entity;

namespace GD.CMS.Service
{
    public class OrigansService : BaseService<OrganizationEntity>, IOrigansService
    {
        private IFreeSql _freesql;
        public OrigansService(IFreeSql fsql) : base(fsql, null, null)
        {
            _freesql = fsql;
        }





    }
}
