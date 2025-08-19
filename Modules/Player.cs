using MySqlConnector;
using Option;
using Shared.Helpers;
using Staff;
using Team;
using Tournament;

namespace Player
{
    public class PlayerObject
    {
        public int Id { get; set; }
        public string? Nombre { get; set; } = string.Empty;
        public string? Tipo { get; set; } = string.Empty;
        public string? Pais { get; set; } = string.Empty;
        public decimal Precio { get; set; } = 0;
        public int Asistencias { get; set; } = 0;
        public ICollection<PlayerObject>? Jugadores { get; set; } = new HashSet<PlayerObject>();
        public ICollection<TecnicalBodyObject>? CuerposTecnicos { get; set; } = new HashSet<TecnicalBodyObject>();
        public ICollection<MedicalBodyObject>? CuerposMedicos { get; set; } = new HashSet<MedicalBodyObject>();
        public ICollection<TournamentObject>? Torneos { get; set; } = new HashSet<TournamentObject>();

    }

    public class PlayerMenu
    {
        public static void AddPlayer()
        {
            Console.Clear();
            TeamMenu.ShowTeams();
            Console.WriteLine("\n=== 🆕 Registrar Jugador ===");

            Console.Write("Nombre: ");
            string nombre = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("❌ El nombre no puede estar vacío.");
                Console.ReadKey();
                return;
            }

            Console.Write("Apellido: ");
            string apellido = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(apellido))
            {
                Console.WriteLine("❌ El apellido no puede estar vacío.");
                Console.ReadKey();
                return;
            }

            Console.Write("Edad: ");
            if (!int.TryParse(Console.ReadLine(), out int edad))
            {
                Console.WriteLine("❌ Edad inválida. Debe ser un número.");
                Console.ReadKey();
                return;
            }

            Console.Write("Dorsal: ");
            int dorsal = int.Parse(Console.ReadLine()!);

            Console.Write("Posición: ");
            string posicion = Console.ReadLine()!;

            Console.Write("Precio del jugador: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal precio))
            {
                Console.WriteLine("❌ Precio inválido.");
                Console.ReadKey();
                return;
            }

            if (precio < 0 || precio > 99999999.99m)
            {
                Console.WriteLine("❌ El precio debe estar entre 0 y 99.999.999,99");
                Console.ReadKey();
                return;
            }

            Console.Write("Asistencias del jugador: ");
            if (!int.TryParse(Console.ReadLine(), out int asistencias))
            {
                Console.WriteLine("❌ Asistencias inválidas. Debe ser un número.");
                Console.ReadKey();
                return;
            }
            if (asistencias < 0)
            {
                Console.WriteLine("❌ Las asistencias no pueden ser negativas.");
                Console.ReadKey();
                return;
            }

            Console.Write("ID del equipo al que pertenece: ");
            int equipoId = int.Parse(Console.ReadLine()!);
            if (equipoId <= 0)
            {
                Console.WriteLine("❌ El ID del equipo debe ser un número positivo.");
                Console.ReadKey();
                return;
            }

            using var conn = DbHelper.GetConnection();
            conn.Open();

            var checkTeam = new MySqlCommand("SELECT COUNT(*) FROM Equipo WHERE id = @id", conn);
            checkTeam.Parameters.AddWithValue("@id", equipoId);
            if (Convert.ToInt32(checkTeam.ExecuteScalar()) == 0)
            {
                Console.WriteLine("❌ El equipo no existe.");
                Console.ReadKey();
                return;
            }

            var cmd = new MySqlCommand(
                "INSERT INTO Jugador (nombre, apellido, edad, dorsal, posicion, precio, asistencias, equipoId) " +
                "VALUES (@nombre, @apellido, @edad, @dorsal, @posicion, @precio, @asistencias, @equipoId); SELECT LAST_INSERT_ID();",
                conn
            );
            cmd.Parameters.AddWithValue("@nombre", nombre);
            cmd.Parameters.AddWithValue("@apellido", apellido);
            cmd.Parameters.AddWithValue("@edad", edad);
            cmd.Parameters.AddWithValue("@dorsal", dorsal);
            cmd.Parameters.AddWithValue("@posicion", posicion);
            cmd.Parameters.AddWithValue("@precio", precio);
            cmd.Parameters.AddWithValue("@asistencias", asistencias);
            cmd.Parameters.AddWithValue("@equipoId", equipoId);

            int newId = Convert.ToInt32(cmd.ExecuteScalar());
            Console.WriteLine($"✅ Jugador registrado correctamente con ID: {newId}");
            Console.ReadKey();
        }

        public static void SearchPlayer()
        {
            Console.Clear();
            Console.WriteLine("=== 🔍 Buscar Jugador por ID 🔍");
            Console.Write("-> ");
            int id = int.Parse(Console.ReadLine()!);

            using var conn = DbHelper.GetConnection();
            conn.Open();

            var cmd = new MySqlCommand("SELECT * FROM Jugador WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                Console.WriteLine("=== ⭐ Jugador Encontrado ⭐ ===");
                Console.WriteLine($"ID: {reader["id"]}");
                Console.WriteLine($"Nombre: {reader["nombre"]}");
                Console.WriteLine($"Apellido: {reader["apellido"]}");
                Console.WriteLine($"Edad: {reader["edad"]}");
                Console.WriteLine($"Dorsal: {reader["dorsal"]}");
                Console.WriteLine($"Posición: {reader["posicion"]}");
                Console.WriteLine($"Equipo ID: {reader["equipoId"]}");
                Console.WriteLine($"Precio: {reader["precio"]}");
                Console.WriteLine($"Asistencias: {reader["asistencias"]}");
            }
            else
            {
                Console.WriteLine("❌ Jugador no encontrado.");
            }

            Console.ReadKey();
        }

