using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public static class Globals
    {
        /// <summary>
        /// 配置文件读取接口
        /// </summary>
        public static IConfiguration Configuration { get; set; }

        private static string _connString;
        private static string _dbType;

        /// <summary>
        /// 返回数据库连接字符串
        /// </summary>
        public static string ConnString
        {
            get
            {
                _connString = Configuration["db:ConnString"];
                return _connString;
            }
        }
        /// <summary>
        /// 返回数据库类型
        /// </summary>
        public static string DbType
        {
            get
            {
                _dbType = Configuration["db:DbType"];
                return _dbType;
            }
        }
    
    }
}
