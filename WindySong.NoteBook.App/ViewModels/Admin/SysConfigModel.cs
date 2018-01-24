using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WindySong.NoteBook.App.ViewModels.Admin
{
    /// <summary>
    /// 站点信息
    /// </summary>
    public class SysConfigModel
    {
        /// <summary>
        /// 站点名称
        /// </summary>
        [Required(ErrorMessage = "站点名称为必填项!")]
        [RegularExpression(@"^.{3,30}$", ErrorMessage = "站点名称必须为3到30位字符!")]       
        public string siteName { get; set; }

        /// <summary>
        /// 站点关键字
        /// </summary>
        [Required(ErrorMessage = "站点关键字为必填项!")]
        [RegularExpression(@"^.{3,50}$", ErrorMessage = "站点关键字必须为3到50位字符!")]       
        public string siteKeyWords { get; set; }

        /// <summary>
        /// 站点描述
        /// </summary>
        [Required(ErrorMessage = "站点描述字为必填项!")]
        [RegularExpression(@"^.{3,50}$", ErrorMessage = "站点描述必须为3到50位字符!")]       
        public string siteDescription { get; set; }
    }
}
