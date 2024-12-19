using MediatR;
using Microsoft.EntityFrameworkCore;
using Newsletter.Api.BuildingBlocks;
using Newsletter.Api.Contracts;
using Newsletter.Api.Database;

namespace Newsletter.Api.Features.Articles
{
    public class GetArticle
    {
        public class GetArticleQuery : IRequest<Result<ArticleResponse>>
        {
            public Guid Id { get; set; }
        }
        public class GetArticleHandler(ApplicationDbContext DbContext) : IRequestHandler<GetArticleQuery, Result<ArticleResponse>>
        {
            public async Task<Result<ArticleResponse>> Handle(GetArticleQuery request, CancellationToken cancellationToken)
            {
                var articleResponse = await DbContext
                    .Articles
                    .AsNoTracking()
                    .Where(article => article.Id == request.Id)
                    .Select(article => new ArticleResponse
                    {
                        Id = article.Id,
                        Title = article.Title,
                        Content = article.Content,
                        Tags = article.Tags,
                        CreatedOnUtc = article.CreatedOnUtc,
                        PublishedOnUtc = article.PublishedOnUtc,

                    }).FirstOrDefaultAsync(cancellationToken);

                if (articleResponse is null) {
                    return Result.Failure<ArticleResponse>(new Error(
                          "GetArticle.Null",
                    "The article with the specified ID was not found"));
                }
                return articleResponse;
            }
        }
    }

    public static class GetArticleEndpoint
    {
        public static void AddRoutes(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/articles/{id}", async (Guid id, ISender sender) =>
            {
                var query = new GetArticle.GetArticleQuery() { Id = id };
                var result = await sender.Send(query);
                if (result.IsFailure)
                {
                    return Results.NotFound(result.Error);
                }
                return Results.Ok(result.Value);
            });
        }
    }
}
