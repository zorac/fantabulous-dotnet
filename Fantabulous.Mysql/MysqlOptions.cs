namespace Fantabulous.Mysql
{
    public class MysqlOptions
    {
        public string Hostname { get; set; }
        public int Port { get; set; } = 3306;
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
