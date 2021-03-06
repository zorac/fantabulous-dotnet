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
            SELECT      series_id                               AS ParentId,
                        GROUP_CONCAT(work_id ORDER BY position) AS ChildIdString
            FROM        series_works
            WHERE       series_id IN @SeriesIds
            GROUP BY    series_id
        ;";

        internal const string SelectIdsByPseudId = @"
            SELECT      work_id
            FROM        work_pseuds
            INNER JOIN  works
              ON        work_id = id
            WHERE       pseud_id = @PseudId
            ORDER BY    majorly_updated DESC
        ;";

        internal const string SelectIdsByTagId = @"
            SELECT      work_id
            FROM        work_tags
            INNER JOIN  works
              ON        work_id = id
            WHERE       tag_id = @TagId
            ORDER BY    majorly_updated DESC
        ;";

        internal const string Insert = @"
            INSERT INTO works
            SET         name = @Name
        ;" + CommonSql.SelectLastInsertId;
    }
}
