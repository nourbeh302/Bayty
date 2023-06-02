namespace Models.Entities
{
    public class Apartment : Property
    {
        public int FloorNumber { get; set; }
        public bool IsFurnished { get; set; }
        public bool IsVitalSite { get; set; }
        public bool HasElevator { get; set; }
    }
}
