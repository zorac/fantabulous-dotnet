namespace Fantabulous.Mysql.Constants
{
    /// <summary>
    /// SQL query strings for working with works.
    /// </summary>
    internal static class WorkSql
    {
        private const string Select = @"
            SELECT  id          AS Id,
                    name        AS Name
            FROM    works";

        internal const string SelectById = Select + @"
            WHERE   id = @Id;";

        internal const string SelectByIds = Select + @"
            WHERE   id IN @Ids;";

        internal const string Insert = @"
            INSERT INTO works
            SET         name = @Name;
            " + CommonSql.SelectLastInsertId;
    }
}
