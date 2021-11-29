using System.Threading.Tasks;
using YLunch.Application.Exceptions;
using YLunch.Domain.DTO.UserModels;
using YLunch.Domain.DTO.UserModels.Registration;
using YLunch.Domain.ModelsAggregate.UserAggregate;
using YLunch.Domain.ModelsAggregate.UserAggregate.Roles;
using YLunch.Domain.Repositories;
using YLunch.Domain.Services;

namespace YLunch.Application.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IUserRepository _userRepository;

        public RegistrationService(
            IUserRepository userRepository
        )
        {
            _userRepository = userRepository;
        }

        private async Task<UserReadDto> Register(SuperAdminCreationDto userCreationDto)
        {
            var user = User.Create(userCreationDto);

            await _userRepository.Register(user, userCreationDto.Password, UserRoles.SuperAdmin);

            return new UserReadDto(user);
        }

        private async Task<UserReadDto> Register(RestaurantOwnerCreationDto userCreationDto)
        {
            var user = User.Create(userCreationDto);

            await _userRepository.Register(user, userCreationDto.Password, UserRoles.RestaurantAdmin);

            return new UserReadDto(user);
        }

        private async Task<UserReadDto> Register(RestaurantAdminCreationDto userCreationDto)
        {
            var user = User.Create(userCreationDto);

            await _userRepository.Register(user, userCreationDto.Password, UserRoles.RestaurantAdmin);

            return new UserReadDto(user);
        }

        private async Task<UserReadDto> Register(EmployeeCreationDto userCreationDto)
        {
            var user = User.Create(userCreationDto);

            await _userRepository.Register(user, userCreationDto.Password, UserRoles.Employee);

            return new UserReadDto(user);
        }

        private async Task<UserReadDto> Register(CustomerCreationDto userCreationDto)
        {
            var user = User.Create(userCreationDto);

            await _userRepository.Register(user, userCreationDto.Password, UserRoles.Customer);

            return new UserReadDto(user);
        }

        public async Task<UserReadDto> Register<T>(T userCreationDto) where T : UserCreationDto
        {
            return userCreationDto switch
            {
                SuperAdminCreationDto superAdminCreationDto => await Register(superAdminCreationDto),
                RestaurantOwnerCreationDto restaurantOwnerCreationDto => await Register(restaurantOwnerCreationDto),
                RestaurantAdminCreationDto restaurantAdminCreationDto => await Register(restaurantAdminCreationDto),
                EmployeeCreationDto employeeCreationDto => await Register(employeeCreationDto),
                CustomerCreationDto customerCreationDto => await Register(customerCreationDto),
                _ => throw new UserRegistrationException()
            };
        }
    }
}
