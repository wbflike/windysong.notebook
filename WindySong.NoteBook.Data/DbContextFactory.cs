using Chloe;
using Chloe.Infrastructure.Interception;
using Chloe.MySql;
using Chloe.SQLite;
using Chloe.SqlServer;
using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace WindySong.NoteBook.Data
{
    /// <summary>
    /// 数据库连接对象创建工厂类
    /// </summary>
    public class DbContextFactory
    {
        /// <summary>
        /// 构造函数获取数据库连接字符串和数据库类型
        /// </summary>
        static DbContextFactory()
        {
            ConnectionString = Globals.ConnString;

            string dbType = Globals.DbType;
            if (string.IsNullOrEmpty(dbType) == false)
            {
                DbType = dbType.ToLower();
            }

        }
        /// <summary>
        /// 连接字符串
        /// </summary>
        public static string ConnectionString { get; private set; }
        /// <summary>
        /// 数据库类型
        /// </summary>
        public static string DbType { get; private set; }

        /// <summary>
        /// 获取数据库连接对象
        /// </summary>
        /// <returns>数据库连接对象</returns>
        public static IDbContext CreateContext()
        {
            IDbContext dbContext = CreateContext(ConnectionString);
            return dbContext;
        }

        /// <summary>
        /// 获取数据库连接对象
        /// </summary>
        /// <param name="connString">数据库连接字符串</param>
        /// <returns>数据库连接对象</returns>
        public static IDbContext CreateContext(string connString)
        {
            IDbContext dbContext = null;

            if (DbType == "sqlite")
            {
                dbContext = CreateSQLiteContext(connString);
            }
            else if (DbType == "sqlserver")
            {
                dbContext = CreateSqlServerContext(connString);
            }
            else if (DbType == "mysql")
            {
                dbContext = CreateMySqlContext(connString);
            }
            else
            {
                dbContext = CreateSqlServerContext(connString);
            }
            return dbContext;
        }

        /// <summary>
        /// SQLite数据库对象创建
        /// </summary>
        /// <param name="connString">数据库连接字符串</param>
        /// <returns>数据库连接对象</returns>
        static IDbContext CreateSQLiteContext(string connString)
        {
            //SQLite特殊处理
            SQLiteContext dbContext = new SQLiteContext(new SQLiteConnectionFactory(string.Format(connString, System.IO.Directory.GetCurrentDirectory())));
            return dbContext;
        }
        /// <summary>
        /// MSSQL数据库对象创建
        /// </summary>
        /// <param name="connString">数据库连接字符串</param>
        /// <returns>数据库连接对象</returns>
        static IDbContext CreateSqlServerContext(string connString)
        {
            MsSqlContext dbContext = new MsSqlContext(connString);
            dbContext.PagingMode = PagingMode.OFFSET_FETCH;
            return dbContext;
        }
        /// <summary>
        /// MYSQL数据库对象创建
        /// </summary>
        /// <param name="connString">数据库连接字符串</param>
        /// <returns>数据库连接对象</returns>
        static IDbContext CreateMySqlContext(string connString)
        {
            MySqlContext dbContext = new MySqlContext(new MySqlConnectionFactory(connString));
            return dbContext;
        }

    }
}
