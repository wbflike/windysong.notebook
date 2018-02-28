using System;
using System.Collections.Generic;
using System.Text;

namespace WindySong.NoteBook.App.ViewModels.Json
{
    public class JsonCol
    {
        public int id { get; set; }
        public int tabId { get; set; }
        public string tabName { get; set; }
        public string name { get; set; }       
        public int rank { get; set; }
        public string lastTime { get; set; }
    }
}
