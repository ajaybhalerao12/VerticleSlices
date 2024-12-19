using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newsletter.Api.BuildingBlocks;
using Newsletter.Api.Contracts;
using Newsletter.Api.Database;
using Newsletter.Api.Entities;

namespace Newsletter.Api.Features.Articles
{
    public static class CreateArticle
    {
        public class CreateArticleCommand : IRequest<Result<Guid>>
        {
            public string Title { get; set; } = string.Empty;

            public string Content { get; set; } = string.Empty;

            public List<string> Tags { get; set; } = new();
        }

        public sealed class CreateArticleHandler : IRequestHandler<CreateArticleCommand, Result<Guid>>
        {
            private readonly ApplicationDbContext _dbContext;

            public CreateArticleHandler(ApplicationDbContext dbContext)
            {
                _dbContext = dbContext;
            }
            public async Task<Result<Guid>> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
            {
                var article = new Article()
                {
                    Id = Guid.NewGuid(),
                    Title = request.Title,
                    Content = request.Content,
                    Tags = request.Tags,
                    CreatedOnUtc = DateTime.UtcNow,
                };
                _dbContext.Articles.Add(article);
                await _dbContext.SaveChangesAsync();
                return article.Id;
            }
        }

    }

    public static class CreateArticleEndpoint
    {
        public static void AddRoutes(this IEndpointRouteBuilder app)
        {

            app.MapGroup("/hello")
                .MapGet("/", () => "Get all");

            app.MapGet("/", () => "Hello World!");
            app.MapPost("/api/articles", async ([FromBody] CreateArticleRequest request, ISender sender) =>
            {
                //var cmd = new CreateArticle.Command()
                //{
                //    Title = request.Title,
                //    Content = request.Content,
                //    Tags = request.Tags
                //};
                var command = request.Adapt<CreateArticle.CreateArticleCommand>();
                var result = await sender.Send(command);
                return Results.Ok(result);
            }).WithName("CreateArticle");
        }
    }

}
