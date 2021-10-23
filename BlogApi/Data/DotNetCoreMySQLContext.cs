using System;
using BlogApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Data
{
    public class DotNetCoreMySQLContext : DbContext
    {
        public DbSet<Post> Post { get; set; }
        public DotNetCoreMySQLContext(DbContextOptions<DotNetCoreMySQLContext> options) : base(options)
        {

        }
    }
}
