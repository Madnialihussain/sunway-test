using System.Text.Json;
using MyHotelApi.Models;
using Microsoft.Extensions.Logging;

namespace MyHotelApi.Services
{
    public class HotelService : IHotelService
    {
        private readonly List<Hotel> _hotels;
        private readonly ILogger<HotelService> _logger;

        public HotelService(ILogger<HotelService> logger)
        {
            _logger = logger;
            _hotels = LoadHotels();
        }

        private List<Hotel> LoadHotels()
        {
            var jsonPath = Path.Combine(AppContext.BaseDirectory, "hotels.json");
            if (!File.Exists(jsonPath))
            {
                _logger.LogError($"File not found: {jsonPath}");
                return new List<Hotel>();
            }

            try
            {
                var jsonString = File.ReadAllText(jsonPath);
                return JsonSerializer.Deserialize<List<Hotel>>(jsonString) ?? new List<Hotel>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reading hotels.json file");
                return new List<Hotel>();
            }
        }

        public (IEnumerable<Hotel> Hotels, PaginationMetadata PaginationMetadata) GetAllHotels(int pageNumber = 1, int pageSize = 10)
        {
            var jsonPath = Path.Combine(AppContext.BaseDirectory, "hotels.json");
            if (!File.Exists(jsonPath))
            {
                throw new FileNotFoundException($"Hotels data file is missing: {jsonPath}");
            }

            var totalCount = _hotels.Count;
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            pageNumber = Math.Max(1, Math.Min(pageNumber, totalPages));

            var paginationMetadata = new PaginationMetadata
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages
            };

            var hotels = _hotels
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return (hotels, paginationMetadata);
        }

        public Hotel? GetHotelById(int id)
        {
            var jsonPath = Path.Combine(AppContext.BaseDirectory, "hotels.json");
            if (!File.Exists(jsonPath))
            {
                throw new FileNotFoundException($"Hotels data file is missing: {jsonPath}");
            }
            return _hotels.FirstOrDefault(h => h.Id == id);
        }
    }
}
