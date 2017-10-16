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
            SELECT      user_id                         AS ParentId,
                        GROUP_CONCAT(id ORDER BY name)  AS PseudId
            FROM        pseuds
            WHERE       user_id IN @UserIds
            GROUP BY    user_id
        ;";

        internal const string SelectIdsByWorkId = @"
            SELECT      pseud_id
            FROM        work_pseuds
            WHERE       work_id = @WorkId
            ORDER BY    position
        ;";

        internal const string SelectIdsByWorkIds = @"
            SELECT      work_id                                     AS ParentId,
                        GROUP_CONCAT(pseud_id ORDER BY position)    AS ChildIdString
            FROM        work_pseuds
            WHERE       work_id IN @WorkIds
            GROUP BY    work_id
        ;";

        internal const string SelectIdsBySeriesId = @"
            SELECT      pseud_id
            FROM        work_pseuds
            INNER JOIN  series_works
              ON        series_works.work_id = work_pseuds.work_id
            WHERE       series_id = @SeriesId
            GROUP BY    pseud_id
            ORDER BY    COUNT(*) DESC,
                        MIN(series_works.position),
                        MIN(work_pseuds.position)
        ;";

        internal const string SelectIdsBySeriesIds = @"
            SELECT      series_id               AS ParentId,
                        GROUP_CONCAT(pseud_id)  AS ChildIdString
            FROM        (
                SELECT      series_id,
                            pseud_id
                FROM        work_pseuds
                INNER JOIN  series_works
                  ON        series_works.work_id = work_pseuds.work_id
                WHERE       series_id IN @SeriesIds
                GROUP BY    series_id,
                            pseud_id
                ORDER BY    series_id,
                            COUNT(*) DESC,
                            MIN(series_works.position),
                            MIN(work_pseuds.position)
                        ) AS series_pseuds
            GROUP BY    series_id
        ;";

        internal const string Insert = @"
            INSERT INTO pseuds
            SET         user_id = @UserId,
                        name = @Name
        ;" + CommonSql.SelectLastInsertId;
    }
}
