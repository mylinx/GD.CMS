using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GD.CMS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController1 : ControllerBase
    {
        [HttpPost]
        public string UploadFile([FromBody] string file)
        {

            return file;
        }
    }
}
