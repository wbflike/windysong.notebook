using Chloe.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace WindySong.NoteBook.App.Entity
{
    [TableAttribute("sysConfig")]
    /// <summary>
    /// 系统配置信息
    /// </summary>
    public class SysConfig
    {
        [Column(IsPrimaryKey = true)]//表示id为主键
        [AutoIncrementAttribute]//表示ID为 自动增长   
        /// <summary>
		/// ID
		/// </summary>
		public int id { get; set; }
        /// <summary>
        /// 站点名称
        /// </summary>
        public string siteName { get; set; }
        /// <summary>
        /// 站点关键字
        /// </summary>
        public string siteKeyWords { get; set; }
        /// <summary>
        /// 站点描述
        /// </summary>
        public string siteDescription { get; set; }
        /// <summary>
        /// cdn地址
        /// </summary>
        public string cdn { get; set; }

    }
}
