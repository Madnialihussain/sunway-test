using System.Text.Json.Serialization;

namespace MyHotelApi.Models
{
    public class Room
    {
        [JsonPropertyName("roomType")]
        public string RoomType { get; set; } = string.Empty;
        
        [JsonPropertyName("amount")]
        public int Amount { get; set; }
    }
}
