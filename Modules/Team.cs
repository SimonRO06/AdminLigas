using MySqlConnector;
using Option;
using Player;
using Shared.Helpers;
using Staff;
using Tournament;

namespace Team
{
    public class TeamObject
    {
        public int Id { get; set; }
        public string? Nombre { get; set; } = string.Empty;
        public string? Tipo { get; set; } = string.Empty;
        public string? Pais { get; set; } = string.Empty;
        public int GolesContra { get; set; } = 0;
        public decimal Dinero { get; set; } = 0;
        public ICollection<PlayerObject>? Jugadores { get; set; } = new HashSet<PlayerObject>();
        public ICollection<TecnicalBodyObject>? CuerposTecnicos { get; set; } = new HashSet<TecnicalBodyObject>();
        public ICollection<MedicalBodyObject>? CuerposMedicos { get; set; } = new HashSet<MedicalBodyObject>();
        public ICollection<TournamentObject>? Torneos { get; set; } = new HashSet<TournamentObject>();
    }

    public class TournamentTeamObject
    {
        public int TorneoId { get; set; }
        public int EquipoId { get; set; }
        public TournamentObject? Torneo { get; set; }
        public TeamObject? Equipo { get; set; }
    }


    public static class TeamMenu
    {
        public static void AddTeam()
        {
            Console.Clear();
            Console.WriteLine("=== 🆕 Registrar Equipo ===");

            Console.Write("Nombre del equipo: ");
            string? nombre = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("❌ El nombre del equipo es obligatorio.");
                Console.ReadKey();
                return;
            }

            Console.Write("Tipo (Club / Selección): ");
            string? tipo = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(tipo) || (tipo != "Club" && tipo != "Selección" && tipo != "Seleccion"))
            {
                Console.WriteLine("❌ El tipo debe ser 'Club' o 'Selección'.");
                Console.ReadKey();
                return;
            }

