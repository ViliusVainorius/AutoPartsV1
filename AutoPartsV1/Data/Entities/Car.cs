namespace AutoPartsV1.Data.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ImageURL { get; set; }
        public string EngineSize { get; set; }
        public string FuelType { get; set; }
        public string CreationYear { get; set; }
        public int BrandId { get; set; }

    }
}
