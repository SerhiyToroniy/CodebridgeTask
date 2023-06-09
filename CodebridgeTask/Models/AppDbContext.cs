﻿using Microsoft.EntityFrameworkCore;

namespace CodebridgeTask.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Dog> Dogs { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }

}
