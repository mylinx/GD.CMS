using System;
using System.Collections.Generic;
using System.Text;

namespace GD.CMS.Common
{
  public  class JwtTokenModel
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }
    }
}
