using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WindySong.NoteBook.App.ViewModels.Admin
{
    /// <summary>
    /// 添加cTab
    /// </summary>
    public class TabAddModel
    {
        /// <summary>
        /// 每页显示多少条数据
        /// </summary>
        [Required(ErrorMessage = "名称必填!")]
        public string name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [RegularExpression(@"^.{2,20}$", ErrorMessage = "描述必须为2到20位字符!")]
        public string description { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "排序值必须为数字!")]
        public int rank { get; set; }
    }
}
