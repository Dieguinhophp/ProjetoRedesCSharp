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
                            password TEXT,
                            cargo TEXT DEFAULT 'Brigadista',
                            status TEXT DEFAULT 'Offline',
                            equipe TEXT DEFAULT 'Base Central',
                            ultimo_login TEXT);";

            var cmd = new SqliteCommand(sql, conn);
            cmd.ExecuteNonQuery();
        }
    }

    public static bool Registrar(string user, string pass, string cargo)
    {
        using (var conn = new SqliteConnection(conexao))
        {
            conn.Open();

            string sql = @"INSERT INTO usuarios(username, password, cargo, status, equipe) VALUES (@u, @p, @c, @s, @e)";
            var cmd = new SqliteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@u", user);
            cmd.Parameters.AddWithValue("@p", pass);
            cmd.Parameters.AddWithValue("@c", cargo);
            cmd.Parameters.AddWithValue("@s", "Offline");
            cmd.Parameters.AddWithValue("@e", "Base Central");

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

            string sql = @"SELECT COUNT(*)
                       FROM usuarios
                       WHERE username=@u AND password=@p";

            var cmd = new SqliteCommand(sql, conn);

            cmd.Parameters.AddWithValue("@u", user);
            cmd.Parameters.AddWithValue("@p", pass);

            int count = Convert.ToInt32(cmd.ExecuteScalar());

            if (count > 0)
            {
                string update = @"UPDATE usuarios
                              SET status='Online',
                                  ultimo_login=@data
                              WHERE username=@u";

                var cmdUpdate =
                    new SqliteCommand(update, conn);

                cmdUpdate.Parameters.AddWithValue("@u", user);

                cmdUpdate.Parameters.AddWithValue(
                    "@data",
                    DateTime.Now.ToString("dd/MM/yyyy HH:mm")
                );

                cmdUpdate.ExecuteNonQuery();

                return true;
            }

            return false;
        }
    }


    public static string ObterCargo(string user)
    {
        using (var conn = new SqliteConnection(conexao))
        {
            conn.Open();

            string sql =
                "SELECT cargo FROM usuarios WHERE username=@u";

            var cmd =
                new SqliteCommand(sql, conn);

            cmd.Parameters.AddWithValue("@u", user);

            object resultado = cmd.ExecuteScalar();

            if (resultado != null)
                return resultado.ToString();

            return "Brigadista";
        }
    }
}