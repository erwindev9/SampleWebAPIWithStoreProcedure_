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
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    status = "error",
                    message = "Invalid data",
                    errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }

            try
            {
                
                await product_repository.InsertProduct(product);

                var response = new
                {
                    status = "success",
                    message = "Item successfully created",
                    data = new
                    {
                        id = product.Id,
                        name = product.Name,
                        price = product.Price
                    }
                };

                return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, response);
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, new
                {
                    status = "error",
                    message = "An error occurred while processing your request",
                    details = ex.Message 
                });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            try
            {
                var products = await product_repository.GetAllProducts();

                var response = new
                {
                    status = "success",
                    message = "Items retrieved successfully",
                    data = products.Select(product => new
                    {
                        id = product.Id,
                        name = product.Name,
                        price = product.Price
                    })
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    status = "error",
                    message = "An error occurred while retrieving items",
                    details = ex.Message 
                });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new
                {
                    status = "error",
                    message = "Invalid product ID",
                    data = (object)null
                });
            }

            try
            {
                var product = await product_repository.GetProductById(id);

                if (product == null)
                {
                    return NotFound(new
                    {
                        status = "error",
                        message = "Product not found",
                        data = (object)null
                    });
                }

                var response = new
                {
                    status = "success",
                    message = "Product retrieved successfully",
                    data = new
                    {
                        id = product.Id,
                        name = product.Name,
                        price = product.Price
                    }
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    status = "error",
                    message = "An error occurred while retrieving the product",
                    data = (object)null
                });
            }
        }


        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Product product)
        {
            if (product.Id <= 0)
            {
                return BadRequest(new
                {
                    status = "error",
                    message = "Invalid product ID",
                    data = (object)null
                });
            }

            try
            {
                var existingProduct = await product_repository.GetProductById(product.Id);
                if (existingProduct == null)
                {
                    return NotFound(new
                    {
                        status = "error",
                        message = "Product not found",
                        data = (object)null
                    });
                }

                await product_repository.UpdateProduct(product);

                var response = new
                {
                    status = "success",
                    message = "Product updated successfully",
                    data = new
                    {
                        id = product.Id,
                        name = product.Name,
                        price = product.Price
                    }
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    status = "error",
                    message = "An error occurred while updating the product",
                    data = (object)null
                });
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new
                {
                    status = "error",
                    message = "Invalid product ID",
                    data = (object)null
                });
            }

            try
            {
                var existingProduct = await product_repository.GetProductById(id);
                if (existingProduct == null)
                {
                    return NotFound(new
                    {
                        status = "error",
                        message = "Product not found",
                        data = (object)null
                    });
                }

                await product_repository.DeleteProduct(id);

                var response = new
                {
                    status = "success",
                    message = "Product deleted successfully",
                    data = new
                    {
                        id = existingProduct.Id,
                        name = existingProduct.Name,
                        price = existingProduct.Price
                    }
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    status = "error",
                    message = "An error occurred while deleting the product",
                    data = (object)null
                });
            }
        }

    }
}
