using System;
using System.Collections.Generic;
using System.Text;

namespace WindySong.NoteBook.App.ViewModels.Json
{
    /// <summary>
    /// cTab
    /// </summary>
    public class JsonTab
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int rank { get; set; }
        public string lastTime { get; set; }
    }
}
