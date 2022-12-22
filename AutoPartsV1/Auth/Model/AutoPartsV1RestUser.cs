using Microsoft.AspNetCore.Identity;

namespace AutoPartsV1.Auth.Model
{
    public class AutoPartsV1RestUser : IdentityUser
    {
        [PersonalData]
        public string? UserData { get; set; }
    }
}
