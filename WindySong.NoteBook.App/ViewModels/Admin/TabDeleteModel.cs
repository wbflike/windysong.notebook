using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WindySong.NoteBook.App.ViewModels.Admin
{
    public class TabDeleteModel
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required(ErrorMessage = "ID必填!")]
        public string deleteid { get; set; }
    }
}
