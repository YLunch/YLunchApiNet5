using System.Collections.Generic;
using System.Linq;

namespace YLunch.Domain.ModelsAggregate.UserAggregate.Roles
{
    public static class UserRoles
    {
        public const string SuperAdmin = "SuperAdmin";
        public const string Customer = "Customer";
        public const string RestaurantAdmin = "RestaurantAdmin";
        public const string Employee = "Employee";

        public static IEnumerable<string> GetList() => typeof(UserRoles).GetFields().Select(x => x.Name).ToList();
    }
}
