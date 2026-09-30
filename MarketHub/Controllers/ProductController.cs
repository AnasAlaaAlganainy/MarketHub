using MarketHub.BLL.DTOs.Products;
using Microsoft.AspNetCore.Authorization;

namespace MarketHub.API.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        //=====================================

        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            this._productService = productService;
        }
        //=====================================

        [HttpGet]
        [Route("GetAll")]
        public ActionResult<GenResponse> GetAll()
        {
            GenResponse response = _productService.GetAll();
            if (response.IsSuccess == false)
            {
                return StatusCode(500, response);
            }
            return Ok(response);

        }
        //=====================================

        [HttpGet]
        [Route("GetById/{Id}")]
        public ActionResult<GenResponse> GetById([FromRoute]int Id)
        {
            GenResponse response = _productService.GetById(Id);
            if (response.IsSuccess == false)
            {
                return StatusCode(400, response);
            }
            return Ok(response);
        }

        //=====================================

        [HttpPost("Create")]
        public ActionResult<GenResponse> Create([FromBody] CreateProductDto productDto)
        {
            GenResponse response = _productService.Create(productDto);
            if (response.IsSuccess == false)
            {
                return StatusCode(400, response);
            }
            return Ok(response);
        }
        
        //=====================================

        [HttpPut("Update")]
        public ActionResult<GenResponse> Update([FromBody] UpdateProductDto updtPrdctDto)
        {
            GenResponse response = _productService.Update(updtPrdctDto);

            if (response.IsSuccess == false)
            {
                return StatusCode(400, response);
            }

            return Ok(response);
        }

        //=====================================

        [HttpDelete("Delete/{id:int}")]
        public ActionResult<GenResponse> Delete([FromRoute]int id)
        {
            GenResponse response = _productService.Delete(id);

            if (response.IsSuccess == false)
            {
                return StatusCode(400, response);
            }

            return Ok(response);
        }
    }
}
