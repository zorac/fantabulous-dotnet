using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using MySql.Data.MySqlClient;

using Fantabulous.Core.Repositories;
using Fantabulous.Mysql.Options;

namespace Fantabulous.Mysql.Repositories
{
    /// <summary>
    /// A repository which connects to a MySQL database.
    /// </summary>
    public class MysqlRepository : ISqlRepository
    {
        private readonly string ConnectionString;
        private readonly ILogger Logger;

        /// <summary>
        /// Create a new MySQL repository.
        /// </summary>
        /// <param name="options">
        /// The options to use to create connections.
        /// </param>
        /// <param name="logger">
        /// A logger for this repository.
        /// </param>
        public MysqlRepository(
            MysqlOptions options,
            ILogger<MysqlRepository> logger)
        {
            ConnectionString = BuildConnectionString(options);
            Logger = logger;
            logger.LogInformation("Repository initialised");
        }

        public async Task<ISqlDb> GetDatabaseAsync()
        {
            var connection = new MySqlConnection(ConnectionString);

            await connection.OpenAsync();

            return new MysqlDb(connection);
        }

        private string BuildConnectionString(MysqlOptions options)
        {
            return new MySqlConnectionStringBuilder
            {
                Server = options.Hostname,
                Port = (uint)options.Port,
                UserID = options.Username,
                Password = options.Password,
                Database = options.Database,
                TreatTinyAsBoolean = true
            }.ToString();
        }
    }
}
