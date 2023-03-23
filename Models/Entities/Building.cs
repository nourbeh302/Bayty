namespace Models.Entities
{
    public class Building
    {
        public int Id { get; set; }
        public bool HasElevator { get; set; }
        public ushort NumberOfFlats { get; set; }
        public ushort NumberOfFloors { get; set; }
        public Apartment PropertyFeatures { get; set; }
    }
}
