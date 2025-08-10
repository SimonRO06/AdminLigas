using Option;

namespace Staff
{
    public class StaffObject
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Role { get; set; }
        public int TeamId { get; set; }

        public StaffObject(int id, string? fullName, string? role, int teamId)
        {
            Id = id;
            FullName = fullName;
            Role = role;
            TeamId = teamId;
        }
        public StaffObject() { }

        public override string ToString()
        {
            return $"ID: {Id} || Nombre: {FullName} || Rol: {Role} || EquipoID: {TeamId}";
        }

        public static List<StaffObject> staffList = new();
    }
}