namespace Fantabulous.Mysql.Constants
{
    /// <summary>
    /// SQL query strings for working with series.
    /// </summary>
    internal static class SeriesSql
    {
        private const string Select = @"
            SELECT  id          AS Id,
                    name        AS Name
            FROM    series";

        internal const string SelectById = Select + @"
            WHERE   id = @Id;";

        internal const string SelectByIds = Select + @"
            WHERE   id IN @Ids;";

        internal const string Insert = @"
            INSERT INTO series
            SET         name = @Name;
            " + CommonSql.SelectLastInsertId;
    }
}
