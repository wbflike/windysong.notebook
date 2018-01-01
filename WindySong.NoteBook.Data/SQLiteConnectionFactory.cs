using Chloe.Infrastructure;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace WindySong.NoteBook.Data
{
    public class SQLiteConnectionFactory : IDbConnectionFactory
    {
        string _connString = null;
        public SQLiteConnectionFactory(string connString)
        {
            this._connString = connString;
        }
        public IDbConnection CreateConnection()
        {
            SqliteConnection conn = new SqliteConnection(this._connString);
            return conn;
        }
    }
}
