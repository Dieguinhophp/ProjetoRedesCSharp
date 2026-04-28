using Microsoft.Data.Sqlite;

class Banco
{
    private static string conexao = "Data Source=usuarios.db";

    public static void Inicializar()
    {
        using (var conn = new SqliteConnection(conexao))
        {
            conn.Open();

            string sql = @"CREATE TABLE IF NOT EXISTS usuarios (
                            id INTEGER PRIMARY KEY AUTOINCREMENT,
                            username TEXT UNIQUE,
                            password TEXT
                          );";

            var cmd = new SqliteCommand(sql, conn);
            cmd.ExecuteNonQuery();
        }
    }

    public static bool Registrar(string user, string pass)
    {
        using (var conn = new SqliteConnection(conexao))
        {
            conn.Open();

            string sql = "INSERT INTO usuarios (username, password) VALUES (@u, @p)";
            var cmd = new SqliteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@u", user);
            cmd.Parameters.AddWithValue("@p", pass);

            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public static bool Login(string user, string pass)
    {
        using (var conn = new SqliteConnection(conexao))
        {
            conn.Open();

            string sql = "SELECT COUNT(*) FROM usuarios WHERE username=@u AND password=@p";
            var cmd = new SqliteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@u", user);
            cmd.Parameters.AddWithValue("@p", pass);

            int count = Convert.ToInt32(cmd.ExecuteScalar());
            return count > 0;
        }
    }
}