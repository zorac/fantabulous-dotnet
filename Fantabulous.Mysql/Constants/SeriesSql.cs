namespace Fantabulous.Mysql.Constants
{
    /// <summary>
    /// SQL query strings for working with series.
    /// </summary>
    internal static class SeriesSql
    {
        private const string Select = @"
            SELECT      id      AS Id,
                        name    AS Name
            FROM        series
        ";

        internal const string SelectById = Select + @"
            WHERE       id = @Id
        ;";

        internal const string SelectByIds = Select + @"
            WHERE       id IN @Ids
            ORDER BY    id
        ;";

        internal const string SelectIdsByWorkId = @"
            SELECT      series_id
            FROM        series_works
            WHERE       work_id = @WorkId
            ORDER BY    series_id
        ;";

        internal const string SelectIdsByWorkIds = @"
            SELECT      work_id                                     AS ParentId,
                        GROUP_CONCAT(series_id ORDER BY series_id)  AS ChildIdString
            FROM        series_works
            WHERE       work_id IN @WorkIds
            GROUP BY    work_id
        ;";

        internal const string Insert = @"
            INSERT INTO series
            SET         name = @Name
        ;" + CommonSql.SelectLastInsertId;
    }
}
