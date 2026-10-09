using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static readonly List<Models.Product> _products = new()
        {
            new(1, "Keyboard", 1499m),
            new(2, "Mouse", 799m),
            new(3, "Monitor", 10999m),
            new(4, "USB Drive", 134.75m)
        };

        private readonly IConfiguration _config;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IConfiguration config, ILogger<ProductsController> logger)
        {
            _config = config;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Models.Product>> GetAll()
        {
            _logger.LogInformation("Fetching all products");
            return Ok(_products);
        }

        [HttpGet("{id:int}")]
        public ActionResult<Models.Product> GetById(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            return product is null ? NotFound() : Ok(product);
        }

        [HttpGet("info")]
        public IActionResult Info() => Ok(new
        {
            Environment = _config["ASPNETCORE_ENVIRONMENT"],
            Message = _config["App:WelcomeMessage"] ?? "default message"
        });
    }
}
