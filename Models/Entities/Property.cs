namespace Models.Entities
{
    public class Property
    {
        public int Id { get; set; }
        public int FloorNumber { get; set; }
        public bool IsFurnished { get; set; }
        public bool IsVitalSite { get; set; }
        public Apartment PropertyFeatures { get; set; }
    }
}
