namespace TrueHomeActivities.Api.Models
{
    public static class UsersRepository
    {
        public static readonly IList<User> Users = new List<User>()
        {
            new User() { UserName = "admin", Password = "test123" }
        };
    }
}
