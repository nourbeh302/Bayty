namespace Models.Entities
{
    public class HouseBaseImagePath
    {
        public int Id { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public int HouseBaseId { get; set; }
    }
}
