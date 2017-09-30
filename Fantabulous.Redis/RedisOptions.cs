using Fantabulous.Core.Exceptions;

namespace Fantabulous.Redis
{
    /// <summary>
    /// Options for a Redis repository.
    /// </summary>
    public class RedisOptions
    {
        /// <summary>
        /// The hostname of the Redis server.
        /// </summary>
        public string Hostname { get; set; }

        /// <summary>
        /// The port the Redis server is running on (defaults to 6379).
        /// </summary>
        public int Port { get; set; } = 6379;

        /// <summary>
        /// The default Redis database number (defaults to 0).
        /// </summary>
        public int Database { get; set; } = 0;

        /// <summary>
        /// A password to connect to Redis with, if needed.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Validate these options.
        /// </summary>
        /// <param name="where">
        /// Specifies the location in the configuration
        /// </param>
        /// <exception cref="InvalidConfigurationException">
        /// Thrown if the options are invalid
        /// </exception>
        public void Validate(string where)
        {
            if (string.IsNullOrEmpty(Hostname))
                throw new InvalidConfigurationException(
                    $"Missing Redis hostname in {where}");
        }
    }
}
