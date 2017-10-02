namespace Fantabulous.Mysql.Constants
{
    /// <summary>
    /// SQL query strings for working with pseudonyms.
    /// </summary>
    internal static class PseudSql
    {
        private const string Select = @"
            SELECT  pseud_id    AS Id,
                    user_id     AS UserId,
                    name        AS Name
            FROM    pseuds";

        internal const string SelectById = Select + @"
            WHERE   pseud_id = @Id;";

        internal const string SelectByIds = Select + @"
            WHERE   pseud_id IN @Ids;";

        internal const string SelectByUserAndName = Select + @"
            WHERE   user_id = @UserId
              AND   name = @Name;";

        internal const string Insert = @"
            INSERT INTO pseuds
            SET         user_id = @UserId,
                        name = @Name;
            " + CommonSql.SelectLastInsertId;
    }
}
