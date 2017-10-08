namespace Fantabulous.Mysql.Constants
{
    /// <summary>
    /// SQL query strings for working with chapters.
    /// </summary>
    internal static class ChapterSql
    {
        private const string Select = @"
            SELECT      id          AS Id,
                        work_id     AS WorkId,
                        position    AS Position,
                        name        AS Name
            FROM        chapters
        ";

        internal const string SelectById = Select + @"
            WHERE       id = @Id
        ;";

        internal const string SelectByIds = Select + @"
            WHERE       id IN @Ids
            ORDER BY    id
        ;";

        internal const string SelectIdsByWorkId = @"
            SELECT      id
            FROM        chapters
            WHERE       work_id = @WorkId
            ORDER BY    position
        ;";

        internal const string SelectIdsByWorkIds = @"
            SELECT      work_id AS ParentId,
                        id      AS ChildId
            FROM        chapters
            WHERE       work_id IN @WorkIds
            ORDER BY    work_id,
                        position
        ;";

        internal const string Insert = @"
            INSERT INTO chapters
            SET         work_id = @WorkId,
                        position = @Position,
                        name = @Name
        ;" + CommonSql.SelectLastInsertId;
    }
}
