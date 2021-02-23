using Infrastructure.Persistence.Interfaces;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace Infrastructure.Persistence
{
    /// <summary>
    /// Conxexão de dados da aplicação
    /// </summary>
    public class RedisConnection : IRedisConnection
    {

        #region Constants

        /// <summary>
        /// Nome da conexão do Redis
        /// </summary>
        private const string CONNECTION_NAME = "Redis";

        #endregion

        #region Private Properties

        /// <summary>
        /// Instância do banco de dados Redis
        /// </summary>
        private IDatabase DataBase { get; }

        #endregion

        #region Public Constructors

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="configuration">Instância do configuração da aplicação</param>
        public RedisConnection(IConfiguration configuration)
        {
            var connection = ConnectionMultiplexer.Connect(configuration.GetConnectionString(CONNECTION_NAME));

            DataBase = connection.GetDatabase();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Retorna o valor armazenado no contexto a partir da chave
        /// </summary>
        /// <param name="key">Chave</param>
        /// <returns>Valor armazenado no contexto</returns>
        public string GetValueFromKey(string key)
        {
            return DataBase.StringGet(key);
        }

        /// <summary>
        /// Armazena valor no contexto para uma chave
        /// </summary>
        /// <param name="key">Chave</param>
        /// <param name="value">Valor</param>
        public void SetValueToKey(string key, string value)
        {
            DataBase.StringSet(key, value);
        }

        #endregion
    }
}
