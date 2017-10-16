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

        internal const string SelectTypesAndIdsByWorkId = @"
            SELECT      type                                AS Type,
                        GROUP_CONCAT(id ORDER BY position)  AS ChildIdString
            FROM        work_tags
            INNER JOIN  tags
              ON        id = tag_id
            WHERE       work_id = @WorkId
            GROUP BY    type
        ;";

        internal const string SelectTypesAndIdsByWorkIds = @"
            SELECT      work_id                             AS ParentId,
                        type                                AS Type,
                        GROUP_CONCAT(id ORDER BY position)  AS ChildIdString
            FROM        work_tags
            INNER JOIN  tags
              ON        id = tag_id
            WHERE       work_id IN @WorkIds
            GROUP BY    work_id,
                        type
        ;";

        internal const string SelectTypesAndIdsBySeriesId = @"
            SELECT      type                AS Type,
                        GROUP_CONCAT(id)    AS ChildIdString
            FROM        (
                SELECT      type,
                            id
                FROM        tags
                INNER JOIN  work_tags
                    ON      tag_id = tags.id
                INNER JOIN  series_works
                    ON      series_works.work_id = work_tags.work_id
                WHERE       series_id = @SeriesId
                GROUP BY    tags.type,
                            tags.id
                ORDER BY    tags.type,
                            COUNT(*) DESC,
                            MIN(series_works.position),
                            MIN(work_tags.position)
                        ) AS type_ids
            GROUP BY    type
          ;";

        internal const string SelectTypesAndIdsBySeriesIds = @"
            SELECT      series_id           AS ParentId,
                        type                AS Type,
                        GROUP_CONCAT(id)    AS ChildIdString
            FROM        (
                SELECT      series_id,
                            type,
                            id
                FROM        tags
                INNER JOIN  work_tags
                  ON        tag_id = tags.id
                INNER JOIN  series_works
                  ON        series_works.work_id = work_tags.work_id
                WHERE       series_id IN @SeriesIds
                GROUP BY    series_id,
                            type,
                            id
                ORDER BY    series_id,
                            type,
                            COUNT(*) DESC,
                            MIN(series_works.position),
                            MIN(work_tags.position)
                        ) AS series_type_ids
            GROUP BY    series_id,
                        type
        ;";

        internal const string Insert = @"
            INSERT INTO tags
            SET         type = @Type,
                        alias_for = @AliasFor,
                        name = @Name
        ;" + CommonSql.SelectLastInsertId;
    }
}
