using Microsoft.AspNetCore.Mvc;
using MyHotelApi.Models;
using MyHotelApi.Services;
using System.Text.Json;

namespace MyHotelApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelService _hotelService;
        private readonly ILogger<HotelsController> _logger;

        public HotelsController(IHotelService hotelService, ILogger<HotelsController> logger)
        {
            _hotelService = hotelService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Hotel>> Get([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                if (pageSize > 50)
                {
                    pageSize = 50;
                }

                var (hotels, paginationMetadata) = _hotelService.GetAllHotels(pageNumber, pageSize);

                Response.Headers.Add("X-Pagination", 
                    JsonSerializer.Serialize(paginationMetadata));

                return Ok(hotels);
            }
            catch (FileNotFoundException ex)
            {
                _logger.LogError(ex, "Hotels data file is missing");
                return StatusCode(500, new ProblemDetails
                { 
                    Status = 500,
                    Title = "Configuration Error",
                    Detail = "The hotels data file is missing. Please contact the system administrator.",
                    Type = "https://datastore.example/errors/missing-file"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving hotels");
                return StatusCode(500, new ProblemDetails
                { 
                    Status = 500,
                    Title = "Internal Server Error",
                    Detail = "An unexpected error occurred while retrieving hotels.",
                    Type = "https://datastore.example/errors/internal-error"
                });
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Hotel> Get(int id)
        {
            try
            {
                var hotel = _hotelService.GetHotelById(id);
                if (hotel == null)
                {
                    return NotFound(new ProblemDetails
                    {
                        Status = 404,
                        Title = "Not Found",
                        Detail = $"Hotel with ID {id} not found.",
                        Type = "https://datastore.example/errors/not-found",
                        Instance = HttpContext.Request.Path
                    });
                }
                return Ok(hotel);
            }
            catch (FileNotFoundException ex)
            {
                _logger.LogError(ex, "Hotels data file is missing");
                return StatusCode(500, new ProblemDetails
                { 
                    Status = 500,
                    Title = "Configuration Error",
                    Detail = "The hotels data file is missing. Please contact the system administrator.",
                    Type = "https://datastore.example/errors/missing-file",
                    Instance = HttpContext.Request.Path
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving hotel");
                return StatusCode(500, new ProblemDetails
                { 
                    Status = 500,
                    Title = "Internal Server Error",
                    Detail = "An unexpected error occurred while retrieving the hotel.",
                    Type = "https://datastore.example/errors/internal-error",
                    Instance = HttpContext.Request.Path
                });
            }
        }
    }
}
