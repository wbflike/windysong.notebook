using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WindySong.NoteBook.App.ViewModels.Admin
{
    public class UpdatePasswordModel
    {
        /// <summary>
        /// 旧密码
        /// </summary>
        [Required(ErrorMessage = "密码为必填项!")]
        [RegularExpression(@"^.{6,16}$", ErrorMessage = "密码必须为6到16位字符!")]
        public string oldPassword { get; set; }

        [Required(ErrorMessage = "新密码为必填项!")]
        [RegularExpression(@"^.{6,16}$", ErrorMessage = "密码必须为6到16位字符!")]
        public string newPassword { get; set; }

        [Compare("newPassword", ErrorMessage = "密码和确认密码不匹配。")]
        public string confirmNewPassword { get; set; }
    }
}
