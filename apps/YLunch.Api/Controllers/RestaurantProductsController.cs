using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using YLunch.Api.Core;
using YLunch.Domain.DTO.ProductModels.RestaurantProductModels;
using YLunch.Domain.ModelsAggregate.RestaurantAggregate;
using YLunch.Domain.ModelsAggregate.UserAggregate;
using YLunch.Domain.ModelsAggregate.UserAggregate.Roles;
using YLunch.Domain.Repositories;
using YLunch.Domain.Services;

namespace YLunch.Api.Controllers
{
    [ApiController]
    [Route("api/restaurant-products")]
    public class RestaurantProductsController : CustomControllerBase
    {
        private readonly IRestaurantProductService _restaurantProductService;
        private readonly IRestaurantProductRepository _restaurantProductRepository;
        private readonly IRestaurantService _restaurantService;

        public RestaurantProductsController(
            UserManager<User> userManager,
            IUserRepository userRepository,
            IConfiguration configuration,
            IRestaurantProductService restaurantProductService,
            IRestaurantProductRepository restaurantProductRepository,
            IRestaurantService restaurantService
        ) : base(userManager, userRepository, configuration)
        {
            _restaurantProductService = restaurantProductService;
            _restaurantProductRepository = restaurantProductRepository;
            _restaurantService = restaurantService;
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.RestaurantAdmin)]
        public async Task<ActionResult<RestaurantProductReadDto>> Create([FromBody] RestaurantProductCreationDto model)
        {
            var currentUser = await GetAuthenticatedUser();

            if (!currentUser.HasARestaurant)
                return StatusCode(
                    StatusCodes.Status403Forbidden,
                    "User has not a restaurant"
                );
            var restaurantProduct =
                await _restaurantProductService.Create(model, currentUser.RestaurantUser.RestaurantId);
            return Ok(restaurantProduct);
        }

        [HttpPatch("{restaurantProductId}")]
        [Authorize(Roles = UserRoles.SuperAdmin + "," + UserRoles.RestaurantAdmin)]
        public async Task<ActionResult<RestaurantProductReadDto>> Update(
            [FromRoute] string restaurantProductId,
            [FromBody] RestaurantProductModificationDto model
        )
        {
            if (restaurantProductId != model.Id)
            {
                return StatusCode(StatusCodes.Status403Forbidden,
                    $"Id {restaurantProductId} from route does not match id {model.Id} from payload");
            }

            var currentUser = await GetAuthenticatedUser();
            var isCurrentUserSuperAdmin = await IsCurrentUserSuperAdmin();

            if (!isCurrentUserSuperAdmin && !currentUser.HasARestaurant)
                return StatusCode(
                    StatusCodes.Status403Forbidden,
                    "User has not a restaurant"
                );

            var restaurantProductToUpdate = await _restaurantProductRepository.GetById(model.Id);

            if (restaurantProductToUpdate == null)
                return StatusCode(
                    StatusCodes.Status404NotFound,
                    $"RestaurantProduct {restaurantProductId} not found"
                );

            return Ok(await _restaurantProductService.Update(model, restaurantProductToUpdate));
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.RestaurantAdmin)]
        public async Task<ActionResult<ICollection<RestaurantProductReadDto>>> GetAll(
            [FromQuery] int? quantityMin,
            [FromQuery] int? quantityMax,
            [FromQuery] bool? isActive
        )
        {
            var currentUser = await GetAuthenticatedUser();

            if (!currentUser.HasARestaurant)
                return StatusCode(
                    StatusCodes.Status403Forbidden,
                    "User has not a restaurant"
                );

            var restaurantProductsFilter = new RestaurantProductsFilter
            {
                RestaurantId = currentUser.RestaurantUser.RestaurantId,
                QuantityMin = quantityMin,
                QuantityMax = quantityMax,
                IsActive = isActive
            };
            var restaurantProducts =
                await _restaurantProductService.GetAll(restaurantProductsFilter);

            return Ok(restaurantProducts);
        }

        [HttpGet("{restaurantProductId}")]
        [Authorize(Roles = UserRoles.SuperAdmin + "," + UserRoles.RestaurantAdmin)]
        public async Task<ActionResult<RestaurantProductReadDto>> Get(string restaurantProductId)
        {
            var currentUser = await GetAuthenticatedUser();
            var isCurrentUserSuperAdmin = await IsCurrentUserSuperAdmin();
            if (!isCurrentUserSuperAdmin && !currentUser.HasARestaurant)
                return StatusCode(
                    StatusCodes.Status403Forbidden,
                    "User has not a restaurant"
                );

            var restaurantProduct =
                await _restaurantProductService.GetById(restaurantProductId);


            if (restaurantProduct == null)
                return StatusCode(
                    StatusCodes.Status404NotFound,
                    $"RestaurantProduct {restaurantProductId} not found"
                );

            if (!isCurrentUserSuperAdmin && restaurantProduct.RestaurantId != currentUser.RestaurantUser.RestaurantId)
            {
                return StatusCode(
                    StatusCodes.Status404NotFound,
                    $"RestaurantProduct {restaurantProductId} not found in restaurant {currentUser.RestaurantUser.RestaurantId}"
                );
            }

            return Ok(restaurantProduct);
        }

        [HttpDelete("{restaurantProductId}")]
        [Authorize(Roles = UserRoles.SuperAdmin + "," + UserRoles.RestaurantAdmin)]
        public async Task<IActionResult> Delete(string restaurantProductId)
        {
            var currentUser = await GetAuthenticatedUser();
            var isCurrentUserSuperAdmin = await IsCurrentUserSuperAdmin();
            if (!isCurrentUserSuperAdmin && !currentUser.HasARestaurant)
                return StatusCode(
                    StatusCodes.Status403Forbidden,
                    "User has not a restaurant"
                );

            var restaurantProduct =
                await _restaurantProductService.GetById(restaurantProductId);
            if (restaurantProduct == null)
                return StatusCode(
                    StatusCodes.Status404NotFound,
                    $"RestaurantProduct {restaurantProductId} not found"
                );

            if (!isCurrentUserSuperAdmin &&
                restaurantProduct.RestaurantId != currentUser.RestaurantUser.RestaurantId
            )
            {
                return StatusCode(
                    StatusCodes.Status404NotFound,
                    $"RestaurantProduct {restaurantProductId} not found in restaurant {currentUser.RestaurantUser.RestaurantId}"
                );
            }

            var restaurantId = restaurantProduct.RestaurantId;
            await _restaurantProductService.Delete(restaurantProductId);
            await _restaurantService.UpdateIsPublished(restaurantId);

            return NoContent();
        }
    }
}
