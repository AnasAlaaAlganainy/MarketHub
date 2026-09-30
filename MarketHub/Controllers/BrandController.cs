using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;
        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpPost]
        [Route("Create")]
        public ActionResult<GenResponse> Create([FromBody] CreateBrandDTO brandDTO)
        {
            GenResponse response = _brandService.Create(brandDTO);
            
            if (response.IsSuccess == false)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("GetById/{id:int}")]
        public ActionResult<GenResponse> GetById(int id)
        {
            GenResponse response = _brandService.GetById(id);

            if(response.IsSuccess == false)
                return BadRequest(response);

            return Ok(response);
        }
        [HttpGet("GetAll")]
        public ActionResult<GenResponse> GetAll()
        {
            
            GenResponse response = _brandService.GetAll();

            if(response.IsSuccess == false )
                return StatusCode(500, response);

            return Ok(response);
        }

        [HttpPut]
        [Route("Update")]
        public ActionResult<GenResponse> Update([FromBody]UpdateBrandDTO brandDTO)
        {
            GenResponse response = _brandService.Update(brandDTO);

            if (response.IsSuccess == false)
                return StatusCode(400, response);

            return Ok(response);
        }

        [HttpDelete("DeleteById/{id:int}")]
        public ActionResult<GenResponse> DeleteById(int id)
        {
            GenResponse response = _brandService.DeleteById(id);
            
            if (response.IsSuccess == false) 
                return StatusCode(400, response);

            return Ok(response);
        } 
    }
}
