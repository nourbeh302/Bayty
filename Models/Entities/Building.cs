namespace Models.Entities
{
    public class Building : Property
    {
        public bool HasElevator { get; set; }
        public ushort NumberOfFlats { get; set; }
        public ushort NumberOfFloors { get; set; }
    }
}
