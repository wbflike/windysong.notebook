using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WindySong.NoteBook.App.ViewModels.Admin
{
    /// <summary>
    /// Tab选项卡分页请求
    /// </summary>
    public class TabPostModel
    {
        /// <summary>
        /// 每页显示多少条数据
        /// </summary>
        [Required(ErrorMessage = "页面大小必填!")]
        public int limit { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        [Required(ErrorMessage = "页码必填项!")]
        public int offset { get; set; }

        /// <summary>
        /// 搜索关键字
        /// </summary>
        [RegularExpression(@"^.{3,50}$", ErrorMessage = "站点描述必须为3到50位字符!")]
        public string searchKey { get; set; }
    }
}
