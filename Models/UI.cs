namespace UI
{
    public static class Menu
    {
        public static void MainMenu()
        {
            Console.Write("""
            ==================================
            | ⚽ Administrado de Torneos ⚽ |
            ==================================
            | 1) Crear Torneo                |
            | 2) Registro Equipos            |
            | 3) Registro Jugadores          |
            | 4) Transferencias              |
            | 5) Estadisticas                |
            | 6) Salir                       |
            ==================================
            Seleccion: 
            """);
        }

        public static void TournamentMenu()
        {
            Console.Write("""
            ==================================
            | 🏆     Menu de Torneos     🏆 |
            ==================================
            | 1) Añadir Torneo               |
            | 2) Buscar Torneo               |
            | 3) Eliminar Torneo             |
            | 4) Actualizar Torneo           |
            | 5) Regresar al Menu Principal  |
            ==================================
            Seleccion: 
            """);
        }

        public static void TeamMenu()
        {
            Console.Write("""
            ==================================
            | 🤜     Menu de Equipos     🤛 |
            ==================================
            | 1) Registrar Equipos           |
            | 2) Registrar Cuerpo Tecnico    |
            | 3) Registrar Cuerpo Medico     |
            | 4) Inscripcion Torneo          |
            | 5) Notificacion de Tareas      |
            | 6) Salir de Torneo             |
            | 7) Salir al Menu Principal     |
            ==================================
            Seleccion: 
            """);
        }

        public static void PlayerMenu()
        {
            Console.Write("""
            ==================================
            | 👤    Menu de Jugadores    👤 |
            ==================================
            | 1) Registrar Jugador           |
            | 2) Buscar Jugador              |
            | 3) Editar Jugador              |
            | 4) Eliminar Jugador            |
            | 5) Salir al Menu Principal     |
            ==================================
            Seleccion: 
            """);
        }

        public static void TransactionMenu()
        {
            Console.Write("""
            ==================================
            | 💵  Menu de Transacciones  💵 |
            ==================================
            | 1) Comprar Jugador             |
            | 2) Prestar Jugador             |
            | 3) Salir al Menu Principal     |
            ==================================
            Seleccion: 
            """);
        }

        public static void StadisticsMenu()
        {
            Console.Write("""
            ==================================
            | 📈  Menu de Estadisticas   📉 |
            ==================================
            | 1) Jugador Con Mas Asistencias |
            | 2) Equipos Con Mas Goles       |
            |    en Contra                   |
            | 3) Jugadores Mas Caros         |
            |    por Equipo                  |
            | 4) Salir al Menu Principal     |
            ==================================
            Seleccion: 
            """);
        }
    }
}