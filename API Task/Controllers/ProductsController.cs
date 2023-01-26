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
        /// return the products list
        /// </summary>  
        /// <param></param> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            try
            {
                var products = _service.GetAll();
                
                if(products == null)
                {
                    return NotFound();
                }

                return Ok(products);
            }
            catch(Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
            }
        }


        /// <summary>  
        /// return product by id
        /// </summary>  
        /// <param name="id">id</param> 
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetById(int id)
        {
            try
            {
                if(id < 0)
                {
                    return BadRequest();
                }

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
        /// delete the product of id
        /// </summary>  
        /// <param name="id">id</param>  
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id)
        {
            try
            {
                if(id < 0)
                {
                    return BadRequest();
                }

                var product = _service.GetById(id);

                if (product == null)
                {
                    return NotFound();
                }

                _service.Delete(product);

                return NoContent();
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
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update(int id, [FromBody] Product product)
        {
            try
            {
                if (product == null || id < 0)
                {
                    return BadRequest();
                }

                var oldProduct = _service.GetById(id);

                if (oldProduct == null)
                {
                    return NotFound();
                }

                var original = _service.Copy(oldProduct);

                _service.Update(oldProduct, product);

                var model = new
                {
                    original,
                    after = oldProduct,
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateQuantity(int id, [FromBody] int quantity)
        {
            try
            {
                if (quantity == null || id < 0)
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
        /// create a new product
        /// </summary>  
        /// <param name="product">object product</param> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Add([FromBody] Product product)
        {
            try
            {
                if (product == null)
                {
                    BadRequest();
                }

                _service.Add(product);

                return Created("Add", product);
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
            }
        }

    }
}
