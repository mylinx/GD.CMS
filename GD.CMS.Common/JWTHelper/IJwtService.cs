using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GD.CMS.Common
{
    public interface IJwtService
    {
        public JwtTokenModel GetToken(UsersIndentityModel model);

        public AjaxResult DeclearToken(string token);

        public string RefreshToken();


    }
}
