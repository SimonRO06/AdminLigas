using UI;
using Tournament;

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
                    break;
                case "2":
                    TournamentMenu.SearchTournament();
                    break;
                case "3":
                    TournamentMenu.DeleteTournament();
                    break;
                case "4":
                    TournamentMenu.UpdateTournament();
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
                    break;
                case "2":
                    break;
                case "3":
                    break;
                case "4":
                    break;
                case "5":
                    break;
                case "6":
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
                    break;
                case "2":
                    break;
                case "3":
                    break;
                case "4":
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
                    break;
                case "2":
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