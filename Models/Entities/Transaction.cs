using Models.Constants;

namespace Models.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public double RentalCost { get; set; }
        public DateTime Date { get; set; }
        public TransactionState State { get; set; }
        public string InitiatorId { get; set; } = string.Empty;
        public User? Initiator { get; set; }
        public string ReceiverId { get; set; } = string.Empty;
        public User? Receiver { get; set; }

    }
}
