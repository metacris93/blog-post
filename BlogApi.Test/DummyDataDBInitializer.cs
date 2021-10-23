using System;
using System.Collections.Generic;
using BlogApi.Data;
using BlogApi.Models;
using Bogus;

namespace BlogApi.Test
{
    public class DummyDataDBInitializer
    {
        public DummyDataDBInitializer()
        {
        }
        public void Seed(DotNetCoreMySQLContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Post.AddRange(FakeData());
            context.SaveChanges();
        }
        public void InitTable(DotNetCoreMySQLContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
        private List<Post> FakeData()
        {
            return new Faker<Post>()
                .RuleFor(o => o.Title, f => f.Company.CompanyName())
                .RuleFor(o => o.Created_at, f => DateTime.Now)
                .RuleFor(o => o.Content, f => f.Lorem.Lines())
                .RuleFor(o => o.Image, f => f.Image.PicsumUrl())
                .Generate(20);
        }
    }
}
