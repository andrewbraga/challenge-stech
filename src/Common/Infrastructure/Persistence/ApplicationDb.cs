using System.Threading.Tasks;
using Infrastructure.Persistence.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace Infrastructure.Persistence
{
    /// <summary>
    /// Banco de dados da aplicação
    /// </summary>
    public class ApplicationDb : IApplicationDb
    {

        #region Private Properties

        /// <summary>
        /// Instância do cliente do banco de dados Redis
        /// </summary>
        private IDatabase Database { get; }

        #endregion

        #region Public Constructors

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="connectionMultiplexer">Conexão do banco de dados Redis</param>
        public ApplicationDb(IConnectionMultiplexer connectionMultiplexer)
        {
            Database = connectionMultiplexer.GetDatabase();
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
            return Database.StringGet(key);
        }

        /// <summary>
        /// Armazena valor no contexto para uma chave
        /// </summary>
        /// <param name="key">Chave</param>
        /// <param name="value">Valor</param>
        public void SetValueToKey(string key, string value)
        {
            Database.StringSet(key, value);
        }

        #endregion
    }
}
