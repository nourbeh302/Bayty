namespace Models.Entities
{
    public class Villa
    {
        public int Id { get; set; }
        public bool HasSwimmingPool { get; set; }
        public Apartment VillaFeatures { get; set; }
    }
}
