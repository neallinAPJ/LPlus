
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Server.DataProvide
{
    public class MySqlContext: IMySqlContext
    {
        private readonly IOptions<AppSettings> _settings;
        public MySqlContext(IOptions<AppSettings> settings)
        {
            _settings = settings;
        }

        private MySqlConnection _connection;
        /// <summary>
        /// 获取数据库连接桥
        /// </summary>
        public MySqlConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = new MySqlConnection(_settings.Value.MySqlConnectionStrings);
                    _connection.Open();
                }
                if (_connection.State == ConnectionState.Closed)
                {
                    _connection.Open();
                }
                if (_connection.State == ConnectionState.Broken)
                {
                    _connection.Close();
                    _connection.Open();
                }
                return _connection;
            }
        }

        #region IDisposable Implementation

        protected bool disposed;
        protected virtual void Dispose(bool disposing)
        {
            lock (this)
            {
                // Do nothing if the object has already been disposed of.
                if (disposed)
                    return;
                if (disposing)
                {
                    // Release disposable objects used by this instance here.
                    if (_connection != null)
                        _connection.Dispose();
                }

                // Release unmanaged resources here. Don't access reference type fields.

                // Remember that the object has been disposed of.
                disposed = true;
            }
        }
        public virtual void Dispose()
        {
            Dispose(true);
            // Unregister object for finalization.
            GC.SuppressFinalize(this);
        }
        #endregion  
    }
}
