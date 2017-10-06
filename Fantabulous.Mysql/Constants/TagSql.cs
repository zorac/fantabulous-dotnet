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

        internal const string SelectIdsByWork = @"
            SELECT      tag_id
            FROM        work_tags
            WHERE       work_id = @WorkId
            ORDER BY    position
        ;";

        internal const string Insert = @"
            INSERT INTO tags
            SET         type = @Type,
                        alias_for = @AliasFor,
                        name = @Name
        ;" + CommonSql.SelectLastInsertId;
    }
}
