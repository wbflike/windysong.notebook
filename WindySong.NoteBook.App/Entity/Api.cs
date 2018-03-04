using Chloe.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace WindySong.NoteBook.App.Entity
{
    [TableAttribute("api")]
    public class Api
    {
        [Column(IsPrimaryKey = true)]//表示id为主键
        [AutoIncrementAttribute]//表示ID为 自动增长    
        public int id { get; set; }
        public int uListId { get; set; }
        public string name { get; set; }
        public string parameter { get; set; }
        public string description { get; set; }
        public int rank { get; set; }
        public string lastTime { get; set; }
    }
}
