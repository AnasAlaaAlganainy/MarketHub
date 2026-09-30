using Azure;
using System.Diagnostics.Contracts;

namespace MarketHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        //=====================================

        [HttpGet]
        [Route("Get/{Id}")]
        public ActionResult<GenResponse> GetById(int Id)
        {
            GenResponse response = _categoryService.GetById(Id);

            if(response.IsSuccess == false)
                return StatusCode(400,"This Category Is not Founded");

            return Ok(response);
        }

        //=====================================

        [HttpGet("GetAll")]
        public ActionResult<GenResponse> GetAll()
        {
            GenResponse response = _categoryService.GetAll();

            if (response.IsSuccess == false)
                return StatusCode(500, response);

            return Ok(response);
        }

        //=====================================

        [HttpPost("Create")]
        public ActionResult<GenResponse> Create([FromBody] CreateCategoryDTO categoryDTO)
        {
            GenResponse response = _categoryService.Create(categoryDTO);
            if (response.IsSuccess == false)
                return BadRequest(response);

            return Ok(response);            
        }

        //=====================================

        [HttpPut]
        [Route("Update")]
        public ActionResult<GenResponse> Update([FromBody] UpdateCategoryDTO updateCategoryDTO)
        {
            GenResponse response = _categoryService.Update(updateCategoryDTO);
            
            if (response.IsSuccess == false)
                return BadRequest(response);

            return Ok(response);
        }

        //=====================================

        [HttpDelete]
        [Route("Delete/{id}")]
        public ActionResult<GenResponse> Delete([FromRoute]int id)
        {
            GenResponse response = _categoryService.Delete(id);

            if (response.IsSuccess == false)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
