using System.Text.Json.Serialization;

namespace MyHotelApi.Models
{
    public class Hotel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("location")]
        public string Location { get; set; } = string.Empty;

        [JsonPropertyName("rating")]
        public float Rating { get; set; }

        [JsonPropertyName("imageUrl")]
        public string ImageUrl { get; set; } = string.Empty;

        [JsonPropertyName("datesOfTravel")]
        public string[] DatesOfTravel { get; set; } = Array.Empty<string>();

        [JsonPropertyName("boardBasis")]
        public string BoardBasis { get; set; } = string.Empty;

        [JsonPropertyName("rooms")]
        public Room[] Rooms { get; set; } = Array.Empty<Room>();
    }
}
