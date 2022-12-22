using System.ComponentModel.DataAnnotations;
using AutoPartsV1.Auth.Model;

namespace AutoPartsV1.Data.Entities
{
    public class Brand : IUserOwnedResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public string UserId { get; set; }
        public AutoPartsV1RestUser User { get; set; }
    }
}
