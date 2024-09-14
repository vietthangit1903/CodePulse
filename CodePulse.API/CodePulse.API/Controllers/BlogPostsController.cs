using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTOs;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostRepository blogPostRepository;

        public BlogPostsController(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlogPostAsync([FromBody] CreateBlogPostRequestDTO createBlogPostRequest)
        {
            var blogPost = new BlogPost
            {
                Title = createBlogPostRequest.Title,
                ShortDescription = createBlogPostRequest.ShortDescription,
                Content = createBlogPostRequest.Content,
                FeaturedImageUrl = createBlogPostRequest.FeaturedImageUrl,
                UrlHandle = createBlogPostRequest.UrlHandle,
                PublishedDate = createBlogPostRequest.PublishedDate,
                Author = createBlogPostRequest.Author,
                IsVisible = createBlogPostRequest.IsVisible,
            };
            blogPost =  await blogPostRepository.CreateAsync(blogPost);

            var response = new BlogPostDTO
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                ShortDescription = blogPost.ShortDescription,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                UrlHandle = blogPost.UrlHandle,
                PublishedDate = blogPost.PublishedDate,
                Author = blogPost.Author,
                IsVisible = blogPost.IsVisible,
            };
            return Ok(response);
        }
    }
}
