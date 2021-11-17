using System;
using System.Collections.Generic;
using YLunch.Domain.ModelsAggregate.CustomerAggregate;
using YLunch.Domain.ModelsAggregate.UserAggregate;

namespace YLunch.Application.Tests.Mocks
{
    public static class UsersMock
    {
        public static readonly User SUPER_ADMIN = new()
        {
            Id = "111",
            UserName = "SUPERADMIN@YNOV.COM",
            Firstname = "superadmin-firstname",
            Lastname = "superadmin-lastname",
            Email = null,
            PhoneNumber = null,
            EmailConfirmed = false,
            PhoneNumberConfirmed = false,
            CreationDateTime = DateTime.Parse("2021-10-31T14:34:46.0431306"),
            IsAccountActivated = false
        };

        public static readonly User CUSTOMER = new()
        {
            Id = "222",
            UserName = "CUSTOMER@YNOV.COM",
            Firstname = "customer_firstname",
            Lastname = "customer_lastname",
            Email = "customer@ynov.com",
            PhoneNumber = "0612345678",
            EmailConfirmed = false,
            PhoneNumberConfirmed = false,
            CreationDateTime = DateTime.Parse("2021-10-31T14:34:46.0431306"),
            IsAccountActivated = false
        };

        public static readonly List<User> USERS = new()
        {
            SUPER_ADMIN,
            CUSTOMER
        };
    }
}
