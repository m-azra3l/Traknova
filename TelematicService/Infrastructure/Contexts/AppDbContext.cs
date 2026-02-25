using Microsoft.EntityFrameworkCore;
using System;

namespace TelematicService.Infrastructure.Contexts
{
    // Application DbContext for EF Core
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
    }
}
