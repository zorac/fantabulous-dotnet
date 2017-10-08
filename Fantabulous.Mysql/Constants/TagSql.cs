namespace Fantabulous.Mysql.Constants
{
    /// <summary>
    /// SQL query strings for working with tags.
    /// </summary>
    internal static class TagSql
    {
        private const string Select = @"
            SELECT      id          AS Id,
                        type        AS Type,
                        alias_for   AS AliasFor,
                        name        AS Name
            FROM        tags
        ";

        internal const string SelectById = Select + @"
            WHERE       id = @Id;
        ";

        internal const string SelectByIds = Select + @"
            WHERE       id IN @Ids
            ORDER BY    id
        ;";

        internal const string SelectIdsByWorkId = @"
            SELECT      tag_id
            FROM        work_tags
            WHERE       work_id = @WorkId
            ORDER BY    position
        ;";

        internal const string SelectIdsByWorkIds = @"
            SELECT      work_id AS ParentId,
                        tag_id  AS ChildId
            FROM        work_tags
            WHERE       work_id IN @WorkIds
            ORDER BY    work_id,
                        position
        ;";

        internal const string SelectIdsBySeriesId = @"
            SELECT      tags.id
            FROM        tags
            INNER JOIN  work_tags
              ON        work_tags.tag_id = tags.id
            INNER JOIN  series_works
              ON        series_works.work_id = work_tags.work_id
            WHERE       series_works.series_id = @SeriesId
            GROUP BY    tags.id
            ORDER BY    tags.type,
                        COUNT(*) DESC,
                        MIN(series_works.position),
                        MIN(work_tags.position)
        ;";

        internal const string SelectIdsBySeriesIds = @"
            SELECT      series_works.series_id  AS ParentId,
                        tags.id                 AS ChildId
            FROM        tags
            INNER JOIN  work_tags
              ON        work_tags.tag_id = tags.id
            INNER JOIN  series_works
              ON        series_works.work_id = work_tags.work_id
            WHERE       series_works.series_id IN @SeriesIds
            GROUP BY    series_works.series_id,
                        tags.id
            ORDER BY    series_works.series_id,
                        tags.type,
                        COUNT(*) DESC,
                        MIN(series_works.position),
                        MIN(work_tags.position)
        ;";

        internal const string Insert = @"
            INSERT INTO tags
            SET         type = @Type,
                        alias_for = @AliasFor,
                        name = @Name
        ;" + CommonSql.SelectLastInsertId;
    }
}