        public static void UpdatePlayer()
        {
            Console.Clear();
            Console.WriteLine("=== 🔄 Editar Jugador 🔄");
            Console.Write("ID del jugador: ");
            int id = int.Parse(Console.ReadLine()!);

            using var conn = DbHelper.GetConnection();
            conn.Open();

            var checkCmd = new MySqlCommand("SELECT COUNT(*) FROM Jugador WHERE id = @id", conn);
            checkCmd.Parameters.AddWithValue("@id", id);
            if (Convert.ToInt32(checkCmd.ExecuteScalar()) == 0)
            {
                Console.WriteLine("❌ Jugador no encontrado.");
                Console.ReadKey();
                return;
            }

            Console.Write("Nuevo nombre: ");
            string nombre = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("❌ El nombre no puede estar vacío.");
                Console.ReadKey();
                return;
            }

            Console.Write("Nuevo apellido: ");
            string apellido = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(apellido))
            {
                Console.WriteLine("❌ El apellido no puede estar vacío.");
                Console.ReadKey();
                return;
            }

            Console.Write("Nueva edad: ");
            int edad = int.Parse(Console.ReadLine()!);
            if (edad <= 0)
            {
                Console.WriteLine("❌ La edad debe ser un número positivo.");
                Console.ReadKey();
                return;
            }

            Console.Write("Nuevo dorsal: ");
            int dorsal = int.Parse(Console.ReadLine()!);
            if (dorsal <= 0)
            {
                Console.WriteLine("❌ El dorsal debe ser un número positivo.");
                Console.ReadKey();
                return;
            }

            Console.Write("Nueva posición: ");
            string posicion = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(posicion))
            {
                Console.WriteLine("❌ La posición no puede estar vacía.");
                Console.ReadKey();
                return;
            }

            Console.Write("Nuevo precio: ");
            decimal precio = decimal.Parse(Console.ReadLine()!);
            if (precio < 0 || precio > 99999999.99m)
            {
                Console.WriteLine("❌ El precio debe estar entre 0 y 99.999.999,99");
                Console.ReadKey();
                return;
            }
            Console.Write("Nuevas asistencias: ");
            int asistencias = int.Parse(Console.ReadLine()!);
            if (asistencias < 0)
            {
                Console.WriteLine("❌ Las asistencias no pueden ser negativas.");
                Console.ReadKey();
                return;
            }

            var checkTeam = new MySqlCommand("SELECT COUNT(*) FROM Equipo WHERE id = @id", conn);
            if (Convert.ToInt32(checkTeam.ExecuteScalar()) == 0)
            {
                Console.WriteLine("❌ El equipo no existe.");
                Console.ReadKey();
                return;
            }

            var updateCmd = new MySqlCommand(
                "UPDATE Jugador SET nombre=@nombre, apellido=@apellido, edad=@edad, dorsal=@dorsal, " +
                "posicion=@posicion, precio=@precio, asistencias=@asistencias WHERE id=@id",
                conn
            );
            updateCmd.Parameters.AddWithValue("@nombre", nombre);
            updateCmd.Parameters.AddWithValue("@apellido", apellido);
            updateCmd.Parameters.AddWithValue("@edad", edad);
            updateCmd.Parameters.AddWithValue("@dorsal", dorsal);
            updateCmd.Parameters.AddWithValue("@posicion", posicion);
            updateCmd.Parameters.AddWithValue("@precio", precio);
            updateCmd.Parameters.AddWithValue("@asistencias", asistencias);
            updateCmd.Parameters.AddWithValue("@id", id);

            updateCmd.ExecuteNonQuery();
            Console.WriteLine("✅ Jugador actualizado correctamente.");
            Console.ReadKey();
        }

        public static void DeletePlayer()
        {
            Console.Clear();
            Console.WriteLine("=== 🗑️ Eliminar Jugador ===");
            Console.Write("ID del jugador: ");
            int id = int.Parse(Console.ReadLine()!);

            using var conn = DbHelper.GetConnection();
            conn.Open();

            var deleteCmd = new MySqlCommand("DELETE FROM Jugador WHERE id = @id", conn);
            deleteCmd.Parameters.AddWithValue("@id", id);

            int rows = deleteCmd.ExecuteNonQuery();
            if (rows > 0)
                Console.WriteLine("✅ Jugador eliminado correctamente.");
            else
                Console.WriteLine("❌ Jugador no encontrado.");

            Console.ReadKey();
        }

        public static void MostAssistancePlayer()
        {
            Console.Clear();
            Console.WriteLine("=== 📊 Jugador con Más Asistencias 📊 ===");

            using var conn = DbHelper.GetConnection();
            conn.Open();

            var cmd = new MySqlCommand(
                "SELECT id, nombre, apellido, asistencias FROM Jugador ORDER BY asistencias DESC LIMIT 1",
                conn
            );

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                Console.WriteLine($"ID: {reader["id"]}");
                Console.WriteLine($"Nombre: {reader["nombre"]} {reader["apellido"]}");
                Console.WriteLine($"Asistencias: {reader["asistencias"]}");
            }
            else
            {
                Console.WriteLine("❌ No se encontraron jugadores.");
            }

            Console.ReadKey();
        }
    }
}