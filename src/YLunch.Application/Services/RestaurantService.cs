using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YLunch.Application.Exceptions;
using YLunch.Domain.DTO.OrderModels;
using YLunch.Domain.DTO.RestaurantModels;
using YLunch.Domain.DTO.UserModels;
using YLunch.Domain.ModelsAggregate.RestaurantAggregate;
using YLunch.Domain.Services.Database.Repositories;
using YLunch.Domain.Services.OrderServices;
using YLunch.Domain.Services.RestaurantServices;

namespace YLunch.Application.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantService(
            IRestaurantRepository restaurantRepository
        )
        {
            _restaurantRepository = restaurantRepository;
        }

        public async Task<RestaurantReadDto> GetById(string id)
        {
            var restaurant = await _restaurantRepository.GetById(id);
            return new RestaurantReadDto(restaurant);
        }

        public async Task<RestaurantReadDto> GetByUserId(string currentUserId)
        {
            var restaurant = await _restaurantRepository.GetByUserId(currentUserId);
            if (restaurant == null) throw new NotFoundException();
            return new RestaurantReadDto(restaurant);
        }

        public async Task<ICollection<RestaurantReadDto>> GetAllForCustomer(RestaurantsFilter restaurantsFilter)
        {
            var restaurants = await _restaurantRepository.GetAllForCustomer(restaurantsFilter);
            return restaurants.Select(x => new RestaurantReadDto(x)).ToList();
        }

        public async Task<ICollection<RestaurantReadDto>> GetAll(RestaurantsFilter restaurantsFilter)
        {
            var restaurants = await _restaurantRepository.GetAll(restaurantsFilter);
            return restaurants.Select(x => new RestaurantReadDto(x)).ToList();
        }

        public async Task DeleteById(string id)
        {
            await _restaurantRepository.DeleteById(id);
        }

        public async Task<RestaurantReadDto> GetByIdForCustomer(string id)
        {
            var restaurant = await _restaurantRepository.GetByIdForCustomer(id);
            if (restaurant == null)
            {
                throw new NotFoundException($"Restaurant {id} not found or not published");
            }
            return new RestaurantReadDto(restaurant);
        }

        public async Task<RestaurantReadDto> Create(RestaurantCreationDto restaurantCreationDto, CurrentUser currentUser)
        {
            var allRestaurantCategories = await _restaurantRepository.GetAllRestaurantCategories();
            var restaurant = Restaurant.Create(restaurantCreationDto, currentUser, allRestaurantCategories);
            await _restaurantRepository.Create(restaurant);
            return new RestaurantReadDto(restaurant);
        }

        public async Task<RestaurantReadDto> Update(RestaurantModificationDto restaurantModificationDto,
            Restaurant restaurant)
        {
            var allRestaurantCategories = await _restaurantRepository.GetAllRestaurantCategories();
            restaurant.Update(restaurantModificationDto, allRestaurantCategories);
            await _restaurantRepository.Update();

            return new RestaurantReadDto(restaurant);
        }

        public async Task UpdateIsPublished(string restaurantId)
        {
            var restaurant = await _restaurantRepository.GetById(restaurantId);
            restaurant.UpdateIsPublished();
            await _restaurantRepository.Update();
        }
    }
}
