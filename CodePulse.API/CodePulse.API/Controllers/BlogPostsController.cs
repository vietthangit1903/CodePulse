using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTOs;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly ICategoryRepository categoryRepository;

        public BlogPostsController(IBlogPostRepository blogPostRepository, ICategoryRepository categoryRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.categoryRepository = categoryRepository;
        }

        //POST: {apiBaseURL}/api/blogposts
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
                Categories = new List<Category>()
            };

            foreach (var categoryId in createBlogPostRequest.Categories)
            {
                var existingCategory = await categoryRepository.FindByIdAsync(categoryId);
                if (existingCategory is not null)
                {
                    blogPost.Categories.Add(existingCategory);
                }
            }

            blogPost = await blogPostRepository.CreateAsync(blogPost);

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
                Categories = blogPost.Categories.Select(x => new CategoryDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,
                }).ToList()
            };
            return Ok(response);
        }

        //GET: {apiBaseURL}/api/blogposts
        [HttpGet]
        public async Task<IActionResult> GetAllBlogPost()
        {
            var blogPosts = await blogPostRepository.GetAllAsync();
            var response = new List<BlogPostDTO>();
            foreach (var blogPost in blogPosts)
            {
                response.Add(new BlogPostDTO
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
                    Categories = blogPost.Categories.Select(x => new CategoryDTO
                    {
                        Id = x.Id,
                        Name = x.Name,
                        UrlHandle = x.UrlHandle,
                    }).ToList()
                });
            }
            return Ok(response);
        }

        //GET: {apiBaseURL}/api/blogposts/{id}
        [HttpGet]
        [Route("{postId:Guid}")]
        public async Task<IActionResult> GetBlogPostById([FromRoute] Guid postId)
        {
            var blogPost = await blogPostRepository.GetByIdAsync(postId);
            if (blogPost is null)
            {
                return NotFound();
            }

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
                Categories = blogPost.Categories.Select(x => new CategoryDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,
                }).ToList()
            };

            return Ok(response);
        }


        //PUT: {apiBaseURL}/api/blogposts/{id}
        [HttpPut]
        [Route("{postId:Guid}")]
        public async Task<IActionResult> UpdateBlogPostById([FromRoute] Guid postId, [FromBody] UpdateBlogPostRequestDTO updateBlogPostRequest)
        {
            var blogPost = await blogPostRepository.GetByIdAsync(postId);
            if (blogPost is null)
            {
                return NotFound();
            }

            blogPost.Title = updateBlogPostRequest.Title;
            blogPost.ShortDescription = updateBlogPostRequest.ShortDescription;
            blogPost.Content = updateBlogPostRequest.Content;
            blogPost.FeaturedImageUrl = updateBlogPostRequest.FeaturedImageUrl;
            blogPost.UrlHandle = updateBlogPostRequest.UrlHandle;
            blogPost.PublishedDate = updateBlogPostRequest.PublishedDate;
            blogPost.Author = updateBlogPostRequest.Author;
            blogPost.IsVisible = updateBlogPostRequest.IsVisible;
            blogPost.Categories = new List<Category>();
            foreach (var categoryId in updateBlogPostRequest.Categories)
            {
                var existingCategory = await categoryRepository.FindByIdAsync(categoryId);
                if (existingCategory is not null)
                {
                    blogPost.Categories.Add(existingCategory);
                }
            }

            var updatedBlogPost = await blogPostRepository.UpdateAsync(blogPost);

            if (updatedBlogPost is null)
            {
                return BadRequest("An error occured while updating blog post");
            }

            var response = new BlogPostDTO
            {
                Id = updatedBlogPost.Id,
                Title = updatedBlogPost.Title,
                ShortDescription = updatedBlogPost.ShortDescription,
                Content = updatedBlogPost.Content,
                FeaturedImageUrl = updatedBlogPost.FeaturedImageUrl,
                UrlHandle = updatedBlogPost.UrlHandle,
                PublishedDate = updatedBlogPost.PublishedDate,
                Author = updatedBlogPost.Author,
                IsVisible = updatedBlogPost.IsVisible,
                Categories = updatedBlogPost.Categories.Select(x => new CategoryDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,
                }).ToList()
            };

            return Ok(response);
        }

        //DELETE: {apiBaseURL}/api/blogposts/{id}
        [HttpDelete]
        [Route("{postId:Guid}")]
        public async Task<IActionResult> DeleteBlogPostById([FromRoute] Guid postId)
        {
            var deletedBlogPost = await blogPostRepository.DeleteAsync(postId);
            if (deletedBlogPost is null)
            {
                return NotFound();
            }
            var response = new BlogPostDTO
            {
                Id = deletedBlogPost.Id,
                Title = deletedBlogPost.Title,
                ShortDescription = deletedBlogPost.ShortDescription,
                Content = deletedBlogPost.Content,
                FeaturedImageUrl = deletedBlogPost.FeaturedImageUrl,
                UrlHandle = deletedBlogPost.UrlHandle,
                PublishedDate = deletedBlogPost.PublishedDate,
                Author = deletedBlogPost.Author,
                IsVisible = deletedBlogPost.IsVisible,
            };
            return Ok(response);
        }
    }
}
