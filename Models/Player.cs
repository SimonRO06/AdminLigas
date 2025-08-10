using Option;

namespace Player
{
    public class PlayerObject
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public int Age { get; set; }
        public string? Position { get; set; }
        public decimal MarketValue { get; set; }
        public int TeamId { get; set; }

        public PlayerObject(int id, string? fullName, int age, string? position, decimal marketValue, int teamId)
        {
            Id = id;
            FullName = fullName;
            Age = age;
            Position = position;
            MarketValue = marketValue;
            TeamId = teamId;
        }
        public PlayerObject() { }

        public override string ToString()
        {
            return $"ID: {Id} || Nombre: {FullName} || Posición: {Position} || Valor: ${MarketValue} || EquipoID: {TeamId}";
        }

        public static List<PlayerObject> players = new();
    }
}