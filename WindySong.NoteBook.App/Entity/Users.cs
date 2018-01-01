using Chloe.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace WindySong.NoteBook.App.Entity
{
    [TableAttribute("Users")]
    /// <summary>
    /// 用户表
    /// </summary>
    public class Users
    {
        [Column(IsPrimaryKey = true)]//表示id为主键
        [AutoIncrementAttribute]//表示ID为 自动增长      
        /// <summary>
		/// ID
		/// </summary>
		public int id { get; set; }

        [Column("userName")]//映射的列名
        /// <summary>
        /// 用户名
        /// </summary>
        public string userName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string passWord { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string photo { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public string lastTime { get; set; }

    }
}
