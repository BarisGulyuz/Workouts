using Microsoft.AspNetCore.Mvc;
using Workouts.API.DatabaseOperations;
using Workouts.API.Models;
using Workouts.API.Results.Response;

namespace Workouts.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly WorkoutContext dbContext;

        public ProductsController(WorkoutContext context)
        {
            dbContext = context;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = dbContext.Products.ToList();

            //throw new BusinessException("Server error occured");
            return Ok(Results.Response.Response<List<Product>>.CreateSuccessResponse(products, ""));
        }

        //[HttpGet("{productId}")]
        //public IActionResult GetProduct(int productId)
        //{
        //    throw new BusinessException("Server error occured");
        //}

        //[HttpPost("send")]
        //public IActionResult SerializeProduct()
        //{
        //    var stackTrace = new StackTrace();

        //    string stringProduct = JsonSerializer.Serialize(new Product());
        //    byte[] productByteArray = Encoding.UTF8.GetBytes(stringProduct);

        //    stackTrace.GetFrames();
        //    return Ok(productByteArray);
        //}
        //[HttpPost("send2")]
        //public IActionResult DeserializeProducts([FromBody] byte[] product)
        //{
        //    Product productObject = JsonSerializer.Deserialize<Product>(product);
        //    return Ok(productObject);
        //}

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int productId)
        {
            Product product = dbContext.Products.FirstOrDefault(p => p.Id == productId);

            if (product is not null)
            {
                dbContext.Products.Remove(product);
                await dbContext.SaveChangesAsync();
                return NoContent();
            }

            return BadRequest(new Response("Not Found", ResultType.Error));
        }
    }
}
