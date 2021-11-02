using System;
using Microsoft.EntityFrameworkCore;
using YLunch.Infrastructure.Database;

namespace YLunch.Application.Tests
{
    public static class ContextBuilder
    {
        public static ApplicationDbContext BuildContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: $"YLunchDatabase-{Guid.NewGuid()}")
                .Options;
            return new ApplicationDbContext(options);
        }
    }
}
