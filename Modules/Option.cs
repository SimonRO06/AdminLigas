using UI;
using Tournament;
using Team;
using Player;
using Transfer;

namespace Option
{
    public static class MenuOption
    {
        public static void MainMenuOptions()
        {
            Console.Clear();
            Menu.MainMenu();
            string? seleccion = Console.ReadLine();
            switch (seleccion)
            {
                case "1":
                    MenuOption.TournamentMenuOptions();
                    break;
                case "2":
                    MenuOption.TeamMenuOptions();
                    break;
                case "3":
                    MenuOption.PlayerMenuOptions();
                    break;
                case "4":
                    MenuOption.TransactionMenuOptions();
                    break;
                case "5":
                    MenuOption.StadisticsMenuOptions();
                    break;
                case "6":
                    Console.Clear();
                    Console.WriteLine("Gracias por usar el programa...");
                    break;
                default:
                    Console.WriteLine("¡Seleccione una opcion valida!");
                    Console.ReadKey();
                    MenuOption.MainMenuOptions();
                    break;
            }
        }

        public static void TournamentMenuOptions()
        {
            Console.Clear();
            Menu.TournamentMenu();
            string? seleccion = Console.ReadLine();
            switch (seleccion)
            {
                case "1":
                    TournamentMenu.AddTournament();
                    MenuOption.TournamentMenuOptions();
                    break;
                case "2":
                    TournamentMenu.SearchTournament();
                    MenuOption.TournamentMenuOptions();
                    break;
                case "3":
                    TournamentMenu.DeleteTournament();
                    MenuOption.TournamentMenuOptions();
                    break;
                case "4":
                    TournamentMenu.UpdateTournament();
                    MenuOption.TournamentMenuOptions();
                    break;
                case "5":
                    MenuOption.MainMenuOptions();
                    break;
                default:
                    Console.WriteLine("¡Seleccione una opcion valida!");
                    Console.ReadKey();
                    MenuOption.TournamentMenuOptions();
                    break;
            }
        }

        public static void TeamMenuOptions()
        {
            Console.Clear();
            Menu.TeamMenu();
            string? seleccion = Console.ReadLine();
            switch (seleccion)
            {
                case "1":
                    TeamMenu.AddTeam();
                    MenuOption.TeamMenuOptions();
                    break;
                case "2":
                    TeamMenu.AddTecnicalBody();
                    MenuOption.TeamMenuOptions();
                    break;
                case "3":
                    TeamMenu.AddMedicalBody();
                    MenuOption.TeamMenuOptions();
                    break;
                case "4":
                    TeamMenu.TeamTournamentInscription();
                    MenuOption.TeamMenuOptions();
                    break;
                case "5":
                    break;
                case "6":
                    TeamMenu.LeaveTournament();
                    MenuOption.TeamMenuOptions();
                    break;
                case "7":
                    MenuOption.MainMenuOptions();
                    break;
                default:
                    Console.WriteLine("¡Seleccione una opcion valida!");
                    Console.ReadKey();
                    MenuOption.TeamMenuOptions();
                    break;
            }
        }

        public static void PlayerMenuOptions()
        {
            Console.Clear();
            Menu.PlayerMenu();
            string? seleccion = Console.ReadLine();
            switch (seleccion)
            {
                case "1":
                    PlayerMenu.AddPlayer();
                    MenuOption.PlayerMenuOptions();
                    break;
                case "2":
                    PlayerMenu.SearchPlayer();
                    MenuOption.PlayerMenuOptions();
                    break;
                case "3":
                    PlayerMenu.UpdatePlayer();
                    MenuOption.PlayerMenuOptions();
                    break;
                case "4":
                    PlayerMenu.DeletePlayer();
                    MenuOption.PlayerMenuOptions();
                    break;
                case "5":
                    MenuOption.MainMenuOptions();
                    break;
                default:
                    Console.WriteLine("¡Seleccione una opcion valida!");
                    Console.ReadKey();
                    MenuOption.PlayerMenuOptions();
                    break;
            }
        }

        public static void TransactionMenuOptions()
        {
            Console.Clear();
            Menu.TransactionMenu();
            string? seleccion = Console.ReadLine();
            switch (seleccion)
            {
                case "1":
                    TransferMenu.BuyPlayer();
                    MenuOption.TransactionMenuOptions();
                    break;
                case "2":
                    TransferMenu.LoanPlayer();
                    MenuOption.TransactionMenuOptions();
                    break;
                case "3":
                    MenuOption.MainMenuOptions();
                    break;
                default:
                    Console.WriteLine("¡Seleccione una opcion valida!");
                    Console.ReadKey();
                    MenuOption.TransactionMenuOptions();
                    break;
            }
        }

        public static void StadisticsMenuOptions()
        {
            Console.Clear();
            Menu.StadisticsMenu();
            string? seleccion = Console.ReadLine();
            switch (seleccion)
            {
                case "1":
                    break;
                case "2":
                    break;
                case "3":
                    break;
                case "4":
                    MenuOption.MainMenuOptions();
                    break;
                default:
                    Console.WriteLine("¡Seleccione una opcion valida!");
                    Console.ReadKey();
                    MenuOption.StadisticsMenuOptions();
                    break;
            }
        }
    }
}