using Models.Constants;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models.Entities
{
    public class Advertisement
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public PropertyType PropertyType { get; set; } // Add -- Front Get Detailed Ad
        public string UserId { get; set; } = string.Empty;
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }
        [NotMapped]
        public Property? Property { get; set; }
        public HouseBase? HouseBase { get; set; }
    }
}
