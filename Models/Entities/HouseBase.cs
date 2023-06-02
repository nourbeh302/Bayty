namespace Models.Entities
{
    public class HouseBase
    {
        public int Id { get; set; }
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public double Price { get; set; }
        public ushort RoomsCount { get; set; }
        public ushort KitchensCount { get; set; }
        public ushort BathroomsCount { get; set; }
        public int AdvertisementId { get; set; }
        public bool IsForRent { get; set; }
        public Advertisement? Advertisement { get; set; }
        public List<HouseBaseImagePath> HouseBaseImagePaths { get; set; } = new List<HouseBaseImagePath>();
    }
}