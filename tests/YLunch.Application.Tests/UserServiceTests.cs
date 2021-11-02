using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using YLunch.Application.Services;
using YLunch.Domain.ModelsAggregate.UserAggregate;
using YLunch.Infrastructure.Database;
using YLunch.Infrastructure.Database.Repositories;

namespace YLunch.Application.Tests
{
    public class UserServiceTests
    {
        private readonly ApplicationDbContext context;
        private readonly UserRepository userRepository;
        private readonly UserService userService;

        public UserServiceTests()
        {
            context = ContextBuilder.BuildContext();
            InitContext();
            userRepository = new UserRepository(null , null ,context);
            userService = new UserService(userRepository);
        }

        private void InitContext()
        {
            var user = new User
            {
                Id = "6899dc5a-3764-45d7-8d8f-91855f294f71",
                UserName = "RAEL06@HOTMAIL.FR",
                Firstname = "rael",
                Lastname = "calitro",
                Email = null,
                PhoneNumber = null,
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                CreationDateTime = DateTime.Parse("2021-10-31T14:34:46.0431306"),
                IsAccountActivated = false
            };

            context.Users.Add(user);
            context.SaveChanges();
        }

        [Fact]
        public async Task GetAllUsers_Should_Return_All_Users()
        {
            var expected = context.Users.Count();
            var result = (await userService.GetAllUsers()).Count;

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task GetAsCustomerById_Should_Return_A_Customer_Based_On_Input_Id()
        {
            // Arrange
            var id = context.Users.First().Id;

            // Act
            var result = (await userService.GetAsCustomerById(id)).Id;

            // Assert
            var expected = context.Users.First().Id;
            Assert.Equal(expected, result);
        }
    }
}
