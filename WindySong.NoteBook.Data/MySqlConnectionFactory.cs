using Chloe.Infrastructure;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace WindySong.NoteBook.Data
{
    public class MySqlConnectionFactory : IDbConnectionFactory
    {
        string _connString = null;
        public MySqlConnectionFactory(string connString)
        {
            this._connString = connString;
        }
        public IDbConnection CreateConnection()
        {
            IDbConnection conn = new MySqlConnection(this._connString);
            conn = new Chloe.MySql.ChloeMySqlConnection(conn);
            return conn;
        }
    }
}
