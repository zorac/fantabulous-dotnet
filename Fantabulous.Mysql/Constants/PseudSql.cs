namespace Fantabulous.Mysql.Constants
{
    /// <summary>
    /// SQL query strings for working with pseudonyms.
    /// </summary>
    internal static class PseudSql
    {
        private const string Select = @"
            SELECT      id      AS Id,
                        user_id AS UserId,
                        name    AS Name
            FROM        pseuds
        ";

        internal const string SelectById = Select + @"
            WHERE       id = @Id
        ;";

        internal const string SelectByIds = Select + @"
            WHERE       id IN @Ids
            ORDER BY    id
        ;";

        internal const string SelectByUserAndName = Select + @"
            WHERE       user_id = @UserId
              AND       name = @Name
        ;";

        internal const string SelectIdsByUser = @"
            SELECT      id
            FROM        pseuds
            WHERE       user_id = @UserId
            ORDER BY    name
        ;";

        internal const string SelectIdsByWork = @"
            SELECT      pseud_id
            FROM        work_pseuds
            WHERE       work_id = @WorkId
            ORDER BY    position
        ;";

        internal const string Insert = @"
            INSERT INTO pseuds
            SET         user_id = @UserId,
                        name = @Name
        ;" + CommonSql.SelectLastInsertId;
    }
}
