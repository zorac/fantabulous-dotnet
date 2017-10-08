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

        internal const string SelectByUserIdAndName = Select + @"
            WHERE       user_id = @UserId
              AND       name = @Name
        ;";

        internal const string SelectIdsByUserId = @"
            SELECT      id
            FROM        pseuds
            WHERE       user_id = @UserId
            ORDER BY    name
        ;";

        internal const string SelectIdsByUserIds = @"
            SELECT      user_id AS UserId,
                        id      AS PseudId
            FROM        pseuds
            WHERE       user_id IN @UserIds
            ORDER BY    user_id,
                        name
        ;";

        internal const string SelectIdsByWorkId = @"
            SELECT      pseud_id
            FROM        work_pseuds
            WHERE       work_id = @WorkId
            ORDER BY    position
        ;";

        internal const string SelectIdsByWorkIds = @"
            SELECT      work_id     AS ParentId,
                        pseud_id    AS ChildId
            FROM        work_pseuds
            WHERE       work_id IN @WorkIds
            ORDER BY    work_id,
                        position
        ;";

        internal const string SelectIdsBySeriesId = @"
            SELECT      work_pseuds.pseud_id
            FROM        work_pseuds
            INNER JOIN  series_works
              ON        series_works.work_id = work_pseuds.work_id
            WHERE       series_works.series_id = @SeriesId
            GROUP BY    work_pseuds.pseud_id
            ORDER BY    COUNT(*) DESC,
                        MIN(series_works.position),
                        MIN(work_pseuds.position)
        ;";

        internal const string SelectIdsBySeriesIds = @"
            SELECT      series_works.series_id  AS ParentId,
                        work_pseuds.pseud_id    AS ChildId
            FROM        work_pseuds
            INNER JOIN  series_works
              ON        series_works.work_id = work_pseuds.work_id
            WHERE       series_works.series_id IN (1,2,3,4,5)
            GROUP BY    series_works.series_id,
                        work_pseuds.pseud_id
            ORDER BY    series_works.series_id,
                        COUNT(*) DESC,
                        MIN(series_works.position),
                        MIN(work_pseuds.position)
        ;";

        internal const string Insert = @"
            INSERT INTO pseuds
            SET         user_id = @UserId,
                        name = @Name
        ;" + CommonSql.SelectLastInsertId;
    }
}
