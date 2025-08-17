using Option;

namespace Task
{
    public class TaskNotificationObject
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public string? Message { get; set; }
        public DateTime Date { get; set; }
        public bool IsCompleted { get; set; }

        public TaskNotificationObject(int id, int teamId, string? message, DateTime date, bool isCompleted)
        {
            Id = id;
            TeamId = teamId;
            Message = message;
            Date = date;
            IsCompleted = isCompleted;
        }
        public TaskNotificationObject() { }

        public override string ToString()
        {
            return $"ID: {Id} || EquipoID: {TeamId} || Mensaje: {Message} || Fecha: {Date.ToShortDateString()} || Completada: {IsCompleted}";
        }

        public static List<TaskNotificationObject> notifications = new();
    }
}