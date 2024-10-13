using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleWebAPIWithStoreProcedure_.Interfaces;
using SampleWebAPIWithStoreProcedure_.Models;
using SampleWebAPIWithStoreProcedure_.Response;

namespace SampleWebAPIWithStoreProcedure_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProduct product_repository;

        public ProductsController(IProduct product_repository)
        {
            this.product_repository = product_repository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<IEnumerable<string>>("error", "Invalid data",
                    ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));
            }

            try
            {
                await product_repository.InsertProduct(product);

                var response = new ApiResponse<object>("success", "Item successfully created", new
                {
                    id = product.Id,
                    name = product.Name,
                    price = product.Price
                });

                return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>("error", "An error occurred while processing your request", ex.Message));
            }
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<object>>>> GetAll()
        {
            try
            {
                var products = await product_repository.GetAllProducts();

                var response = new ApiResponse<IEnumerable<object>>("success", "Items retrieved successfully",
                    products.Select(product => new
                    {
                        id = product.Id,
                        name = product.Name,
                        price = product.Price
                    }));

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>("error", "An error occurred while retrieving items", ex.Message));
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<object>("error", "Invalid product ID", null));
            }

            try
            {
                var product = await product_repository.GetProductById(id);

                if (product == null)
                {
                    return NotFound(new ApiResponse<object>("error", "Product not found", null));
                }

                var response = new ApiResponse<object>("success", "Product retrieved successfully", new
                {
                    id = product.Id,
                    name = product.Name,
                    price = product.Price
                });

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>("error", "An error occurred while retrieving the product", ex.Message));
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Product product)
        {
            if (product.Id <= 0)
            {
                return BadRequest(new ApiResponse<object>("error", "Invalid product ID", null));
            }

            try
            {
                var existingProduct = await product_repository.GetProductById(product.Id);
                if (existingProduct == null)
                {
                    return NotFound(new ApiResponse<object>("error", "Product not found", null));
                }

                await product_repository.UpdateProduct(product);

                var response = new ApiResponse<object>("success", "Product updated successfully", new
                {
                    id = product.Id,
                    name = product.Name,
                    price = product.Price
                });

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>("error", "An error occurred while updating the product", ex.Message));
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ApiResponse<object>("error", "Invalid product ID", null));
            }

            try
            {
                var existingProduct = await product_repository.GetProductById(id);
                if (existingProduct == null)
                {
                    return NotFound(new ApiResponse<object>("error", "Product not found", null));
                }

                await product_repository.DeleteProduct(id);

                var response = new ApiResponse<object>("success", "Product deleted successfully", new
                {
                    id = existingProduct.Id,
                    name = existingProduct.Name,
                    price = existingProduct.Price
                });

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>("error", "An error occurred while deleting the product", ex.Message));
            }
        }
    }
}