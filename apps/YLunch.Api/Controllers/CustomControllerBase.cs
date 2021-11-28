using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using YLunch.Application.Exceptions;
using YLunch.Domain.DTO.UserModels;
using YLunch.Domain.ModelsAggregate.UserAggregate;
using YLunch.Domain.ModelsAggregate.UserAggregate.Roles;
using YLunch.Domain.Services.Database.Repositories;

namespace YLunch.Api.Controllers
{
    [Route("api")]
    public class CustomControllerBase : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;
        protected readonly IConfiguration Configuration;

        public CustomControllerBase(UserManager<User> userManager, IUserRepository userRepository,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            Configuration = configuration;
        }

        protected async Task<CurrentUser> GetAuthenticatedUser()
        {
            var userName = User.Identity?.Name;
            if (userName == null)
                return null;

            var user = await _userRepository.GetFullUser(userName);
            if (user == null) return null;
            var userRoles = await _userManager.GetRolesAsync(user);

            return new CurrentUser(user, userRoles);
        }

        protected async Task<UserReadDto> GetAuthenticatedUserDto()
        {
            var userName = User.Identity?.Name;
            if (userName == null)
                return null;

            var user = await _userRepository.GetFullUser(userName);
            if (user == null) throw new NotFoundException();
            return new UserReadDto(user);
        }

        protected async Task<bool> IsCurrentUserSuperAdmin()
        {
            try
            {
                return (await GetAuthenticatedUser()).Roles.Contains(UserRoles.SuperAdmin);
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected async Task<bool> IsCurrentUserCustomer()
        {
            try
            {
                return (await GetAuthenticatedUser()).Roles.Contains(UserRoles.Customer);
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected async Task<bool> IsCurrentUserRestaurantAdmin()
        {
            try
            {
                return (await GetAuthenticatedUser()).Roles.Contains(UserRoles.RestaurantAdmin);
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected async Task<bool> IsCurrentUserEmployee()
        {
            try
            {
                return (await GetAuthenticatedUser()).Roles.Contains(UserRoles.Employee);
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected async Task<bool> IsCurrentUserAuthenticated()
        {
            try
            {
                return await GetAuthenticatedUser() != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected async Task<bool> IsCurrentUserRestaurantUser()
        {
            return await IsCurrentUserRestaurantAdmin() || await IsCurrentUserEmployee();
        }
    }
}
