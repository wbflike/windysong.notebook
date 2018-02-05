using Chloe.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace WindySong.NoteBook.App.Entity
{
    [TableAttribute("cTab")]
    public class CTab
    {
        [Column(IsPrimaryKey = true)]//表示id为主键
        [AutoIncrementAttribute]//表示ID为 自动增长    
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int rank { get; set; }
        public string lastTime { get; set; }
    }
}
