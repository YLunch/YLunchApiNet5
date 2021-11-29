using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using YLunch.Api.Core;
using YLunch.Application.Exceptions;
using YLunch.Domain.DTO.OrderModels;
using YLunch.Domain.DTO.OrderModels.OrderStatusModels;
using YLunch.Domain.ModelsAggregate.OrderAggregate;
using YLunch.Domain.ModelsAggregate.UserAggregate;
using YLunch.Domain.ModelsAggregate.UserAggregate.Roles;
using YLunch.Domain.Repositories;
using YLunch.Domain.Services;
using YLunch.DomainShared.RestaurantAggregate.Enums;

namespace YLunch.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : CustomControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(
            UserManager<User> userManager,
            IUserRepository userRepository,
            IConfiguration configuration,
            IOrderService orderService
        ) : base(userManager, userRepository, configuration)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Customer)]
        public async Task<IActionResult> Create([FromBody] OrderCreationDto orderCreationDto)
        {
            try
            {
                var currentUser = await GetAuthenticatedUser();
                var orderReadDto = await _orderService.Create(orderCreationDto, currentUser.Customer);
                return Ok(orderReadDto);
            }
            catch (NotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("statuses")]
        [Authorize(Roles = UserRoles.RestaurantAdmin + "," + UserRoles.Employee)]
        public async Task<IActionResult> AddStatusToMultipleOrders(
            [FromBody] AddOrderStatusToMultipleOrdersDto addOrderStatusToMultipleOrdersDto)
        {
            try
            {
                var orderReadDtoCollection =
                    await _orderService.AddStatusToMultipleOrders(addOrderStatusToMultipleOrdersDto);
                return Ok(orderReadDtoCollection);
            }
            catch (BadNewOrderStateException badNewOrderStateException)
            {
                return StatusCode(StatusCodes.Status403Forbidden, badNewOrderStateException.Message);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.SuperAdmin + "," + UserRoles.RestaurantAdmin + "," + UserRoles.Employee)]
        public async Task<IActionResult> GetOrders([FromQuery] OrderState? status, [FromQuery] DateTime? afterDateTime,
            [FromQuery] string restaurantId)
        {
            var currentUser = await GetAuthenticatedUser();
            if (currentUser == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Current User not found");
            }

            var ordersFilter = new OrdersFilter
            {
                AfterDateTime = afterDateTime,
                Status = status,
                RestaurantId = await IsCurrentUserSuperAdmin() ? restaurantId : currentUser.RestaurantUser.RestaurantId
            };

            var orderReadDtoCollection =
                await _orderService.GetAll(ordersFilter);
            return Ok(orderReadDtoCollection.ToList());
        }
    }
}
