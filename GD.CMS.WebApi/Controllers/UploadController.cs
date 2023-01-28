using Aspose.Cells;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using GD.CMS.Common;
using System;
using Microsoft.AspNetCore.Authorization;

namespace GD.CMS.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UploadController : ControllerBase
    {

        [HttpPost]
        [AllowAnonymous]
        public string UploadFile( string filename, [FromForm(Name = "file")] IFormFile formfile)
        {
            IFormFile formFile = Request.Form.Files[0];
            string virpath = "MyStaticUpload\\" + DateTime.Now.ToString("yyyyMMdd") +"\\" ;
            string floder = GlobalSwitch.WebRootPath + "\\" + virpath;
            var filePath = GlobalSwitch.WebRootPath +"\\"+ virpath+ formFile.FileName;

            if(!Directory.Exists(floder))
            {
                Directory.CreateDirectory(floder);
            }

            if (formFile.Length > 0)
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    formFile.CopyTo(stream);
                }
            }
            var data = new
            {
                filename,
                 virpath
            };

            return data.ToJson();
        }
    }
}
