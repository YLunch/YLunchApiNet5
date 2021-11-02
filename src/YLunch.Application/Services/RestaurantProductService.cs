using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YLunch.Domain.DTO.ProductModels.RestaurantProductModels;
using YLunch.Domain.ModelsAggregate.RestaurantAggregate;
using YLunch.Domain.Services.Database.Repositories;
using YLunch.Domain.Services.RestaurantServices;

namespace YLunch.Application.Services
{
    public class RestaurantProductService : IRestaurantProductService
    {
        private readonly IRestaurantProductRepository _restaurantProductRepository;
        private readonly IRestaurantService _restaurantService;

        public RestaurantProductService(
            IRestaurantProductRepository restaurantProductRepository,
            IRestaurantService restaurantService
        )
        {
            _restaurantProductRepository = restaurantProductRepository;
            _restaurantService = restaurantService;
        }

        public async Task<RestaurantProductReadDto> Create(RestaurantProductCreationDto restaurantProductCreationDto,
            string restaurantId)
        {
            var restaurantProduct = RestaurantProduct.Create(restaurantProductCreationDto, restaurantId);
            await _restaurantProductRepository.Create(restaurantProduct);

            await _restaurantService.UpdateIsPublished(restaurantId);

            return new RestaurantProductReadDto(restaurantProduct);
        }

        public async Task<RestaurantProductReadDto> Update(
            RestaurantProductModificationDto restaurantProductModificationDto,
            RestaurantProduct restaurantProduct)
        {
            restaurantProduct.Update(restaurantProductModificationDto);
            await _restaurantProductRepository.Update();
            await _restaurantService.UpdateIsPublished(restaurantProduct.RestaurantId);

            return new RestaurantProductReadDto(restaurantProduct);
        }

        public async Task<ICollection<RestaurantProductReadDto>> GetAllByRestaurantId(string restaurantId)
        {
            var restaurantProducts = await _restaurantProductRepository.GetAllByRestaurantId(restaurantId);
            return restaurantProducts.Select(x => new RestaurantProductReadDto(x)).ToList();
        }

        public async Task<ICollection<RestaurantProductReadDto>> GetAllForCustomerByRestaurantId(string restaurantId)
        {
            var restaurantProducts = await _restaurantProductRepository.GetAllForCustomerByRestaurantId(restaurantId);
            return restaurantProducts.Select(x => new RestaurantProductReadDto(x)).ToList();
        }

        public async Task Delete(string restaurantProductId)
        {
            await _restaurantProductRepository.Delete(restaurantProductId);
        }
    }
}
