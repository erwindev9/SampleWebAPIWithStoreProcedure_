using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleWebAPIWithStoreProcedure_.Interfaces;
using SampleWebAPIWithStoreProcedure_.Models;

namespace SampleWebAPIWithStoreProcedure_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProduct product_repository) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            await product_repository.InsertProduct(product);
            return CreatedAtAction(nameof(GetProductById), new {id = product.Id},product);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            return Ok(await  product_repository.GetAllProducts());
        }

        [HttpGet("{id:int}")]
        public async Task<Product>GetProductById(int id)
        {
            return await product_repository.GetProductById(id);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]Product product)
        {
            await product_repository.UpdateProduct(product);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult>Delete(int id)
        {
            await product_repository.DeleteProduct(id);
            return NoContent();
        }
    }
}