            Console.Write("País: ");
            string? pais = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(pais))
            {
                Console.WriteLine("❌ El país es obligatorio.");
                Console.ReadKey();
                return;
            }

            Console.Write("Monto de dinero inicial del equipo: ");
            decimal dinero = decimal.Parse(Console.ReadLine()!);
            if (dinero < 0)
            {
                Console.WriteLine("❌ El dinero inicial no puede ser negativo.");
                Console.ReadKey();
                return;
            }

            Console.Write("Goles en contra: ");
            int goles_contra = int.Parse(Console.ReadLine()!);
            if (goles_contra < 0)
            {
                Console.WriteLine("❌ Los goles en contra no pueden ser negativos.");
                Console.ReadKey();
                return;
            }

            using var conn = Shared.Helpers.DbHelper.GetConnection();
            conn.Open();

            var checkCmd = new MySqlCommand("SELECT COUNT(*) FROM Equipo WHERE nombre = @nombre", conn);
            checkCmd.Parameters.AddWithValue("@nombre", nombre);
            var exists = Convert.ToInt32(checkCmd.ExecuteScalar());

            if (exists > 0)
            {
                Console.WriteLine("❌ Este equipo ya existe, ingrese otro...");
                Console.ReadKey();
                return;
            }

            var cmd = new MySqlCommand(
                "INSERT INTO Equipo (nombre, tipo, pais, dinero, goles_contra) VALUES (@nombre, @tipo, @pais, @dinero, @goles_contra); SELECT LAST_INSERT_ID();",
                conn
            );

            cmd.Parameters.AddWithValue("@nombre", nombre);
            cmd.Parameters.AddWithValue("@tipo", tipo);
            cmd.Parameters.AddWithValue("@pais", pais);
            cmd.Parameters.AddWithValue("@dinero", dinero);
            cmd.Parameters.AddWithValue("@goles_contra", goles_contra);

            int newId = Convert.ToInt32(cmd.ExecuteScalar());

            Console.WriteLine($"✅ Equipo registrado correctamente con ID: {newId}");
            Console.ReadKey();
        }

        public static void AddTecnicalBody()
        {
            Console.Clear();
            TeamMenu.ShowTeams();
            Console.WriteLine("\n=== 👔 Registrar Cuerpo Técnico ===");

            Console.Write("Nombre: ");
            string? nombre = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("❌ El nombre es obligatorio.");
                Console.ReadKey();
                return;
            }

            Console.Write("Apellido: ");
            string? apellido = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(apellido))
            {
                Console.WriteLine("❌ El apellido es obligatorio.");
                Console.ReadKey();
                return;
            }

            Console.Write("Edad: ");
            int edad = Convert.ToInt32(Console.ReadLine());
            if (edad <= 0)
            {
                Console.WriteLine("❌ La edad debe ser un número positivo.");
                Console.ReadKey();
                return;
            }

            Console.Write("Cargo (Ej: Director Técnico, Asistente, Preparador físico): ");
            string? cargo = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(cargo))
            {
                Console.WriteLine("❌ El cargo es obligatorio.");
                Console.ReadKey();
                return;
            }

            Console.Write("ID del equipo al que pertenece: ");
            int equipoId = Convert.ToInt32(Console.ReadLine());
            if (equipoId <= 0)
            {
                Console.WriteLine("❌ El ID del equipo debe ser un número positivo.");
                Console.ReadKey();
                return;
            }

            using var conn = Shared.Helpers.DbHelper.GetConnection();
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
                "INSERT INTO CuerpoTecnico (nombre, apellido, edad, cargo, EquipoId) VALUES (@nombre, @apellido, @edad, @cargo, @equipoId)",
                conn
            );
            cmd.Parameters.AddWithValue("@nombre", nombre);
            cmd.Parameters.AddWithValue("@apellido", apellido);
            cmd.Parameters.AddWithValue("@edad", edad);
            cmd.Parameters.AddWithValue("@cargo", cargo);
            cmd.Parameters.AddWithValue("@equipoId", equipoId);

            cmd.ExecuteNonQuery();

            Console.WriteLine("✅ Cuerpo técnico registrado correctamente.");
            Console.ReadKey();
        }

        public static void AddMedicalBody()
        {
            Console.Clear();
            TeamMenu.ShowTeams();
            Console.WriteLine("\n=== 🏥 Registrar Cuerpo Médico ===");

            Console.Write("Nombre: ");
            string? nombre = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("❌ El nombre es obligatorio.");
                Console.ReadKey();
                return;
            }

            Console.Write("Apellido: ");
            string? apellido = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido))
            {
                Console.WriteLine("❌ Nombre y apellido son obligatorios.");
                Console.ReadKey();
                return;
            }

            Console.Write("Edad: ");
            int edad = Convert.ToInt32(Console.ReadLine());
            if (edad <= 0)
            {
                Console.WriteLine("❌ La edad debe ser un número positivo.");
                Console.ReadKey();
                return;
            }

            Console.Write("Especialidad (Ej: Fisioterapeuta, Médico general, Nutricionista): ");
            string? especialidad = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(especialidad))
            {
                Console.WriteLine("❌ La especialidad es obligatoria.");
                Console.ReadKey();
                return;
            }

            Console.Write("ID del equipo al que pertenece: ");
            int equipoId = Convert.ToInt32(Console.ReadLine());
            if (equipoId <= 0)
            {
                Console.WriteLine("❌ El ID del equipo debe ser un número positivo.");
                Console.ReadKey();
                return;
            }

            using var conn = Shared.Helpers.DbHelper.GetConnection();
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
                "INSERT INTO CuerpoMedico (nombre, apellido, edad, especialidad, EquipoId) VALUES (@nombre, @apellido, @edad, @especialidad, @equipoId)",
                conn
            );
            cmd.Parameters.AddWithValue("@nombre", nombre);
            cmd.Parameters.AddWithValue("@apellido", apellido);
            cmd.Parameters.AddWithValue("@edad", edad);
            cmd.Parameters.AddWithValue("@especialidad", especialidad);
            cmd.Parameters.AddWithValue("@equipoId", equipoId);

            cmd.ExecuteNonQuery();

            Console.WriteLine("✅ Cuerpo médico registrado correctamente.");
            Console.ReadKey();
        }

        public static void TeamTournamentInscription()
        {
            Console.Clear();
            Console.WriteLine("=== 🏆 Inscribir Equipo a Torneo ===");

            using var conn = DbHelper.GetConnection();
            conn.Open();

            Console.WriteLine("\n📋 Lista de Torneos:");
            var torneoCmd = new MySqlCommand("SELECT id, nombre FROM Torneo", conn);
            using (var reader = torneoCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader.GetInt32("id")} | Nombre: {reader.GetString("nombre")}");
                }
            }

            Console.Write("\n👉 Ingresa el ID del Torneo: ");
            int torneoId = int.Parse(Console.ReadLine()!);

            Console.WriteLine("\n📋 Lista de Equipos:");
            var equipoCmd = new MySqlCommand("SELECT id, nombre FROM Equipo", conn);
            using (var reader = equipoCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader.GetInt32("id")} | Nombre: {reader.GetString("nombre")}");
                }
            }

            Console.Write("\n👉 Ingresa el ID del Equipo: ");
            int equipoId = int.Parse(Console.ReadLine()!);

            var checkCmd = new MySqlCommand(
                "SELECT COUNT(*) FROM Torneo_Equipo WHERE torneoId = @torneoId AND equipoId = @equipoId",
                conn
            );
            checkCmd.Parameters.AddWithValue("@torneoId", torneoId);
            checkCmd.Parameters.AddWithValue("@equipoId", equipoId);

            int exists = Convert.ToInt32(checkCmd.ExecuteScalar());
            if (exists > 0)
            {
                Console.WriteLine("❌ Este equipo ya está inscrito en este torneo.");
                Console.ReadKey();
                return;
            }

            var insertCmd = new MySqlCommand(
                "INSERT INTO Torneo_Equipo (torneoId, equipoId) VALUES (@torneoId, @equipoId)",
                conn
            );
            insertCmd.Parameters.AddWithValue("@torneoId", torneoId);
            insertCmd.Parameters.AddWithValue("@equipoId", equipoId);

            insertCmd.ExecuteNonQuery();

            Console.WriteLine("✅ Equipo inscrito correctamente en el torneo.");
            Console.ReadKey();
        }

        public static void LeaveTournament()
        {
            Console.Clear();
            Console.WriteLine("=== 🏃‍♂️ Salir de un Torneo ===");

            using var conn = DbHelper.GetConnection();
            conn.Open();

            Console.WriteLine("\n📋 Lista de Torneos:");
            var torneoCmd = new MySqlCommand("SELECT id, nombre FROM Torneo", conn);
            using (var reader = torneoCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader.GetInt32("id")} | Nombre: {reader.GetString("nombre")}");
                }
            }

            Console.Write("\n👉 Ingresa el ID del Torneo: ");
            int torneoId = int.Parse(Console.ReadLine()!);

            Console.WriteLine("\n📋 Lista de Equipos:");
            var equipoCmd = new MySqlCommand("SELECT id, nombre FROM Equipo", conn);
            using (var reader = equipoCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader.GetInt32("id")} | Nombre: {reader.GetString("nombre")}");
                }
            }

            Console.Write("\n👉 Ingresa el ID del Equipo que desea salir: ");
            int equipoId = int.Parse(Console.ReadLine()!);

            var checkCmd = new MySqlCommand(
                "SELECT COUNT(*) FROM Torneo_Equipo WHERE torneoId = @torneoId AND equipoId = @equipoId",
                conn
            );
            checkCmd.Parameters.AddWithValue("@torneoId", torneoId);
            checkCmd.Parameters.AddWithValue("@equipoId", equipoId);

            int exists = Convert.ToInt32(checkCmd.ExecuteScalar());
            if (exists == 0)
            {
                Console.WriteLine("❌ Este equipo no está inscrito en el torneo.");
                Console.ReadKey();
                return;
            }

            var deleteCmd = new MySqlCommand(
                "DELETE FROM Torneo_Equipo WHERE torneoId = @torneoId AND equipoId = @equipoId",
                conn
            );
            deleteCmd.Parameters.AddWithValue("@torneoId", torneoId);
            deleteCmd.Parameters.AddWithValue("@equipoId", equipoId);

            deleteCmd.ExecuteNonQuery();

            Console.WriteLine("✅ Equipo eliminado del torneo correctamente.");
            Console.ReadKey();
        }

        public static void ShowTeams()
        {
            Console.Clear();
            Console.WriteLine("=== 📋 Lista de Equipos ===");

            using var conn = Shared.Helpers.DbHelper.GetConnection();
            conn.Open();

            var cmd = new MySqlCommand("SELECT id, nombre, tipo, pais, dinero, goles_contra FROM Equipo", conn);
            using var reader = cmd.ExecuteReader();

            if (!reader.HasRows)
            {
                Console.WriteLine("❌ No hay equipos registrados.");
            }
            else
            {
                while (reader.Read())
                {
                    Console.WriteLine(
                        $"ID: {reader.GetInt32("id")} | Nombre: {reader.GetString("nombre")} | Tipo: {reader.GetString("tipo")} | País: {reader.GetString("pais")} | Dinero: ${reader.GetDecimal("dinero"):N2} | Goles en Contra: {reader.GetInt32("goles_contra")}"
                    );
                }
            }

            Console.ReadKey();
        }

        public static void MostGoalAgainst()
        {
            Console.Clear();
            Console.WriteLine("=== 🏆 Equipos con Más Goles en Contra ===");

            using var conn = Shared.Helpers.DbHelper.GetConnection();
            conn.Open();

            var cmd = new MySqlCommand(
                "SELECT id, nombre, goles_contra FROM Equipo ORDER BY goles_contra DESC LIMIT 10",
                conn
            );
            using var reader = cmd.ExecuteReader();

            if (!reader.HasRows)
            {
                Console.WriteLine("❌ No hay equipos registrados.");
            }
            else
            {
                while (reader.Read())
                {
                    Console.WriteLine(
                        $"ID: {reader.GetInt32("id")} | Nombre: {reader.GetString("nombre")} | Goles en Contra: {reader.GetInt32("goles_contra")}"
                    );
                }
            }

            Console.ReadKey();
        }

        public static void MostExpensivePlayersByTeam()
        {
            Console.Clear();
            Console.WriteLine("=== 💰 Jugadores Más Caros por Equipo ===");

            using var conn = Shared.Helpers.DbHelper.GetConnection();
            conn.Open();

            var cmd = new MySqlCommand(
                "SELECT e.id AS equipoId, e.nombre AS equipoNombre, j.id AS jugadorId, j.nombre AS jugadorNombre, j.precio " +
                "FROM Equipo e JOIN Jugador j ON e.id = j.equipoId ORDER BY j.precio DESC",
                conn
            );
            using var reader = cmd.ExecuteReader();

            if (!reader.HasRows)
            {
                Console.WriteLine("❌ No hay jugadores registrados.");
            }
            else
            {
                while (reader.Read())
                {
                    Console.WriteLine(
                        $"Equipo ID: {reader.GetInt32("equipoId")} | Equipo: {reader.GetString("equipoNombre")} | Jugador ID: {reader.GetInt32("jugadorId")} | Jugador: {reader.GetString("jugadorNombre")} | Precio: ${reader.GetDecimal("precio"):N2}"
                    );
                }
            }

            Console.ReadKey();
        }
    }
}