namespace Models.Entities
{
    public class Report
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;
        public User? Complainer { get; set; }
        public string ComplainerId { get; set; } = string.Empty;
        public User? Complainee { get; set; }
        public string ComplaineeId { get; set; } = string.Empty;
    }
}
