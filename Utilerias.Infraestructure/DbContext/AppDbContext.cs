using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using Polly;
using Polly.Retry;
using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using Utilerias.Infraestructure.DbContext.Interface;

namespace Utilerias.Infraestructure.DbContext
{
    [ExcludeFromCodeCoverage]
    public class AppDbContext : IAppDbContext
    {
        #region ATTRIBUTES
        private readonly IConfiguration _configuration;
        private IDbConnection _dbConnection;
        #endregion

        #region "CONSTRUCTOR"
        public AppDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region "METODOS"
        public IDbConnection GetConnection()
        {
            if (_dbConnection == null)
            {
                var connectionString = _configuration.GetConnectionString("OracleConnection");
                _dbConnection = new OracleConnection(connectionString);
            }

            var _retryPolicy = RetryPolicy.Handle<Exception>()
            .WaitAndRetry(7, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
            {
                Console.WriteLine("Problema con la Conexion de Oracle " + ex.StackTrace);
            });

            var _connection = _retryPolicy.ExecuteAndCapture(() =>
            {
                if (_dbConnection.State != ConnectionState.Open)
                    _dbConnection.Open();

                return _dbConnection;
            });

            return _connection.Result;
        }
        #endregion
    }
}
