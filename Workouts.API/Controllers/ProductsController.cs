using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using Workouts.API.Models;
using Workouts.API.Results.Exceptions;

namespace Workouts.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetProducts()
        {
            throw new BusinessException("Server error occured");
        }

        [HttpGet("{productId}")]
        public IActionResult GetProduct(int productId)
        {
            throw new BusinessException("Server error occured", "U dont have right to do that");
        }

        [HttpPost("send")]
        public IActionResult SendProduct([FromBody] Product product)
        {
            string stringProduct = JsonSerializer.Serialize(product);
            byte[] productByteArray = Encoding.UTF8.GetBytes(stringProduct);
            return Ok(productByteArray);
        }
        [HttpPost("send2")]
        public IActionResult SendProduct([FromBody] byte[] product)
        {
            Product productObject = JsonSerializer.Deserialize<Product>(product);
            return Ok(productObject);
        }
    }
}
