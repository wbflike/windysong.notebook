using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WindySong.NoteBook.App.ViewModels.Admin
{
    /// <summary>
    /// 更新cTab
    /// </summary>
    public class TabUpdateModel
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required(ErrorMessage = "ID必填!")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "ID必须为数字!")]
        public int updateid { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "名称必填!")]
        [RegularExpression(@"^.{2,12}$", ErrorMessage = "描述必须为2到12位字符!")]
        public string updateName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [RegularExpression(@"^.{2,20}$", ErrorMessage = "描述必须为2到20位字符!")]
        public string updateDescription { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "排序值必须为数字!")]
        public int? updateRank { get; set; }
    }
}
