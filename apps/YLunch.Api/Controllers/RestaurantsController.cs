using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using YLunch.Application.Exceptions;
using YLunch.Domain.DTO.ProductModels.RestaurantProductModels;
using YLunch.Domain.DTO.RestaurantModels;
using YLunch.Domain.ModelsAggregate.RestaurantAggregate;
using YLunch.Domain.ModelsAggregate.UserAggregate;
using YLunch.Domain.ModelsAggregate.UserAggregate.Roles;
using YLunch.Domain.Repositories;
using YLunch.Domain.Services;

namespace YLunch.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantsController : CustomControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IRestaurantProductService _restaurantProductService;

        public RestaurantsController(
            UserManager<User> userManager,
            IUserRepository userRepository,
            IConfiguration configuration,
            IRestaurantService restaurantService,
            IRestaurantRepository restaurantRepository,
            IRestaurantProductService restaurantProductService
        ) : base(userManager, userRepository, configuration)
        {
            _restaurantService = restaurantService;
            _restaurantRepository = restaurantRepository;
            _restaurantProductService = restaurantProductService;
        }

        [HttpPost]
        [Core.Authorize(Roles = UserRoles.RestaurantAdmin)]
        public async Task<IActionResult> Create([FromBody] RestaurantCreationDto model)
        {
            try
            {
                var currentUser = await GetAuthenticatedUser();
                if (currentUser.HasARestaurant)
                    return StatusCode(
                        StatusCodes.Status403Forbidden,
                        "User has already a restaurant"
                    );
                return Ok(await _restaurantService.Create(model, currentUser));
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    e
                );
            }
        }

        [HttpPatch("{id}")]
        [Core.Authorize(Roles = UserRoles.RestaurantAdmin)]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] RestaurantModificationDto model)
        {
            if (id != model.Id)
            {
                return StatusCode(StatusCodes.Status403Forbidden,
                    "Id in query doesn't match restaurantId in body");
            }

            try
            {
                var currentUser = await GetAuthenticatedUser();
                var restaurant = await _restaurantRepository.GetByIdIncludingProducts(model.Id);

                if (restaurant == null)
                    return StatusCode(
                        StatusCodes.Status404NotFound,
                        "Restaurant not found"
                    );

                if (!restaurant.RestaurantUsers.Any(x => x.UserId.Equals(currentUser.Id)))
                    return StatusCode(
                        StatusCodes.Status403Forbidden,
                        "User is not from the restaurant"
                    );

                return Ok(await _restaurantService.Update(model, restaurant));
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    e
                );
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] bool? isPublic)
        {
            var restaurantsFilter = new RestaurantsFilter { IsPublished = isPublic };
            return Ok(await IsCurrentUserSuperAdmin()
                ? await _restaurantService.GetAll(restaurantsFilter)
                : await _restaurantService.GetAllForCustomer(restaurantsFilter));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var restaurant = await IsCurrentUserSuperAdmin()
                    ? await _restaurantService.GetById(id)
                    : await _restaurantService.GetByIdForCustomer(id);
                return Ok(restaurant);
            }
            catch (NotFoundException)
            {
                return StatusCode(StatusCodes.Status404NotFound, $"Restaurant {id} not found");
            }
        }

        [HttpGet("mine")]
        [Core.Authorize(Roles = UserRoles.RestaurantAdmin)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var currentUser = await GetAuthenticatedUser();
                return Ok(await _restaurantService.GetByUserId(currentUser.Id));
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    e
                );
            }
        }

        [HttpDelete("{id}")]
        [Core.Authorize(Roles = UserRoles.SuperAdmin)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _restaurantService.DeleteById(id);
            }
            catch (NotFoundException notFoundException)
            {
                return StatusCode(StatusCodes.Status404NotFound, notFoundException.Message);
            }

            return NoContent();
        }

        [HttpPost("{restaurantId}")]
        [Authorize(Roles = UserRoles.SuperAdmin)]
        public async Task<IActionResult> CreateProduct(
            [FromRoute] string restaurantId,
            [FromBody] RestaurantProductCreationDto model
        )
        {
            var restaurant = await _restaurantService.GetById(restaurantId);
            if (restaurant == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, $"Restaurant {restaurantId} not found");
            }

            var restaurantProduct =
                await _restaurantProductService.Create(model, restaurantId);
            return Ok(restaurantProduct);
        }

        [AllowAnonymous]
        [HttpGet("{restaurantId}/restaurant-products")]
        public async Task<IActionResult> GetProducts(
            [FromRoute] string restaurantId,
            [FromQuery] int? quantityMin,
            [FromQuery] int? quantityMax,
            [FromQuery] bool? isActive
        )
        {
            var restaurantProductsFilter = new RestaurantProductsFilter
            {
                QuantityMin = quantityMin,
                QuantityMax = quantityMax,
                IsActive = isActive,
                RestaurantId = restaurantId
            };
            var restaurantProducts =
                await _restaurantProductService.GetAll(restaurantProductsFilter);

            return Ok(restaurantProducts);
        }

        [AllowAnonymous]
        [HttpGet("{restaurantId}/restaurant-products/{restaurantProductId}")]
        public async Task<IActionResult> GetProduct(string restaurantId, string restaurantProductId)
        {
            var restaurantProduct =
                await _restaurantProductService.GetById(restaurantProductId);
            if (restaurantProduct == null)
            {
                return StatusCode(
                    StatusCodes.Status404NotFound,
                    $"Product {restaurantProductId} not found"
                );
            }

            if (restaurantProduct.RestaurantId != restaurantId)
            {
                return StatusCode(
                    StatusCodes.Status404NotFound,
                    $"Product {restaurantProductId} not found in restaurant {restaurantId}"
                );
            }

            return Ok(restaurantProduct);
        }
    }
}
