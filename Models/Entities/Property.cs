namespace Models.Entities
{
    public abstract class Property
    {
        public int Id { get; set; }
        public int HouseBaseId { get; set; }
        public HouseBase? HouseBase { get; set; }
    }
}
