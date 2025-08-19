using MySqlConnector;
using Option;
using Team;

namespace Tournament
{
    public class TournamentObject
    {
        public string? Nombre { get; set; } = string.Empty;
        public string? Tipo { get; set; } = string.Empty; 
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public ICollection<TeamObject>? Equipos { get; set; } = new HashSet<TeamObject>();
    }

    public static class TournamentMenu
    {
        public static void AddTournament()
        {
            string? name = "";
            string? type = "";
            DateTime creationDate;
            DateTime endDate;

            Console.Clear();
            Console.WriteLine("=== Ingrese el nombre del nuevo torneo ===");
            Console.Write("-> ");
            name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("❌ El nombre es obligatorio.");
                Console.ReadKey();
                return;
            }

            Console.Write("Tipo del torneo: ");
            type = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(type))
            {
                Console.WriteLine("❌ El tipo es obligatorio.");
                Console.ReadKey();
                return;
            }

            Console.Write("Fecha de inicio (yyyy-MM-dd): ");
            creationDate = DateTime.Parse(Console.ReadLine() ?? DateTime.Now.ToString());

            Console.Write("Fecha de fin (yyyy-MM-dd): ");
            endDate = DateTime.Parse(Console.ReadLine() ?? DateTime.Now.ToString());

            using var conn = Shared.Helpers.DbHelper.GetConnection();
            conn.Open();

            var checkCmd = new MySqlCommand("SELECT COUNT(*) FROM Torneo WHERE nombre = @nombre", conn);
            checkCmd.Parameters.AddWithValue("@nombre", name);
            var exists = Convert.ToInt32(checkCmd.ExecuteScalar());

            if (exists > 0)
            {
                Console.WriteLine("❌ Este nombre ya existe, ingrese otro...");
                Console.ReadKey();
                return;
            }

            var cmd = new MySqlCommand(@"
                INSERT INTO Torneo (nombre, tipo, fecha_inicio, fecha_fin) 
                VALUES (@nombre, @tipo, @inicio, @fin);
                SELECT LAST_INSERT_ID();", conn);

            cmd.Parameters.AddWithValue("@nombre", name);
            cmd.Parameters.AddWithValue("@tipo", type);
            cmd.Parameters.AddWithValue("@inicio", creationDate);
            cmd.Parameters.AddWithValue("@fin", endDate);

            int newId = Convert.ToInt32(cmd.ExecuteScalar());


            Console.WriteLine($"✅ Torneo añadido correctamente con ID: {newId}");
            Console.ReadKey();
        }

        public static void SearchTournament()
        {
            Console.Clear();
            Console.WriteLine("=== 🔍 Buscar Torneo por ID 🔍 ===");
            Console.Write("-> ");
            int id = Convert.ToInt32(Console.ReadLine());
            if (id <= 0)
            {
                Console.WriteLine("❌ El ID debe ser un número positivo.");
                Console.ReadKey();
                return;
            }
            
            using var conn = Shared.Helpers.DbHelper.GetConnection();
            conn.Open();

            var cmd = new MySqlCommand("SELECT * FROM Torneo WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                Console.WriteLine("=== ⭐ ¡Torneo Encontrado! ⭐ ===");
                Console.WriteLine($"Id: {reader["id"]}, Nombre: {reader["nombre"]}, Tipo: {reader["tipo"]}, Inicio: {reader["fecha_inicio"]}, Fin: {reader["fecha_fin"]}");
            }
            else
            {
                Console.WriteLine("❌ ¡Torneo No Encontrado!");
            }

            Console.ReadKey();
        }

