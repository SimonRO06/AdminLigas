using System.Data;
using System.Data.Common;
using System.Runtime.InteropServices;
using Option;
using UI;

namespace Tournament
{
    public class TournamentObject
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime CreationDate { get; set; }
        public TournamentObject(int id, string? name, DateTime creationDate)
        {
            Id = id;
            Name = name;
            CreationDate = creationDate;
        }
        public TournamentObject() { }
        public override string ToString()
        {
            return $"ID: {Id} || Fecha de Creacion: {CreationDate} || Torneo: {Name}";
        }
        public static List<TournamentObject> tournaments = new List<TournamentObject>();
    }

    public static class TournamentMenu
    {
        public static void AddTournament()
        {

            int id = 0;
            string? name = "";
            DateTime creationDate = DateTime.Now;

            while (true)
            {
                Console.Clear();
                Console.Write("""
                === Ingrese el ID del nuevo torneo ===
                -> 
                """);
                id = Convert.ToInt32(Console.ReadLine());
                if (TournamentObject.tournaments.Any(t => t.Id == id))
                {
                    Console.WriteLine("Este ID ya existe, debe ingresar otro...");
                    while (true)
                    {
                        Console.WriteLine("¿Deseas volver al menu principal? (s/n): ");
                        string? seleccion = Console.ReadLine();
                        if (seleccion == "s")
                        {
                            Console.WriteLine("Volviendo al menu principal...");
                            Console.ReadKey();
                            MenuOption.MainMenuOptions();
                            // break;
                        }
                        if (seleccion == "n")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Escoge una de las opciones...");
                            Console.ReadKey();
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            while (true)
            {
                Console.Clear();
                Console.Write("""
                === Ingrese el nombre del nuevo torneo ===
                -> 
                """);
                name = Console.ReadLine();
                if (TournamentObject.tournaments.Any(t => t.Name == name))
                {
                    Console.WriteLine("Este nombre ya existe, ingrese otro...");
                    while (true)
                    {
                        Console.WriteLine("¿Deseas volver al menu principal? (s/n): ");
                        string? seleccion = Console.ReadLine();
                        if (seleccion == "s")
                        {
                            Console.WriteLine("Volviendo al menu principal...");
                            Console.ReadKey();
                            MenuOption.MainMenuOptions();
                            break;
                        }
                        if (seleccion == "n")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Escoge una de las opciones...");
                            Console.ReadKey();
                        }
                    }
                }
                else
                {
                    TournamentObject newTournament = new TournamentObject(id, name, creationDate);
                    TournamentObject.tournaments.Add(newTournament);
                    Console.WriteLine("Torneo añadido correctamente!");
                    Console.ReadKey();
                    Console.Clear();
                    Console.WriteLine("====== 🏆 Torneos Creados 🏆 ======");
                    foreach (TournamentObject tournament in TournamentObject.tournaments)
                    {
                        Console.WriteLine(tournament.ToString());
                    }
                    Console.WriteLine("====================================");
                    Console.ReadKey();
                    MenuOption.TournamentMenuOptions();
                    break;
                }
            }
        }

        public static void SearchTournament()
        {
            Console.Clear();
            Console.Write("""
            === 🔍 Buscar Torneo por ID 🔍 ===
            -> 
            """);
            int id = Convert.ToInt32(Console.ReadLine());
            TournamentObject? findTournament = TournamentObject.tournaments.Find(t => t.Id == id);
            if (findTournament != null)
            {
                Console.Clear();
                Console.WriteLine("=== ⭐ ¡Torneo Encontrado! ⭐ ===");
                Console.WriteLine(findTournament.ToString());
                Console.ReadKey();
                MenuOption.TournamentMenuOptions();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("=== ❌ ¡Torneo No Encontrado! ❌ ===");
                Console.ReadKey();
                MenuOption.TournamentMenuOptions();
            }
        }

        public static void DeleteTournament()
        {
            Console.Clear();
            Console.WriteLine("=== 📝 Torneos Registrados 📝 ===");
            foreach (TournamentObject tournament in TournamentObject.tournaments)
            {
                Console.WriteLine(tournament.ToString());
            }
            Console.Write("""
            === Eliminar Torneo por ID ===
            -> 
            """);
            int id = Convert.ToInt32(Console.ReadLine());
            TournamentObject? findTournament = TournamentObject.tournaments.Find(t => t.Id == id);
            if (findTournament != null)
            {
                Console.Clear();
                Console.WriteLine("=== 🗑️ ¡Torneo Eliminado! 🗑️ ===");
                TournamentObject.tournaments.Remove(findTournament);
                Console.ReadKey();
                MenuOption.TournamentMenuOptions();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("=== ❌ ¡Torneo No Existente! ❌ ===");
                Console.ReadKey();
                MenuOption.TournamentMenuOptions();
            }
        }

        public static void UpdateTournament()
        {
            Console.Clear();
            Console.WriteLine("=== 📝 Torneos Registrados 📝 ===");
            foreach (TournamentObject tournament in TournamentObject.tournaments)
            {
                Console.WriteLine(tournament.ToString());
            }
            int id = Convert.ToInt32(Console.ReadLine());
            TournamentObject? findTournament = TournamentObject.tournaments.Find(t => t.Id == id);
            if (findTournament != null)
            {
                Console.Clear();
                Console.Write("""
                === Ingrese el nuevo nombre ===
                -> 
                """);
                string? name = Console.ReadLine();
                if (TournamentObject.tournaments.Any(t => t.Name == name))
                {
                    Console.WriteLine("¡Este nombre ya esta en uso!");
                    Console.ReadKey();
                    MenuOption.TournamentMenuOptions();
                }
                else
                {
                    Console.Clear();
                    findTournament.Name = name;
                    Console.WriteLine("""
                    === 🔄 Torneo Actualizado Correctamente 🔄 ===
                    """);
                    Console.WriteLine(findTournament.ToString());
                    Console.ReadKey();
                    MenuOption.TournamentMenuOptions();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("=== ❌ ¡Torneo No Existente! ❌ ===");
                Console.ReadKey();
                MenuOption.TournamentMenuOptions();
            }
        }
    }
}