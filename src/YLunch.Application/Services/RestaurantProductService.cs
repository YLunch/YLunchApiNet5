using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YLunch.Domain.DTO.ProductModels.RestaurantProductModels;
using YLunch.Domain.ModelsAggregate.RestaurantAggregate;
using YLunch.Domain.Repositories;
using YLunch.Domain.Services;

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

        public async Task<ICollection<RestaurantProductReadDto>> GetAll(RestaurantProductsFilter restaurantProductsFilter)
        {
            var restaurantProducts = await _restaurantProductRepository.GetAll(restaurantProductsFilter);
            return restaurantProducts.Select(x => new RestaurantProductReadDto(x)).ToList();
        }

        public async Task Delete(string restaurantProductId)
        {
            await _restaurantProductRepository.Delete(restaurantProductId);
        }

        public async Task<RestaurantProductReadDto> GetById(string restaurantProductId)
        {
            var restaurantProduct = await _restaurantProductRepository.GetById(restaurantProductId);
            return new RestaurantProductReadDto(restaurantProduct);
        }
    }
}
