using System;
using System.Collections.Generic;
using System.Text;

namespace WindySong.NoteBook.App.ViewModels.Json
{
    /// <summary>
    /// Tab Select 
    /// </summary>
    public class JsonTabSelect
    {
        public List<JsonTabValue> options { get; set; }
    }

    public class JsonTabValue
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
