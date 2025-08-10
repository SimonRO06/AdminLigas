using Option;
using Player;
using Staff;

namespace Team
{
    public class TeamObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public List<PlayerObject> Players { get; set; } = new();
        public List<StaffObject> Staff { get; set; } = new();

        public TeamObject(int id, string name, string city)
        {
            Id = id;
            Name = name;
            City = city;
        }
        public TeamObject() { }

        public TeamObject(int id, string? name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return $"ID: {Id} || Equipo: {Name} || Ciudad: {City} || Jugadores: {Players.Count}";
        }

        public static List<TeamObject> teams = new();
    }
}