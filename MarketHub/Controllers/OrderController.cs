using MarketHub.BLL.DTOs.Order;
using MarketHub.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MarketHub.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        //Di
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        //APIs
        [HttpGet]
        [Route("")]
        [Authorize(Roles ="User")]
        public ActionResult<GenResponse> GetAll(string? status)
        {
            GenResponse response = new GenResponse();

            string userId = GetUserId();

            response = _orderService.GetAll(userId, status);

            if (response.IsSuccess == false)
                return StatusCode(400, response);

            return Ok(response);
        }

        [HttpGet]
        [Route("{orderId:int}")]
        [Authorize(Roles = "User")]
        public ActionResult<GenResponse> GetById(int orderId)
        {
            GenResponse response = new GenResponse();

            string userId = GetUserId();

            response = _orderService.GetById(userId, orderId);

            if (response.IsSuccess == false)
                return StatusCode(400, response);

            return Ok(response);
        }

        [HttpPost("")]
        [Authorize(Roles = "User")]
        public ActionResult<GenResponse> CheckOut(OrderCreateDTO orderCreateDTO)
        {
            GenResponse response = new GenResponse();

            string userId = GetUserId();
            
            response = _orderService.CheckOut(userId, orderCreateDTO, User);

            if (response.IsSuccess == false)
                return StatusCode(400, response);

            return Ok(response);
        }

        [HttpDelete]
        [Route("{orderId:int}/Cancel")]
        [Authorize(Roles = "User")]

        public ActionResult<GenResponse> CancelOrder(int orderId)
        {
            GenResponse response = new GenResponse();

            string userId = GetUserId();

            response = _orderService.CancelOrder(orderId, userId);

            if (response.IsSuccess == false)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPatch("{orderId:int}/UpdateStatus")]
        [Authorize(Roles = "Admin")]
        public ActionResult<GenResponse> UpdateStatus(int orderId, UpdateOrderStatusDTO updateOrderStatusDTO)
        {
            GenResponse response = new GenResponse();

            string userId = GetUserId();

            response = _orderService.UpdateStatus(userId, orderId, updateOrderStatusDTO);

            if (response.IsSuccess == false)
                return BadRequest(response);

            return Ok(response);
        }

        
        private string GetUserId()//(ClaimsPrincipal User)
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