        public static void DeleteTournament()
        {
            Console.Clear();
            Console.WriteLine("=== 🗑️ Eliminar Torneo por ID 🗑️ ===");
            Console.Write("-> ");
            int id = Convert.ToInt32(Console.ReadLine());

            using var conn = Shared.Helpers.DbHelper.GetConnection();
            conn.Open();

            var deleteRelationsCmd = new MySqlCommand(
                "DELETE FROM Torneo_Equipo WHERE torneoId = @id", conn);
            deleteRelationsCmd.Parameters.AddWithValue("@id", id);
            deleteRelationsCmd.ExecuteNonQuery();

            var deleteTournamentCmd = new MySqlCommand(
                "DELETE FROM Torneo WHERE id = @id", conn);
            deleteTournamentCmd.Parameters.AddWithValue("@id", id);

            int rows = deleteTournamentCmd.ExecuteNonQuery();

            if (rows > 0)
            {
                Console.WriteLine("✅ ¡Torneo eliminado correctamente!");
            }
            else
            {
                Console.WriteLine("❌ ¡Torneo no encontrado!");
            }

            Console.ReadKey();
        }

        public static void UpdateTournament()
        {
            Console.Clear();
            Console.WriteLine("=== 🔄 Actualizar Torneo 🔄 ===");
            Console.Write("Ingrese el ID del torneo: ");
            int id = Convert.ToInt32(Console.ReadLine());

            using var conn = Shared.Helpers.DbHelper.GetConnection();
            conn.Open();

            var checkCmd = new MySqlCommand("SELECT COUNT(*) FROM Torneo WHERE id = @id", conn);
            checkCmd.Parameters.AddWithValue("@id", id);
            int exists = Convert.ToInt32(checkCmd.ExecuteScalar());

            if (exists == 0)
            {
                Console.WriteLine("❌ El torneo no existe.");
                Console.ReadKey();
                return;
            }

            Console.Write("Nuevo nombre: ");
            string? name = Console.ReadLine();

            Console.Write("Nuevo tipo: ");
            string? type = Console.ReadLine();

            Console.Write("Nueva fecha de inicio (yyyy-MM-dd): ");
            DateTime startDate = DateTime.Parse(Console.ReadLine() ?? DateTime.Now.ToString());

            Console.Write("Nueva fecha de fin (yyyy-MM-dd): ");
            DateTime endDate = DateTime.Parse(Console.ReadLine() ?? DateTime.Now.ToString());

            var updateCmd = new MySqlCommand(
                "UPDATE Torneo SET nombre = @nombre, tipo = @tipo, fecha_inicio = @inicio, fecha_fin = @fin WHERE id = @id",
                conn
            );

            updateCmd.Parameters.AddWithValue("@nombre", name);
            updateCmd.Parameters.AddWithValue("@tipo", type);
            updateCmd.Parameters.AddWithValue("@inicio", startDate);
            updateCmd.Parameters.AddWithValue("@fin", endDate);
            updateCmd.Parameters.AddWithValue("@id", id);

            int rows = updateCmd.ExecuteNonQuery();

            if (rows > 0)
            {
                Console.WriteLine("✅ Torneo actualizado correctamente.");
            }
            else
            {
                Console.WriteLine("⚠️ No se pudo actualizar el torneo.");
            }
            Console.ReadKey();
        }

        public static void ShowTournaments()
        {
            Console.Clear();
            Console.WriteLine("=== 🏆 Lista de Torneos ===");

            using var conn = Shared.Helpers.DbHelper.GetConnection();
            conn.Open();

            var cmd = new MySqlCommand("SELECT id, nombre, tipo, fecha_inicio, fecha_fin FROM Torneo", conn);
            using var reader = cmd.ExecuteReader();

            if (!reader.HasRows)
            {
                Console.WriteLine("❌ No hay torneos registrados.");
            }
            else
            {
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader.GetInt32("id")} | Nombre: {reader.GetString("nombre")} | Tipo: {reader.GetString("tipo")} | Inicio: {reader.GetDateTime("fecha_inicio"):yyyy-MM-dd} | Fin: {reader.GetDateTime("fecha_fin"):yyyy-MM-dd}");
                }
            }

            Console.ReadKey();
        }
    }
}