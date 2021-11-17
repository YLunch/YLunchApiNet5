using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using YLunch.Domain.ModelsAggregate.UserAggregate;
using YLunch.Domain.ModelsAggregate.UserAggregate.Roles;
using YLunch.Domain.Services.Database.Repositories;

namespace YLunch.Api.Controllers
{
    [Route("api")]
    public class ApiController : CustomControllerBase
    {
        private const string API_RUNNING_MESSAGE = "Api is running organization";

        private static readonly string API_RUNNING_AS_AUTHENTICATED_MESSAGE =
            $"{API_RUNNING_MESSAGE}, and you're authenticated";

        public ApiController(
            UserManager<User> userManager,
            IUserRepository userRepository,
            IConfiguration configuration
        ) : base(userManager, userRepository, configuration)
        {
        }

        [HttpGet("try")]
        [AllowAnonymous]
        public string Try() => API_RUNNING_MESSAGE;

        [HttpGet("try-authenticated")]
        [Core.Authorize]
        public string TryAsAuthenticated() => API_RUNNING_AS_AUTHENTICATED_MESSAGE;

        [HttpGet("try-authenticated-superAdmin")]
        [Core.Authorize(UserRoles.SuperAdmin)]
        public string TryAsAuthenticatedSuperAdmin() =>
            $"{API_RUNNING_MESSAGE}, and you're a {UserRoles.SuperAdmin}";


        [HttpGet("try-authenticated-restaurantAdmin")]
        [Core.Authorize(UserRoles.RestaurantAdmin)]
        public string TryAsAuthenticatedRestaurantAdmin() =>
            $"{API_RUNNING_MESSAGE}, and you're a {UserRoles.RestaurantAdmin}";

        [HttpGet("try-authenticated-employee")]
        [Core.Authorize(UserRoles.Employee)]
        public string TryAsAuthenticatedEmployee() =>
            $"{API_RUNNING_MESSAGE}, and you're a {UserRoles.Employee}";

        [HttpGet("try-authenticated-customer")]
        [Core.Authorize(UserRoles.Customer)]
        public string TryAsAuthenticatedCustomer() =>
            $"{API_RUNNING_MESSAGE}, and you're a {UserRoles.Customer}";
    }
}
