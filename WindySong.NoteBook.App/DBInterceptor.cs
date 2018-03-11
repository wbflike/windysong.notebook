using Chloe.Infrastructure.Interception;
using System;
using System.Collections.Generic;
using System.Text;

namespace WindySong.NoteBook.App
{
    /// <summary>
    /// SQL拦截器运行方法
    /// </summary>
    public class DBInterceptor
    {
        public DBInterceptor()
        {
            IDbCommandInterceptor interceptor = new DbCommandInterceptor();
            DbInterception.Add(interceptor);
        }
    }
}
