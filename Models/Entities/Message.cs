namespace Models.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string SenderId { get; set; } = string.Empty;
        public User? Sender { get; set; }
        public string ReceiverId { get; set; } = string.Empty;
        public User? Receiver { get; set; }
        public string MessageContent { get; set; } = string.Empty;
    }
}
