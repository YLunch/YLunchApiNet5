using System.Collections.Generic;
using System.Threading.Tasks;
using YLunch.Domain.DTO.OrderModels;
using YLunch.Domain.DTO.RestaurantModels;
using YLunch.Domain.DTO.UserModels;
using YLunch.Domain.ModelsAggregate.RestaurantAggregate;

namespace YLunch.Domain.Services.RestaurantServices
{
    public interface IRestaurantService
    {
        Task<RestaurantReadDto> Create(RestaurantCreationDto restaurantCreationDto, CurrentUser currentUser);
        Task<RestaurantReadDto> Update(RestaurantModificationDto restaurantModificationDto,
            Restaurant restaurant);
        Task UpdateIsPublished(string restaurantId);
        Task<RestaurantReadDto> GetById(string id);
        Task<RestaurantReadDto> GetByUserId(string currentUserId);
        Task<ICollection<RestaurantReadDto>> GetAllForCustomer(RestaurantsFilter restaurantsFilter);
        Task<ICollection<RestaurantReadDto>> GetAll(RestaurantsFilter restaurantsFilter);
        Task DeleteById(string id);
        Task<RestaurantReadDto> GetByIdForCustomer(string id);
    }
}
