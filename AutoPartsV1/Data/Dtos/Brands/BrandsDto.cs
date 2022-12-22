namespace AutoPartsV1.Data.Dtos.Brands
{
    public record BrandDto(int Id, string Name, string Description);
    public record CreateBrandDto(string Name, string Description);
    public record UpdateBrandDto(string Name, string Description);
}
