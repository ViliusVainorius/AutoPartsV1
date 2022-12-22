using AutoPartsV1.Auth.Model;
using AutoPartsV1.Data.Dtos.Brands;
using AutoPartsV1.Data.Entities;
using AutoPartsV1.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
//using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AutoPartsV1.Controllers
{
    [ApiController]
    [Route("api/brands")]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandsRepository _brandsRepository;
        private readonly IAuthorizationService _authorizationService;

        public BrandsController(IBrandsRepository brandsRepository, IAuthorizationService authorizationService)
        {
            _brandsRepository = brandsRepository;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<IEnumerable<BrandDto>> GetMany()
        {
            var brands = await _brandsRepository.GetManyAsync();

            return brands.Select(o => new BrandDto(o.Id, o.Name, o.Description));
        }

        // api/brands/{brandId}
        [HttpGet]
        [Route("{brandId}", Name = "GetBrand")]
        public async Task<ActionResult<BrandDto>> Get(int brandId)
        {
            var brand = await _brandsRepository.GetAsync(brandId);

            // 404
            if (brand == null)
                return NotFound();

            return new BrandDto(brand.Id, brand.Name, brand.Description);
        }

        // api/brands
        [HttpPost]
        [Authorize(Roles = AutoPartsV1Roles.ForumUser)]
        public async Task<ActionResult<BrandDto>> Create(CreateBrandDto createBrandDto)
        {
            var brand = new Brand 
            { 
                Name = createBrandDto.Name, 
                Description = createBrandDto.Description,
                UserId = User.FindFirstValue(JwtRegisteredClaimNames.Sub)
                //"457935d5-ea34-4001-9ad8-991fbaadaa27"
            };

            await _brandsRepository.CreateAsync(brand);

            // 201
            return Created("", new BrandDto(brand.Id, brand.Name, brand.Description));
            //return CreatedAtAction("GetBrand", new { brandId = brand.Id }, 
                //new BrandDto(brand.Name, brand.Description));
        }

        // api/brands
        [HttpPut]
        [Route("{brandId}")]
        [Authorize(Roles = AutoPartsV1Roles.ForumUser)]
        public async Task<ActionResult<BrandDto>> Update(int brandId, UpdateBrandDto updateBrandDto)
        {
            var brand = await _brandsRepository.GetAsync(brandId);

            // 404
            if (brand == null)
                return NotFound();

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, brand, PolicyNames.ResourceOwner);
            if (!authorizationResult.Succeeded)
            {
                // 404
                return Forbid();
            }

            brand.Name = updateBrandDto.Name;
            brand.Description = updateBrandDto.Description;
            await _brandsRepository.UpdateAsync(brand);

            return Ok(new BrandDto(brand.Id, brand.Name, brand.Description));
        }

        // api/brands/{brandId}
        [HttpDelete]
        [Route("{brandId}")]
        public async Task<ActionResult> Remove(int brandId)
        {
            var brand = await _brandsRepository.GetAsync(brandId);

            // 404
            if (brand == null)
                return NotFound();

            await _brandsRepository.DeleteAsync(brand);

            // 204
            return NoContent();
        }
    }
}
