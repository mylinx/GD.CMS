using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GD.CMS.WebApi.Areas
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizationFilter]
    public class BaseController : ControllerBase
    {
         
    }
}
