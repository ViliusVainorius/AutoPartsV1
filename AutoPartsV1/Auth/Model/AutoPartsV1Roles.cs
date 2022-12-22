namespace AutoPartsV1.Auth.Model
{
    public static class AutoPartsV1Roles
    {
        public const string Admin = nameof(Admin);
        public const string ForumUser = nameof(ForumUser);

        public static readonly IReadOnlyCollection<string> All = new[] { Admin, ForumUser };
    }
}
