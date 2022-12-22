using AutoPartsV1.Data.Dtos.CarParts;
using AutoPartsV1.Data.Dtos.Cars;
using AutoPartsV1.Data.Entities;
using AutoPartsV1.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AutoPartsV1.Controllers
{
    [ApiController]
    [Route("api/")]
    public class CarPartsController : ControllerBase
    {
        private readonly ICarsRepository _carsRepository;
        private readonly IBrandsRepository _brandsRepository;
        private readonly ICarPartsRepository _carPartsRepository;

        public CarPartsController(ICarsRepository carsRepository, IBrandsRepository brandsRepository, ICarPartsRepository carPartsRepository)
        {
            _carsRepository = carsRepository;
            _brandsRepository = brandsRepository;
            _carPartsRepository = carPartsRepository;
        }

        [HttpGet]
        [Route("brands/{brandId}/cars/{carId}/carParts")]
        public async Task<IEnumerable<CarPartDto>> GetManyByBrandCar(int carId)
        {
            var carParts = await _carPartsRepository.GetManyAsync(carId);

            IEnumerable<CarPartDto> carPartsDto = carParts.Select(x => GetCarPartDto(x));

            return carPartsDto;
        }

        // /api/v1/cars/{carId}/carParts/{carPartId}
        [HttpGet]
        [Route("cars/{carId}/carParts")]
        public async Task<IEnumerable<CarPartDto>> GetManyByCar(int carId)
        {
            var carParts = await _carPartsRepository.GetManyAsync(carId);

            IEnumerable<CarPartDto> carPartDto = carParts.Select(x => GetCarPartDto(x));

            return carPartDto;
        }

        [HttpGet]
        [Route("carParts")]
        public async Task<IEnumerable<CarPartDto>> GetMany()
        {
            var carParts = await _carPartsRepository.GetManyAsync();

            IEnumerable<CarPartDto> carPartsDto = carParts.Select(x => GetCarPartDto(x));

            return carPartsDto;
        }

        // /api/v1/brands/{brandId}/cars/{carId}/carParts/{carPartId}
        [HttpGet]
        [Route("brands/{brandId}/cars/{carId}/carParts/{carPartId}")]
        public async Task<ActionResult<CarPartDto>> GetByBrandCar(int carId, int carPartId)
        {
            var carPart = await _carPartsRepository.GetAsync(carId, carPartId);

            // 404
            if (carPart == null)
            {
                return NotFound();
            }

            return GetCarPartDto(carPart);
        }

        // /api/cars/{carId}/carParts/{carPartId}
        [HttpGet]
        [Route("cars/{carId}/carParts/{carPartId}")]
        public async Task<ActionResult<CarPartDto>> GetByCar(int carPartId)
        {
            var carPart = await _carPartsRepository.GetCarPartAsync(carPartId);

            // 404
            if (carPart == null)
            {
                return NotFound();
            }

            return GetCarPartDto(carPart);
        }

        // /api/carParts/{carPartId}
        [HttpGet]
        [Route("carParts/{carPartId}")]
        public async Task<ActionResult<CarPartDto>> Get(int carPartId)
        {
            var carPart = await _carPartsRepository.GetCarPartAsync(carPartId);

            // 404
            if (carPart == null)
            {
                return NotFound();
            }

            return GetCarPartDto(carPart);
        }

        // /api/v1/brands/{brandId}/cars/{carId}/carParts/
        [HttpPost]
        [Route("brands/{brandId}/cars/{carId}/carParts")]
        public async Task<ActionResult<CarPartDto>> Create(int carId, CreateCarPartDto createCarPartDto)
        {
            var carPart = new CarPart 
            { 
                Name = createCarPartDto.Name, 
                ImageURL = createCarPartDto.ImageURL,
                Code = createCarPartDto.Code,
                Description = createCarPartDto.Description, 
                CarId = carId
            };


            await _carPartsRepository.CreateAsync(carPart);

            // 201
            return Created("", GetCarPartDto(carPart));
        }

        [HttpPut]
        [Route("brands/{brandId}/cars/{carId}/carParts/{carPartId}")]
        public async Task<ActionResult<CarPartDto>> Update(int brandId, int carId, int carPartId, UpdateCarPartDto updateCarPartDto)
        {

            var car = await _carsRepository.GetByBrandAsync(carId, brandId);

            // 404
            if (car == null)
            {
                return NotFound();
            }

            var carPart = await _carPartsRepository.GetAsync(carId, carPartId);

            // 404
            if (carPart == null)
            {
                return NotFound();
            }

            if (updateCarPartDto.Name != null)
                carPart.Name = updateCarPartDto.Name;

            if (updateCarPartDto.ImageURL != null)
                carPart.ImageURL = updateCarPartDto.ImageURL;

            if (updateCarPartDto.Code != null)
                carPart.Code = updateCarPartDto.Code;

            if (updateCarPartDto.Description != null)
                carPart.Description = updateCarPartDto.Description;

            await _carPartsRepository.UpdateAsync(carPart);

            return Ok(GetCarPartDto(carPart));
        }

        // /api/v1/brands/{brandId}/cars/{carId}
        [HttpDelete]
        [Route("brands/{brandId}/cars/{carId}/carParts/{carPartId}")]
        public async Task<ActionResult> Remove(int carPartId, int carId)
        {
            var carPart = await _carPartsRepository.GetAsync(carId, carPartId);

            // 404
            if (carPart == null)
            {
                return NotFound();
            }

            await _carPartsRepository.DeleteAsync(carPart);

            //204
            return NoContent();
        }

        [HttpGet]
        [Route("cars/{carId}/carParts")]
        public async Task<IEnumerable<CarPartDto>> GetManyCarPartsByCar()
        {
            var carParts = await _carPartsRepository.GetManyAsync();

            IEnumerable<CarPartDto> carPartsDto = carParts.Select(x => GetCarPartDto(x));

            return carPartsDto;
        }

        public CarPartDto GetCarPartDto(CarPart carPart)
        {
            return new CarPartDto(carPart.Id, carPart.Name, carPart.ImageURL, carPart.Code, carPart.Description, carPart.CarId);
        }
    }
}
