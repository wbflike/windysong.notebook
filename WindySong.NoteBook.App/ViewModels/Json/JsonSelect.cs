using System;
using System.Collections.Generic;
using System.Text;

namespace WindySong.NoteBook.App.ViewModels.Json
{
    /// <summary>
    /// Tab Select 
    /// </summary>
    public class JsonSelect
    {
        public List<JsonSelectValue> options { get; set; }
    }

    public class JsonSelectValue
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
