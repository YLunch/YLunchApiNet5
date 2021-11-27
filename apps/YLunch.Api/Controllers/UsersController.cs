using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using YLunch.Api.Core.Response;
using YLunch.Application.Exceptions;
using YLunch.Domain.DTO.UserModels.Registration;
using YLunch.Domain.ModelsAggregate.UserAggregate;
using YLunch.Domain.ModelsAggregate.UserAggregate.Roles;
using YLunch.Domain.Services.Database.Repositories;
using YLunch.Domain.Services.Registration;
using YLunch.Domain.Services.UserServices;

namespace YLunch.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : CustomControllerBase
    {
        private readonly IRegistrationService _registrationService;
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;

        public UsersController(
            UserManager<User> userManager,
            IUserService userService,
            IUserRepository userRepository,
            IConfiguration configuration,
            IRegistrationService registrationService
        ) : base(userManager, userRepository, configuration)
        {
            _userManager = userManager;
            _userService = userService;
            _registrationService = registrationService;
        }

        [HttpPost("initial-super-admin")]
        public async Task<IActionResult> InitSuperAdmin([FromBody] InitSuperAdminDto model)
        {
            if (model.EndpointPassword != Configuration["InitAdminPass"])
                return Unauthorized();

            return await RegisterUser(model.User);
        }

        [HttpPost("super-admin")]
        [Core.Authorize(Roles = UserRoles.SuperAdmin)]
        public async Task<IActionResult> RegisterSuperAdmin([FromBody] SuperAdminCreationDto model)
        {
            return await RegisterUser(model);
        }

        [HttpPost("restaurant-owner")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterRestaurantOwner([FromBody] RestaurantOwnerCreationDto model)
        {
            return await RegisterUser(model);
        }

        [HttpPost("restaurant-admin")]
        [Core.Authorize(Roles = UserRoles.RestaurantAdmin)]
        public async Task<IActionResult> RegisterRestaurantAdmin([FromBody] RestaurantAdminCreationDto model)
        {
            var currentUser = await GetAuthenticatedUser();
            var restaurantId = currentUser.RestaurantUser.RestaurantId;
            model.RestaurantId = restaurantId;
            return await RegisterUser(model);
        }

        [HttpPost("restaurant-employee")]
        [Core.Authorize(Roles = UserRoles.RestaurantAdmin)]
        public async Task<IActionResult> RegisterRestaurantEmployee([FromBody] EmployeeCreationDto model)
        {
            var currentUser = await GetAuthenticatedUser();
            var restaurantId = currentUser.RestaurantUser.RestaurantId;
            model.RestaurantId = restaurantId;
            return await RegisterUser(model);
        }

        [HttpPost("customer")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterCustomer([FromBody] CustomerCreationDto model)
        {
            // Todo valid username based on company's email template
            if (!model.IsValid())
                return StatusCode(
                    StatusCodes.Status403Forbidden,
                    new Response
                    {
                        Status = ResponseStatus.Error,
                        Message = "You must set a username with an Ynov email address"
                    }
                );
            return await RegisterUser(model);
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.SuperAdmin)]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsers());
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
            catch (Exception e)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    e
                );
            }
        }

        [HttpDelete("{username}")]
        [Authorize(Roles = UserRoles.SuperAdmin)]
        public async Task<IActionResult> DeleteUserByUsername(string username)
        {
            try
            {
                await _userService.DeleteUserByUsername(username);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = ResponseStatus.Error, Message = e.Message });
            }
        }

        private async Task CheckUserNonexistence(string username)
        {
            var userExists = await _userManager.FindByNameAsync(username);
            if (userExists != null) throw new UserAlreadyExistsException();
        }

        private async Task<IActionResult> RegisterUser(UserCreationDto userCreationDto)
        {
            try
            {
                await CheckUserNonexistence(userCreationDto.UserName);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = ResponseStatus.Error, Message = e.Message });
            }

            try
            {
                var userDto = await _registrationService.Register(userCreationDto);

                return StatusCode(
                    StatusCodes.Status201Created,
                    userDto
                );
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response
                    {
                        Status = ResponseStatus.Error,
                        Message = "User creation failed! Please check user details and try again."
                    });
            }
        }
    }
}
