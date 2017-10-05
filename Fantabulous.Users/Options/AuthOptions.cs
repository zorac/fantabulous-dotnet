namespace Fantabulous.Users.Options
{
    /// <summary>
    /// Configuration options for the auth service.
    /// </summary>
    public class AuthOptions
    {
        /// <summary>
        /// Set to true to include a SQL auth service.
        /// </summary>
        public bool Sql { get; set; } = false;
    }
}
