using MarketHub.BLL.DTOs.Cart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MarketHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        //*********************************************************
        //=========================================================
        //*********************************************************

        [HttpGet]
        [Route("GetCart")]
        public ActionResult<GenResponse> GetCart()
        {
            GenResponse response = new GenResponse();
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            response = _cartService.GetCart(userId);

            if (response.IsSuccess == false)
                return StatusCode(400, response);

            return StatusCode(200, response);
        }


        //*********************************************************
        //=========================================================
        //*********************************************************


        [HttpPost("AddCartItem")]
        public ActionResult<GenResponse> AddCartItem( [FromBody] AddCartItemDTO addCartItemDTO) 
        {   
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            GenResponse response = _cartService.AddCartItem(addCartItemDTO, userId);
            
            if (!response.IsSuccess)
                return StatusCode(400, response);

            return StatusCode(200, response);
        }


        //*********************************************************
        //=========================================================
        //*********************************************************


        [HttpPut]
        [Route("UpdateCartItem/{cartItemId:int}")]
        public ActionResult<GenResponse> UpdateCartItem([FromRoute]int cartItemId, [FromBody] UpdateCartItemDTO updateCartItemDTO)
        {
            GenResponse response = new GenResponse();
            
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            response = _cartService.UpdateCartItem(cartItemId, updateCartItemDTO, userId);

            if (response.IsSuccess == false)
                return StatusCode(400, response);

            return StatusCode(200, response);
        }


        //*********************************************************
        //=========================================================
        //*********************************************************

        [HttpDelete]
        [Route("RemoveCartItem/{cartItemId:int}")]
        public ActionResult<GenResponse> RemoveCartItem([FromRoute] int cartItemId)//, [FromBody] RemoveCartItemDTO removeCartItemDTO)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            GenResponse response = _cartService.RemoveCartItem(cartItemId, userId);

            if (response.IsSuccess == false)
                return StatusCode(404, response);

            return Ok(response);
        }


        //*********************************************************
        //=========================================================
        //*********************************************************


        [HttpDelete("ClearCart")]
        public ActionResult<GenResponse> ClearCart()
        {
            GenResponse response = new GenResponse();

            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            response = _cartService.ClearCart(userId);
            
            if (response.IsSuccess == false)
                return StatusCode(400, response);

            return StatusCode(200, response);
        }

        //*********************************************************
        //=========================================================
        //*********************************************************

    }
}
