namespace Models.Entities
{
    public class Apartment
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public double Price { get; set; }
        public ushort RoomsCount { get; set; }
        public ushort KitchensCount { get; set; }
        public ushort BathroomsCount { get; set; }
        public List<ApartmentImagePath> ApartmentImagesPaths { get; set; } = new List<ApartmentImagePath>();
    }
}
// Abs Factory + Liskov