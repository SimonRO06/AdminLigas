using MySqlConnector;
using Team;

namespace Transfer
{
    public class TransferObject
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public Player.PlayerObject? Player { get; set; }
        public int FromTeamId { get; set; }
        public TeamObject? FromTeam { get; set; }
        public int ToTeamId { get; set; }
        public TeamObject? ToTeam { get; set; }
        public decimal Amount { get; set; } = 0;
        public string? Type { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;
    }

    public static class TransferMenu
    {
        public static void BuyPlayer()
        {
            Console.Clear();
            Console.WriteLine("=== 💰 Comprar Jugador ===");

            Console.Write("ID del jugador a comprar: ");
            int playerId = int.Parse(Console.ReadLine()!);
            if (playerId <= 0)
            {
                Console.WriteLine("❌ El ID del jugador debe ser un número positivo.");
                Console.ReadKey();
                return;
            }

            Console.Write("ID del equipo comprador: ");
            int toTeamId = int.Parse(Console.ReadLine()!);
            if (toTeamId <= 0)
            {
                Console.WriteLine("❌ El ID del equipo debe ser un número positivo.");
                Console.ReadKey();
                return;
            }

            using var conn = Shared.Helpers.DbHelper.GetConnection();
            conn.Open();

            var playerCmd = new MySqlCommand("SELECT id, nombre, precio, equipoId FROM Jugador WHERE id = @id", conn);
            playerCmd.Parameters.AddWithValue("@id", playerId);
            using var reader = playerCmd.ExecuteReader();

            if (!reader.Read())
            {
                Console.WriteLine("❌ Jugador no encontrado.");
                Console.ReadKey();
                return;
            }

            decimal price = reader.GetDecimal("precio");
            if (price <= 0)
            {
                Console.WriteLine("❌ El jugador no está disponible para compra.");
                Console.ReadKey();
                return;
            }
            int fromTeamId = reader.GetInt32("equipoId");
            reader.Close();

            var moneyCmd = new MySqlCommand("SELECT dinero FROM Equipo WHERE id = @id", conn);
            moneyCmd.Parameters.AddWithValue("@id", toTeamId);
            decimal teamMoney = Convert.ToDecimal(moneyCmd.ExecuteScalar());

            if (teamMoney < price)
            {
                Console.WriteLine("❌ El equipo no tiene suficiente dinero.");
                Console.ReadKey();
                return;
            }

            using var transaction = conn.BeginTransaction();
            try
            {
                var updateToTeam = new MySqlCommand("UPDATE Equipo SET dinero = dinero - @price WHERE id = @id", conn, transaction);
                updateToTeam.Parameters.AddWithValue("@price", price);
                updateToTeam.Parameters.AddWithValue("@id", toTeamId);
                updateToTeam.ExecuteNonQuery();

                var updateFromTeam = new MySqlCommand("UPDATE Equipo SET dinero = dinero + @price WHERE id = @id", conn, transaction);
                updateFromTeam.Parameters.AddWithValue("@price", price);
                updateFromTeam.Parameters.AddWithValue("@id", fromTeamId);
                updateFromTeam.ExecuteNonQuery();

                var updatePlayer = new MySqlCommand("UPDATE Jugador SET equipoId = @toTeamId WHERE id = @playerId", conn, transaction);
                updatePlayer.Parameters.AddWithValue("@toTeamId", toTeamId);
                updatePlayer.Parameters.AddWithValue("@playerId", playerId);
                updatePlayer.ExecuteNonQuery();

                var insertTransfer = new MySqlCommand(
                    "INSERT INTO Transferencia (playerId, fromTeamId, toTeamId, amount, type) VALUES (@playerId, @fromTeamId, @toTeamId, @amount, 'Compra')",
                    conn, transaction
                );
                insertTransfer.Parameters.AddWithValue("@playerId", playerId);
                insertTransfer.Parameters.AddWithValue("@fromTeamId", fromTeamId);
                insertTransfer.Parameters.AddWithValue("@toTeamId", toTeamId);
                insertTransfer.Parameters.AddWithValue("@amount", price);
                insertTransfer.ExecuteNonQuery();

                transaction.Commit();
                Console.WriteLine("✅ Compra realizada correctamente.");
            }
            catch
            {
                transaction.Rollback();
                Console.WriteLine("❌ Error en la transacción.");
            }

            Console.ReadKey();
        }

        public static void LoanPlayer()
        {
            Console.Clear();
            Console.WriteLine("=== 🔄 Prestar Jugador ===");

            Console.Write("ID del jugador a prestar: ");
            if (!int.TryParse(Console.ReadLine(), out int playerId))
            {
                Console.WriteLine("❌ ID inválido. Debe ser un número.");
                Console.ReadKey();
                return;
            }

            Console.Write("ID del equipo receptor: ");
            if (!int.TryParse(Console.ReadLine(), out int toTeamId))
            {
                Console.WriteLine("❌ ID inválido. Debe ser un número.");
                Console.ReadKey();
                return;
            }

            using var conn = Shared.Helpers.DbHelper.GetConnection();
            conn.Open();

            var playerCmd = new MySqlCommand("SELECT id, equipoId FROM Jugador WHERE id = @id", conn);
            playerCmd.Parameters.AddWithValue("@id", playerId);
            using var reader = playerCmd.ExecuteReader();

            if (!reader.Read())
            {
                Console.WriteLine("❌ Jugador no encontrado.");
                Console.ReadKey();
                return;
            }

            int fromTeamId = reader.GetInt32("equipoId");
            if (fromTeamId == toTeamId)
            {
                Console.WriteLine("❌ El jugador ya pertenece a este equipo.");
                Console.ReadKey();
                return;
            }
            reader.Close();

            using var transaction = conn.BeginTransaction();
            try
            {
                var updatePlayer = new MySqlCommand("UPDATE Jugador SET equipoId = @toTeamId WHERE id = @playerId", conn, transaction);
                updatePlayer.Parameters.AddWithValue("@toTeamId", toTeamId);
                updatePlayer.Parameters.AddWithValue("@playerId", playerId);
                updatePlayer.ExecuteNonQuery();

                var insertTransfer = new MySqlCommand(
                    "INSERT INTO Transferencia (playerId, fromTeamId, toTeamId, amount, type) VALUES (@playerId, @fromTeamId, @toTeamId, 0, 'Préstamo')",
                    conn, transaction
                );
                insertTransfer.Parameters.AddWithValue("@playerId", playerId);
                insertTransfer.Parameters.AddWithValue("@fromTeamId", fromTeamId);
                insertTransfer.Parameters.AddWithValue("@toTeamId", toTeamId);
                insertTransfer.ExecuteNonQuery();

                transaction.Commit();
                Console.WriteLine("✅ Préstamo realizado correctamente.");
            }
            catch
            {
                transaction.Rollback();
                Console.WriteLine("❌ Error en la transacción.");
            }

            Console.ReadKey();
        }
    }
}
