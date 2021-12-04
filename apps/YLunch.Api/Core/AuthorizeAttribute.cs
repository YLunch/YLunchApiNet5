using System;
using System.Reflection;
using YLunch.Domain.DTO.UserModels;
using YLunch.Domain.ModelsAggregate.UserAggregate.Roles;

namespace YLunch.Api.Core
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AuthorizeAttribute : Microsoft.AspNetCore.Authorization.AuthorizeAttribute
    {
        public AuthorizeAttribute(string roles)
        {
            Roles = roles;
        }

        public AuthorizeAttribute()
        {
            Roles = string.Join(",", UserRoles.GetList());
        }
    }
}
