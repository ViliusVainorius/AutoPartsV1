namespace AutoPartsV1.Data.Dtos.CarParts
{
    public record CarPartDto(int id, string Name, string ImageURL, string Code, string Description, int CarId);
    public record CreateCarPartDto(string Name, string ImageURL, string Code, string Description);
    public record UpdateCarPartDto(string Name, string ?ImageURL, string ?Code, string ?Description);
    
}

/*
 * public int Id { get; set; }
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int CarId { get; set; }
 */
