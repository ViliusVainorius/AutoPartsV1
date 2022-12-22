namespace AutoPartsV1.Data.Dtos.Cars
{
    public record CarDto(int id, string Name, string ImageURL, string EngineSize, string FuelType, string CreationYear, int BrandId);
    public record CreateCarDto(string Name, string? ImageURL, string EngineSize, string FuelType, string CreationYear, int BrandId);
    public record UpdateCarDto(string? Name, string? ImageURL, string EngineSize, string FuelType, string CreationYear, int BrandId);
}

/*
 * int Id { get; set; }
        public string Name { get; set; }
        public string? ImageURL { get; set; }
        public string EngineSize { get; set; }
        public string FuelType { get; set; }
        public string CreationYear { get; set; }
        public int BrandId { get; set; }
 * */
