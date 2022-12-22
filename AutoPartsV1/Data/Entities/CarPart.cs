namespace AutoPartsV1.Data.Entities
{
    public class CarPart
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int CarId { get; set; }
    }
}
