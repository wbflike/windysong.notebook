﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WindySong.NoteBook.App.ViewModels.Admin
{
    public class ColModel
    {
        /// <summary>
        /// ID
        /// </summary>
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "ID必须为数字!")]
        public int? Id { get; set; }

        /// <summary>
        /// TabId
        /// </summary>
        [Required(ErrorMessage = "名称必填!")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "选项卡必须为数字!")]
        public int CTabId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "名称必填!")]
        [RegularExpression(@"^.{2,12}$", ErrorMessage = "描述必须为2到12位字符!")]
        public string Name { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "排序值必须为数字!")]
        public int? Rank { get; set; }
    }
}