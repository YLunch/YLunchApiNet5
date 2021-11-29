using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using YLunch.Domain.ModelsAggregate.UserAggregate;
using YLunch.Domain.ModelsAggregate.UserAggregate.Roles;
using YLunch.Domain.Repositories;

namespace YLunch.Api.Controllers
{
    [Route("api/[controller]")]
    public class TrialsController : CustomControllerBase
    {
        private const string API_RUNNING_MESSAGE = "Api is running";

        private static readonly string API_RUNNING_AS_AUTHENTICATED_MESSAGE =
            $"{API_RUNNING_MESSAGE}, and you're authenticated";

        public TrialsController(
            UserManager<User> userManager,
            IUserRepository userRepository,
            IConfiguration configuration
        ) : base(userManager, userRepository, configuration)
        {
        }

        [HttpGet]
        [AllowAnonymous]
        public string Try() => API_RUNNING_MESSAGE;

        [HttpGet("authenticated")]
        [Core.Authorize]
        public string TryAsAuthenticated() => API_RUNNING_AS_AUTHENTICATED_MESSAGE;

        [HttpGet("authenticated-superAdmin")]
        [Core.Authorize(UserRoles.SuperAdmin)]
        public string TryAsAuthenticatedSuperAdmin() =>
            $"{API_RUNNING_MESSAGE}, and you're a {UserRoles.SuperAdmin}";


        [HttpGet("authenticated-restaurantAdmin")]
        [Core.Authorize(UserRoles.RestaurantAdmin)]
        public string TryAsAuthenticatedRestaurantAdmin() =>
            $"{API_RUNNING_MESSAGE}, and you're a {UserRoles.RestaurantAdmin}";

        [HttpGet("authenticated-employee")]
        [Core.Authorize(UserRoles.Employee)]
        public string TryAsAuthenticatedEmployee() =>
            $"{API_RUNNING_MESSAGE}, and you're a {UserRoles.Employee}";

        [HttpGet("authenticated-customer")]
        [Core.Authorize(UserRoles.Customer)]
        public string TryAsAuthenticatedCustomer() =>
            $"{API_RUNNING_MESSAGE}, and you're a {UserRoles.Customer}";
    }
}
