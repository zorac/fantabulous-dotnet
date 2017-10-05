namespace Fantabulous.Mysql.Constants
{
    /// <summary>
    /// SQL query strings for working with users.
    /// </summary>
    internal static class UserSql
    {
        private const string Select = @"
            SELECT  id      AS Id,
                    name    AS Name
            FROM    users";

        internal const string SelectById = Select + @"
            WHERE   id = @Id;";

        internal const string SelectByIds = Select + @"
            WHERE   id IN @Ids;";

        internal const string SelectByName = Select + @"
            WHERE   name = @Name;";

        internal const string LoginByUsername = Select + @"
            WHERE   name = @Username
              AND   password = SHA2(CONCAT(@Password, salt), 256);";

        internal const string LoginByEmail = Select + @"
            WHERE   email = @Email
              AND   password = SHA2(CONCAT(@Password, salt), 256);";

        internal const string Insert = @"
            INSERT INTO users
            SET         name = @Username,
                        email = @Email,
                        salt = TO_BASE64(RANDOM_BYTES(24)),
                        password = SHA2(CONCAT(@Password, salt), 256);
            " + CommonSql.SelectLastInsertId;

        internal const string UpdatePassword = @"
            UPDATE  users
            SET     password = SHA2(CONCAT(@NewPassword, salt), 256)
            WHERE   user_id = @Id
              AND   password = SHA2(CONCAT(@OldPassword, salt), 256);";
    }
}
