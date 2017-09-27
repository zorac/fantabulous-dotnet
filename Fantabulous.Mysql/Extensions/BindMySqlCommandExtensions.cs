using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MySql.Data.MySqlClient
{
    public static class BindMySqlCommandExtensions
    {
        public static void Bind(
            this MySqlCommand command,
            string param,
            uint value)
        {
            command.Parameters.Add(new MySqlParameter
            {
                ParameterName = param,
                DbType = DbType.UInt32,
                Value = (uint)value,
            });
        }

        public static void Bind(
            this MySqlCommand command,
            string param,
            ulong value)
        {
            command.Parameters.Add(new MySqlParameter
            {
                ParameterName = param,
                DbType = DbType.UInt64,
                Value = (ulong)value,
            });
        }

        public static void Bind(
            this MySqlCommand command,
            string param,
            string value)
        {
            command.Parameters.Add(new MySqlParameter
            {
                ParameterName = param,
                DbType = DbType.String,
                Value = value,
            });
        }

        public static void BindId(this MySqlCommand command, long id)
        {
            command.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.UInt32,
                Value = (uint)id,
            });
        }

        public static void BindLongId(this MySqlCommand command, long id)
        {
            command.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.UInt64,
                Value = (ulong)id,
            });
        }

        public static void BindName(this MySqlCommand command, string name)
        {
            command.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@name",
                DbType = DbType.String,
                Value = name,
            });
        }

        public static void BindIds(
            this MySqlCommand command,
            IEnumerable<long> ids)
        {
            var names = new StringBuilder();
            var parameters = new List<MySqlParameter>();
            var i = 1;

            foreach (var id in ids)
            {
                var name = $"@id{i}";

                if (names.Length > 0) names.Append(',');
                names.Append(name);

                parameters.Add(new MySqlParameter
                {
                    ParameterName = name,
                    DbType = DbType.UInt32,
                    Value = (uint)id,
                });
            }

            command.CommandText = command.CommandText.Replace("@ids",
                names.ToString());
            command.Parameters.AddRange(parameters.ToArray());
        }
    }
}
