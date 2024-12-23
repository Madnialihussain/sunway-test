using MyHotelApi.Models;

namespace MyHotelApi.Services
{
    public interface IHotelService
    {
        (IEnumerable<Hotel> Hotels, PaginationMetadata PaginationMetadata) GetAllHotels(int pageNumber = 1, int pageSize = 10);
        Hotel? GetHotelById(int id);
    }
}
