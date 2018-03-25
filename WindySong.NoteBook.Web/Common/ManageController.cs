using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WindySong.NoteBook.App;

namespace WindySong.NoteBook.Web.Common
{
    /// <summary>
    /// Admin控制器父类
    /// </summary>
    public class ManageController: BaseController
    {
        public string GetUserPhoto()
        {
            AppService appService = new AppService();
            return appService.GetUserPhoto(int.Parse(User.FindFirstValue(ClaimTypes.Sid)));
        }
    }
}
