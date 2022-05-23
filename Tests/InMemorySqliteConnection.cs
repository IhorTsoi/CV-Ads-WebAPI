using Microsoft.Data.Sqlite;

namespace Tests
{
    public class InMemorySqliteConnection : SqliteConnection
    {
        public InMemorySqliteConnection() : base("Filename=:memory:") { }
    }
}
