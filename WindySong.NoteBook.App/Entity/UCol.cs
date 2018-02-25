using Chloe.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace WindySong.NoteBook.App.Entity
{
    [TableAttribute("uCol")]
    public class UCol
    {
        [Column(IsPrimaryKey = true)]//表示id为主键
        [AutoIncrementAttribute]//表示ID为 自动增长    
        public int id { get; set; }
        public int cTabId { get; set; }
        public string name { get; set; }
        public int rank { get; set; }
        public string lastTime { get; set; }
    }
}
