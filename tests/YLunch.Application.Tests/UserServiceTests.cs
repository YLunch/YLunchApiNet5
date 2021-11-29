using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using YLunch.Application.Exceptions;
using YLunch.Application.Services;
using YLunch.Application.Tests.Mocks;
using YLunch.Domain.DTO.UserModels;
using YLunch.Domain.ModelsAggregate.UserAggregate;
using YLunch.Domain.Services;
using YLunch.Infrastructure.Database;
using YLunch.Infrastructure.Database.Repositories;

namespace YLunch.Application.Tests
{
    public class UserServiceTests
    {
        private const string NOT_EXISTING_USERNAME = "not-existing@ynov.com";
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public UserServiceTests()
        {
            _context = ContextBuilder.BuildContext();
            _context.Users.AddRange(UsersMock.USERS);
            _context.SaveChanges();

            var userRepository = new UserRepository(null, null, _context);
            _userService = new UserService(userRepository);
        }

        [Fact]
        public async Task GetAllUsers_Should_Return_All_Users()
        {
            // Act
            var actual = await _userService.GetAllUsers();

            // Assert
            var expected = await _context.Users.Select(u => new UserReadDto(u)).ToListAsync();

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetCustomerById_Should_Return_A_Customer_Based_On_Input_Id()
        {
            // Arrange
            var id = UsersMock.CUSTOMER.Id;

            // Act
            var actual = await _userService.GetCustomerById(id);

            // Assert
            var user = await _context.Users.FindAsync(id);
            var expected = new UserAsCustomerDetailsReadDto(user);
            actual.Should().BeEquivalentTo(expected);
        }


        [Fact]
        public async Task DeleteUserByUsername_Should_Delete_A_User_Given_His_Username()
        {
            // Arrange
            var username = UsersMock.SUPER_ADMIN.UserName;

            // Act
            await _userService.DeleteUserByUsername(username);

            // Assert
            var user = await _context.Users.FindAsync(username);
            Assert.Null(user);
        }

        [Fact]
        public async Task DeleteUserByUsername_Should_Throw_If_User_Given_His_Username_Not_Exists()
        {
            // Arrange
            const string username = NOT_EXISTING_USERNAME;

            // Act
            async Task Act() => await _userService.DeleteUserByUsername(username);

            // Assert
            var notFoundException = await Assert.ThrowsAsync<NotFoundException>(Act);
            Assert.Equal(notFoundException.Message, $"Entity not found exception: User with username: '{NOT_EXISTING_USERNAME}' not found");
        }
    }
}
