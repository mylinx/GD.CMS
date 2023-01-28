using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GD.CMS.Common;

namespace GD.CMS.WebApi
{
    public interface IJwtService
    {
        public JwtTokenModel GetToken(UsersIndentityModel model);

        public string DeclearToken(string token);

        public string RefreshToken();


    }
}
