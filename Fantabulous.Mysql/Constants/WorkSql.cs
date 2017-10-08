namespace Fantabulous.Mysql.Constants
{
    /// <summary>
    /// SQL query strings for working with works.
    /// </summary>
    internal static class WorkSql
    {
        private const string Select = @"
            SELECT      id      AS Id,
                        name    AS Name
            FROM        works
        ";

        internal const string SelectById = Select + @"
            WHERE       id = @Id
        ;";

        internal const string SelectByIds = Select + @"
            WHERE       id IN @Ids
            ORDER BY    id
        ;";

        internal const string SelectIdsBySeriesId = @"
            SELECT      work_id
            FROM        series_works
            WHERE       series_id = @SeriesId
            ORDER BY    position
        ;";

        internal const string SelectIdsBySeriesIds = @"
            SELECT      series_id   AS ParentId,
                        work_id     AS ChildId
            FROM        series_works
            WHERE       series_id IN @SeriesIds
            ORDER BY    series_id,
                        position
        ;";

        internal const string Insert = @"
            INSERT INTO works
            SET         name = @Name
        ;" + CommonSql.SelectLastInsertId;
    }
}
