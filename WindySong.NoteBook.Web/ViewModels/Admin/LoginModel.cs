using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WindySong.NoteBook.Web.ViewModels.Admin
{
    public class LoginModel
    {
        [Required(ErrorMessage = "用户名为必填项!<br />")]
        [RegularExpression(@"^.{3,10}$", ErrorMessage = "用户名必须为3到10位字符!<br />")]
        public string name { get; set; }
        [Required(ErrorMessage = "密码为必填项!<br />")]
        [RegularExpression(@"^.{6,16}$", ErrorMessage = "密码必须为6到16位字符!<br />")]
        public string password { get; set; }
    }
}
