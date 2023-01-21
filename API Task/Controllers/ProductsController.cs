using API_Task.Models;
using API_Task.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_Task.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductService _service;

        public ProductsController(IProductService serivice)
        {
            _service = serivice;
        }

        /// <summary>  
        /// get all products
        /// </summary>  
        /// <param></param> 
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var products = _service.GetAll();
                return Ok(products);
            }
            catch(Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
            }
        }


        /// <summary>  
        /// get product by id
        /// </summary>  
        /// <param name="id">id</param> 
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var product = _service.GetById(id);

                if (product == null)
                {
                    return NotFound();
                }

                return Ok(product);
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
            }
        }

        /// <summary>  
        /// delete product
        /// </summary>  
        /// <param name="id">id</param>  
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var product = _service.GetById(id);

                if (product == null)
                {
                    return NotFound();
                }

                _service.Delete(product);

                return Ok();
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
            }
        }

        /// <summary>  
        /// update product
        /// </summary>  
        /// <param name="id">id</param>  
        /// <param name="product">object product</param> 
        [HttpPut]
        public IActionResult Update([FromBody] Product product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest();
                }

                var oldProduct = _service.GetById(product.Id);

                if (oldProduct == null)
                {
                    return NotFound();
                }

                var original = _service.Copy(oldProduct);

                _service.Update(oldProduct, product);

                var model = new
                {
                    original,
                    after = product,
                };

                return Ok(model);
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
            }
        }

        /// <summary>  
        /// update product quantity
        /// </summary>  
        /// <param name="id">id</param>  
        /// <param name="quantity">new product quantity</param> 
        [HttpPost("{id}/Quantity")]
        public IActionResult UpdateQuantity(int id, [FromBody] int quantity)
        {
            try
            {
                if (quantity == null)
                {
                    return BadRequest();
                }

                var product = _service.GetById(id);

                if (product == null)
                {
                    return NotFound();
                }

                var original = _service.Copy(product);

                _service.UpdateQuantity(quantity, product);

                var model = new
                {
                    original,
                    after = product,
                };

                return Ok(model);
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
            }
        }

        /// <summary>  
        /// create product
        /// </summary>  
        /// <param name="product">object product</param> 
        [HttpPost]
        public IActionResult Add([FromBody] Product product)
        {
            try
            {
                if (product == null)
                {
                    BadRequest();
                }

                _service.Add(product);

                return Ok(product);
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
            }
        }

    }
}
