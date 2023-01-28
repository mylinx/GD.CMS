using GD.CMS.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.CMS.Service.ServiceDto.UploadDto
{
    public class UploadService : IUploadService
    {
        public Task<AjaxResult> BigFileUpload(IFormFile file)
        {
            throw new NotImplementedException();
        }

        public Task<AjaxResult> Meters(IFormFile file)
        {
            throw new NotImplementedException();
        }

        public Task<AjaxResult> UploadFile(IFormFile file)
        {
            throw new NotImplementedException();
        }

        public Task<AjaxResult> UploadImage(IFormFile file)
        {
            throw new NotImplementedException();
        }

        public Task<AjaxResult> UploadVideo(IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}
