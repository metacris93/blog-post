using System;
using BlogApi.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using BlogApi.Models;
using System.Threading.Tasks;
using BlogApi.Repository;
using Microsoft.AspNetCore.Http;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController
    {
        private static readonly List<Post> defaultPosts = new List<Post>();
        private readonly IPostRepository _postRepository;
        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        [HttpGet]
        [Route("GetPosts")]
        public async Task<JsonResult> GetPosts()
        {
            try
            {
                var posts = await _postRepository.GetPosts();
                if (posts == null || posts.Count() == 0)
                {
                    return new JsonResult(defaultPosts)
                    {
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }
                return new JsonResult(posts) {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch// (Exception ex)
            {
                return new JsonResult(defaultPosts)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
        [HttpGet("{id}")]
        [Route("GetPost")]
        public async Task<JsonResult> GetPost(int? id)
        {
            if (id == null)
            {
                return new JsonResult(null)
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            try
            {
                var post = await _postRepository.GetPost(id);
                if (post == null)
                {
                    return new JsonResult(null)
                    {
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }
                return new JsonResult(post)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new JsonResult(null)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
        [HttpPost]
        [Route("AddPost")]
        public async Task<JsonResult> AddPost([FromBody]Post model)
        {
            try
            {
                var result = await _postRepository.AddPost(model);
                if (result)
                {
                    return new JsonResult(null)
                    {
                        StatusCode = StatusCodes.Status201Created
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new JsonResult(null)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
        [HttpDelete]
        [Route("DeletePost")]
        public async Task<JsonResult> DeletePost(int? id)
        {
            int result = 0;
            if (id == null)
            {
                return new JsonResult(null)
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            try
            {
                result = await _postRepository.DeletePost(id);
                if (result == 0)
                {
                    return new JsonResult(null)
                    {
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }
                return new JsonResult(null)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch //(Exception ex)
            {
                return new JsonResult(null)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
        [HttpDelete]
        [Route("UpdatePost")]
        public async Task<JsonResult> UpdatePost([FromBody]Post model)
        {
            try
            {
                await _postRepository.UpdatePost(model);
                return new JsonResult(null)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch //(Exception ex)
            {
                return new JsonResult(null)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
    }
}
