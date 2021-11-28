using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using YLunch.Application.Exceptions;
using YLunch.Domain.ModelsAggregate.UserAggregate;
using YLunch.Domain.ModelsAggregate.UserAggregate.Roles;
using YLunch.Domain.Services.Database.Repositories;
using YLunch.Infrastructure.Database.Exceptions;

namespace YLunch.Infrastructure.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public UserRepository(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context
        )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task Register(User user, string password, string role)
        {
            var result = await _userManager.CreateAsync(user, password);
            var success = result.Succeeded;

            if (!await _roleManager.RoleExistsAsync(UserRoles.SuperAdmin))
                success &= (await _roleManager.CreateAsync(new IdentityRole(UserRoles.SuperAdmin))).Succeeded;
            if (!await _roleManager.RoleExistsAsync(UserRoles.RestaurantAdmin))
                success &= (await _roleManager.CreateAsync(new IdentityRole(UserRoles.RestaurantAdmin))).Succeeded;
            if (!await _roleManager.RoleExistsAsync(UserRoles.Employee))
                success &= (await _roleManager.CreateAsync(new IdentityRole(UserRoles.Employee))).Succeeded;
            if (!await _roleManager.RoleExistsAsync(UserRoles.Customer))
                success &= (await _roleManager.CreateAsync(new IdentityRole(UserRoles.Customer))).Succeeded;

            if (await _roleManager.RoleExistsAsync(role))
                success &= (await _userManager.AddToRoleAsync(user, role)).Succeeded;

            if (!success)
                throw new UserCreationException();
        }

        public async Task<User> GetFullUser(string username)
        {
            var user = await _context.Users
                .Include(x => x.Customer)
                .Include(x => x.RestaurantUser)
                .FirstOrDefaultAsync(x => x.UserName.Equals(username));

            return user;
        }

        public async Task<ICollection<User>> GetFullUsers()
        {
            return await _context.Users
                .Include(x => x.Customer)
                .Include(x => x.RestaurantUser)
                .ToListAsync();
        }

        public async Task<User> GetCustomerById(string id)
        {
            var user = await _context.Users
                .Include(x => x.Customer)
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            return user;
        }

        public async Task DeleteByUsername(string username)
        {

            var user = await GetFullUser(username);
            if (user is null)
            {
                throw new NotFoundException($"User with username: '{username}' not found");
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
