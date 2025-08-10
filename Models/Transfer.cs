using Option;

namespace Transfer
{
    public class TransferObject
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int FromTeamId { get; set; }
        public int ToTeamId { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public string? Type { get; set; }

        public TransferObject(int id, int playerId, int fromTeamId, int toTeamId, decimal price, DateTime date, string? type)
        {
            Id = id;
            PlayerId = playerId;
            FromTeamId = fromTeamId;
            ToTeamId = toTeamId;
            Price = price;
            Date = date;
            Type = type;
        }
        public TransferObject() { }

        public override string ToString()
        {
            return $"ID: {Id} || JugadorID: {PlayerId} || De: {FromTeamId} -> A: {ToTeamId} || {Type} por ${Price} || Fecha: {Date.ToShortDateString()}";
        }

        public static List<TransferObject> transfers = new();
    }
}