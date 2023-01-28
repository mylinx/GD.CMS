using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GD.CMS.Entity;
using GD.CMS.Common;
using Microsoft.AspNetCore.Http;

namespace GD.CMS.Service
{

    /// <summary>
    /// 文件上传接口
    /// </summary>
    public interface IUploadService
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public Task<AjaxResult> UploadFile(IFormFile file);


        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public Task<AjaxResult> UploadImage(IFormFile file);


        /// <summary>
        /// 上传音频
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public Task<AjaxResult> UploadVideo(IFormFile file);

        /// <summary>
        /// 大文件上传
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public Task<AjaxResult> BigFileUpload(IFormFile file);

        /// <summary>
        /// 合并文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public Task<AjaxResult> Meters(IFormFile file);

    }
}
