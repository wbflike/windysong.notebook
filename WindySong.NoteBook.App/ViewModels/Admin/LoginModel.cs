using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WindySong.NoteBook.App.ViewModels.Admin
{
    public class LoginModel
    {
        public int id { get; set; }

        [Required(ErrorMessage = "用户名为必填项!<br />")]
        [RegularExpression(@"^.{3,10}$", ErrorMessage = "用户名必须为3到10位字符!<br />")]
        public string name { get; set; }


        [Required(ErrorMessage = "密码为必填项!<br />")]
        [RegularExpression(@"^.{6,16}$", ErrorMessage = "密码必须为6到16位字符!<br />")]
        public string password { get; set; }

        public string photo { get; set; }
    }
}
