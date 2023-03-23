using Models.Constants;

namespace Models.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public double RentalCost { get; set; }
        public DateTime Date { get; set; }
        public TransactionState State { get; set; }
        public string InitiatorId { get; set; }
        public User Initiator { get; set; }
        public string ReceiverId { get; set; }
        public User Receiver { get; set; }

    }
}
