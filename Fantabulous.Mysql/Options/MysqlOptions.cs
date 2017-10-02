using Fantabulous.Core.Exceptions;

namespace Fantabulous.Mysql.Options
{
    /// <summary>
    /// Options used to connect to a MySQL database.
    /// </summary>
    public class MysqlOptions
    {
        /// <summary>
        /// The hostname of the MySQL database server.
        /// </summary>
        public string Hostname { get; set; }

        /// <summary>
        /// The port to connect to MySQL on (defaults to 3306).
        /// </summary>
        public int Port { get; set; } = 3306;

        /// <summary>
        /// The default MySQL database to use.
        /// </summary>
        public string Database { get; set; }

        /// <summary>
        /// The username to connect to MySQL as.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The password to connect to MySQL with.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Validate these options.
        /// </summary>
        /// <exception cref="InvalidConfigurationException">
        /// Thrown if the options are invalid
        /// </exception>
        public void Validate()
        {
            if (string.IsNullOrEmpty(Hostname))
                throw new InvalidConfigurationException(
                    "Missing MySQL hostname");
            if (string.IsNullOrEmpty(Database))
                throw new InvalidConfigurationException(
                    "Missing MySQL database");
            if (string.IsNullOrEmpty(Username))
                throw new InvalidConfigurationException(
                    "Missing MySQL username");
        }
    }
}
