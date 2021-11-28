using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using YLunch.Api.Core;
using YLunch.Application.Exceptions;
using YLunch.Domain.ModelsAggregate.UserAggregate;
using YLunch.Domain.ModelsAggregate.UserAggregate.Roles;
using YLunch.Domain.Services.Database.Repositories;
using YLunch.Domain.Services.RestaurantServices;
using YLunch.Domain.Services.UserServices;

namespace YLunch.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : CustomControllerBase
    {
        private readonly IUserService _userService;

        public CustomersController(
            UserManager<User> userManager,
            IUserService userService,
            IUserRepository userRepository,
            IConfiguration configuration
        ) : base(userManager, userRepository, configuration)
        {
            _userService = userService;
        }


        [HttpGet("{customerId}")]
        [Core.Authorize(Roles = UserRoles.RestaurantAdmin + "," + UserRoles.Employee)]
        public async Task<IActionResult> GetCustomerDetails(string customerId)
        {
            try
            {
                var customer = await _userService.GetCustomerById(customerId);
                return Ok(customer);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
