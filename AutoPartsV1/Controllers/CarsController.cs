//using AutoPartsV1.Auth.Model;
using AutoPartsV1.Data;
using AutoPartsV1.Data.Dtos.Brands;
//using AutoPartsV1.Data.Dtos.CarParts;
using AutoPartsV1.Data.Dtos.Cars;
using AutoPartsV1.Data.Entities;
using AutoPartsV1.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;
using System.Text.Json;

namespace AutoPartsV1.Controllers
{
    [ApiController]
    [Route("api/")] //"api/brands/{brandId}/cars"
    public class CarsController : ControllerBase
    {
        private readonly ICarsRepository _carsRepository;
        private readonly IBrandsRepository _brandsRepository;

        public CarsController(ICarsRepository carsRepository, IBrandsRepository brandsRepository)
        {
            _carsRepository = carsRepository;
            _brandsRepository = brandsRepository;
        }

        // /api/v1/brands/{brandId}/cars
        [HttpGet]
        [Route("brands/{brandId}/cars")]
        public async Task<IEnumerable<CarDto>> GetManyByBrand(int brandId)
        {
            var cars = await _carsRepository.GetManyByBrandAsync(brandId);

            IEnumerable<CarDto> carsDto = cars.Select(x => GetCarDto(x));

            return carsDto;
        }

        [HttpGet]
        [Route("cars/{carId}")] 
        public async Task<ActionResult<CarDto>> Get(int carId) // car pagal id
        {
            // brand  exists + car exists
            // else => NotFound()
            var car = await _carsRepository.GetAsync(carId);

            // 404
            if (car == null)
            {
                return NotFound();
            }

            return GetCarDto(car);
        }

        // /api/v1/brands/{brandId}/cars/{carId}
        [HttpGet]
        [Route("brands/{brandId}/cars/{carId}", Name = "GetCar")]
        public async Task<ActionResult<CarDto>> Get(int carId, int brandId) // vieno car get pagal brand
        {
            var car = await _carsRepository.GetByBrandAsync(carId, brandId);

            // 404
            if (car == null)
            {
                return NotFound();
            }

            return GetCarDto(car);
        }

        // /api/v1/brands/{brandId}/cars
        [HttpPost]
        [Route("brands/{brandId}/cars")]
        //[Authorize(Roles = FoodleRoles.ForumUser)]
        public async Task<ActionResult<CarDto>> Create(int brandId, CreateCarDto createCarDto)
        {
            var car = new Car
            {
                Name = createCarDto.Name,
                ImageURL = createCarDto.ImageURL,
                EngineSize = createCarDto.EngineSize,
                FuelType = createCarDto.FuelType,
                CreationYear = createCarDto.CreationYear,
                BrandId = brandId,
            };

            await _carsRepository.CreateAsync(car);

            // 201
            return Created("", GetCarDto(car));
        }

        // /api/v1/brands/{brandId}/cars/{carId}
        [HttpPut]
        [Route("brands/{brandId}/cars/{carId}")]
        //[Authorize(Roles = FoodleRoles.ForumUser)]
        public async Task<ActionResult<CarDto>> Update(int brandId, int carId, UpdateCarDto updateCarDto)
        {
            var brand = await _brandsRepository.GetAsync(brandId);

            // 404
            if (brand == null)
            {
                return NotFound();
            }

            var car = await _carsRepository.GetByBrandAsync(carId, brandId);

            // 404
            if (car == null)
            {
                return NotFound();
            }

            if (updateCarDto.Name != null)
                car.Name = updateCarDto.Name;

            if (updateCarDto.ImageURL != null)
                car.ImageURL = updateCarDto.ImageURL;

            if (updateCarDto.EngineSize != null)
                car.EngineSize = updateCarDto.EngineSize;

            if (updateCarDto.FuelType != null)
                car.FuelType = updateCarDto.FuelType;

            if(updateCarDto.CreationYear != null)
                car.CreationYear = updateCarDto.CreationYear;

            await _carsRepository.UpdateAsync(car);

            return Ok(GetCarDto(car));
        }


        // /api/v1/brands/{brandId}/cars/{carId}
        [HttpDelete]
        [Route("brands/{brandId}/cars/{carId}")]
        public async Task<ActionResult> Remove(int carId, int brandId)
        {
            var car = await _carsRepository.GetByBrandAsync(carId, brandId);

            // 404
            if (car == null)
            {
                return NotFound();
            }

            await _carsRepository.DeleteAsync(car);

            //204
            return NoContent();
        }

        public CarDto GetCarDto(Car car)
        {
            return new CarDto(car.Id, car.Name, car.ImageURL, car.EngineSize, 
                car.FuelType, car.CreationYear, car.BrandId);
        }
    }
}
