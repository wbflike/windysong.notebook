using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WindySong.NoteBook.App.ViewModels.Admin
{
    public class ApiModel
    {
        /// <summary>
        /// ID
        /// </summary>
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "ID必须为数字!")]
        public int? Id { get; set; }

        /// <summary>
        /// TabId
        /// </summary>
        [Required(ErrorMessage = "分类必填!")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "分类为数字!")]
        public int ListId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "名称必填!")]
        [RegularExpression(@"^.{1,200}$", ErrorMessage = "名称必须为1到200位字符!")]
        public string Name { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        [StringLength(100, ErrorMessage = "参数的长度不能大于8000个字符")]
        public string Parameter { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(8000, ErrorMessage = "描述的长度不能大于8000个字符")]
        public string Description { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "排序值必须为数字!")]
        public int? Rank { get; set; }
    }
}
