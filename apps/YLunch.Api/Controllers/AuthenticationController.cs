using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using YLunch.Api.Core.Response;
using YLunch.Api.Core.Response.Errors;
using YLunch.Application.Exceptions;
using YLunch.Domain.DTO.UserModels;
using YLunch.Domain.DTO.UserModels.Registration;
using YLunch.Domain.ModelsAggregate.UserAggregate;
using YLunch.Domain.ModelsAggregate.UserAggregate.Roles;
using YLunch.Domain.Services.Database.Repositories;
using YLunch.Domain.Services.Registration;

namespace YLunch.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : CustomControllerBase
    {
        private readonly IRegistrationService _registrationService;
        private readonly UserManager<User> _userManager;

        public AuthenticationController(
            UserManager<User> userManager,
            IUserRepository userRepository,
            IConfiguration configuration,
            IRegistrationService registrationService
        ) : base(userManager, userRepository, configuration)
        {
            _userManager = userManager;
            _registrationService = registrationService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password)) return Unauthorized();

            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new(ClaimTypes.Name, user.UserName),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            authClaims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSecret"]));

            var token = new JwtSecurityToken(
                issuer: Configuration["JWT:ValidIssuer"],
                audience: Configuration["JWT:ValidAudience"],
                // Todo update expiration time
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(Configuration["JWT:Expiration"])),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }

        [HttpPost("init-super-admin")]
        public async Task<IActionResult> InitSuperAdmin([FromBody] InitSuperAdminDto model)
        {
            if (model.EndpointPassword != Configuration["InitAdminPass"])
                return Unauthorized();

            return await RegisterUser(model.User);
        }

        [HttpPost("register-super-admin")]
        [Core.Authorize(Roles = UserRoles.SuperAdmin)]
        public async Task<IActionResult> RegisterSuperAdmin([FromBody] SuperAdminCreationDto model)
        {
            return await RegisterUser(model);
        }

        [HttpPost("register-restaurantOwner")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterRestaurantOwner([FromBody] RestaurantOwnerCreationDto model)
        {
            return await RegisterUser(model);
        }

        [HttpPost("register-restaurantAdmin")]
        [Core.Authorize(Roles = UserRoles.RestaurantAdmin)]
        public async Task<IActionResult> RegisterRestaurantAdmin([FromBody] RestaurantAdminCreationDto model)
        {
            var currentUser = await GetAuthenticatedUser();
            var restaurantId = currentUser.RestaurantUser.RestaurantId;
            model.RestaurantId = restaurantId;
            return await RegisterUser(model);
        }

        [HttpPost("register-restaurant-employee")]
        [Core.Authorize(Roles = UserRoles.RestaurantAdmin)]
        public async Task<IActionResult> RegisterRestaurantEmployee([FromBody] EmployeeCreationDto model)
        {
            var currentUser = await GetAuthenticatedUser();
            var restaurantId = currentUser.RestaurantUser.RestaurantId;
            model.RestaurantId = restaurantId;
            return await RegisterUser(model);
        }

        [HttpPost("register-customer")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterCustomer([FromBody] CustomerCreationDto model)
        {
            // todo valid username based on company's email template
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

        [Core.Authorize]
        [HttpGet("current-user")]
        public async Task<ActionResult<UserReadDto>> GetCurrentUser()
        {
            var currentUser = await GetAuthenticatedUserDto();
            if (currentUser == null)
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new InvalidTokenErrorResponse()
                );

            return currentUser;
        }

        private async Task CheckUserNonexistence(string username)
        {
            var userExists = await _userManager.FindByNameAsync(username);
            if (userExists != null) throw new UserAlreadyExistsException();
        }

        private async Task<IActionResult> RegisterUser<T>(T model) where T : UserCreationDto
        {
            try
            {
                await CheckUserNonexistence(model.UserName);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response {Status = ResponseStatus.Error, Message = e.Message});
            }

            try
            {
                var userDto = await _registrationService.Register(model);

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
