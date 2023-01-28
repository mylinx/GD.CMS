using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GD.CMS.Common;

namespace GD.CMS.Service
{
    /// <summary>
    /// 公共数据服务
    /// </summary>
    public interface ICommonService
    {

        /// <summary>
        /// 文件上传(小于5M的)
        /// </summary>
        /// <returns></returns>
        Task<AjaxResult> UploadFile();



        /// <summary>
        /// 分片上传(大于5M的)
        /// </summary>
        /// <returns></returns>
        AjaxResult UploadBigFile();



        /// <summary>
        /// 合并文件
        /// </summary>
        /// <returns></returns>
        AjaxResult MeterFile();
    }
}
