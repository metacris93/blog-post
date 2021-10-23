using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using BlogApi.Controllers;
using BlogApi.Data;
using BlogApi.Models;
using BlogApi.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace BlogApi.Test
{
    public class PostUnitTestController
    {
        private readonly ITestOutputHelper output;
        private readonly PostController controller;
        private readonly DotNetCoreMySQLContext dbContext;
        private readonly DummyDataDBInitializer dummyData;
        private readonly PostRepository repository;
        public static DbContextOptions<DotNetCoreMySQLContext> dbContextOptions { get; }
        public static string connectionString = "server=localhost;port=3306;user=admin;password=\"nuevo-admin\";database=challenge";

        static PostUnitTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<DotNetCoreMySQLContext>()
                .UseMySQL(connectionString).Options;
        }
        public PostUnitTestController(ITestOutputHelper output)
        {
            this.output = output;
            dbContext = new DotNetCoreMySQLContext(dbContextOptions);
            dummyData = new DummyDataDBInitializer();
            repository = new PostRepository(dbContext);
            controller = new PostController(repository);
        }
        [Fact]
        public async void Task_GetAllPosts_OkResult()
        {
            dummyData.Seed(dbContext);
            var data = await controller.GetPosts();
            Assert.IsType<JsonResult>(data);
            Assert.NotNull(data);
            Assert.Equal(200, data.StatusCode);
        }
        [Fact]
        public async void Task_GetAllPosts_NotFound()
        {
            dummyData.InitTable(dbContext);
            var data = await controller.GetPosts();
            Assert.IsType<JsonResult>(data);
            Assert.Equal(404, data.StatusCode);
            Assert.IsType<List<Post>>(data.Value);
            var posts = data.Value as List<Post>;
            Assert.NotNull(posts);
            Assert.Empty(posts);
        }
        [Fact]
        public async void Task_GetPost_OkResult()
        {
            dummyData.Seed(dbContext);
            var list = await controller.GetPosts();
            var posts = list.Value as List<Post>;
            Assert.NotNull(posts);
            var post = posts.FirstOrDefault();
            Assert.NotNull(post);
            var data = await controller.GetPost(post.Id);
            Assert.IsType<JsonResult>(data);
            Assert.Equal(200, data.StatusCode);
            var newPost = data.Value as Post;
            Assert.NotNull(newPost);
        }
        [Fact]
        public async void Task_GetPost_Negative_NotFound()
        {
            int id = -1;
            dummyData.Seed(dbContext);
            var data = await controller.GetPost(id);
            Assert.IsType<JsonResult>(data);
            Assert.Equal(404, data.StatusCode);
            Assert.Null(data.Value);
        }
        [Fact]
        public async void Task_GetPost_Null_NotFound()
        {
            int? id = null;
            dummyData.Seed(dbContext);
            var data = await controller.GetPost(id);
            Assert.IsType<JsonResult>(data);
            Assert.Equal(400, data.StatusCode);
            Assert.Null(data.Value);
        }
        [Fact]
        public async void Task_AddPost_ValidData_Return_OkResult()
        {
            dummyData.InitTable(dbContext);
            var currentDate = DateTime.Now;
            var post = new Post() { Title="New Title", Created_at = currentDate, Content = "New Content", Image = "http://example.net/photo.png" };
            var res = await controller.AddPost(post);
            Assert.IsType<JsonResult>(res);
            Assert.Equal(201, res.StatusCode);
            var data = await controller.GetPost(1);
            var p = data.Value as Post;
            Assert.NotNull(p);
            Assert.Equal(post.Title, p.Title);
            Assert.Equal(post.Created_at, p.Created_at);
            Assert.Equal(post.Content, p.Content);
            Assert.Equal(post.Image, p.Image);
        }
        [Fact]
        public async void Task_AddPost_InValidData()
        {
            dummyData.InitTable(dbContext);
            var currentDate = DateTime.Now;
            var post = new Post() { Title = null, Created_at = currentDate, Content = null, Image = null };
            var res = await controller.AddPost(post);
            Assert.IsType<JsonResult>(res);
            Assert.Equal(500, res.StatusCode);
            var data = await controller.GetPosts();
            var list = data.Value as List<Post>;
            Assert.NotNull(list);
            Assert.Empty(list);
        }
        [Fact]
        public async void Task_UpdatePost_ValidData()
        {
            dummyData.Seed(dbContext);
            var data = await controller.GetPost(1);
            var post = data.Value as Post;
            Assert.NotNull(post);
            post.Title = "Title updated";
            post.Updated_at = DateTime.Now;
            post.Content = "Content updated";
            post.Image = "http://example.net/photo.png";
            var updatedData = await controller.UpdatePost(post);
            Assert.IsType<JsonResult>(updatedData);
            Assert.Equal(200, updatedData.StatusCode);
        }
        [Fact]
        public async void Task_UpdatePost_InvalidData()
        {
            dummyData.Seed(dbContext);
            var data = await controller.GetPost(1);
            var post = data.Value as Post;
            Assert.NotNull(post);
            post.Title = null;
            post.Updated_at = null;
            post.Content = null;
            post.Image = null;
            var updatedData = await controller.UpdatePost(post);
            Assert.IsType<JsonResult>(updatedData);
            Assert.Equal(500, updatedData.StatusCode);
        }
        [Fact]
        public async void Task_DeletePost_ValidData()
        {
            dummyData.Seed(dbContext);
            var data = await controller.GetPost(1);
            var post = data.Value as Post;
            Assert.NotNull(post);
            var deletedData = await controller.DeletePost(post.Id);
            Assert.Equal(200, deletedData.StatusCode);
        }
        [Fact]
        public async void Task_DeletePost_InvalidData()
        {
            JsonResult res;
            dummyData.Seed(dbContext);
            res = await controller.DeletePost(null);
            Assert.Equal(400, res.StatusCode);

            res = await controller.DeletePost(-1);
            Assert.Equal(404, res.StatusCode);
        }
    }
}
