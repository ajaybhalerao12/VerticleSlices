using MediatR;
using Microsoft.EntityFrameworkCore;
using Newsletter.Api.BuildingBlocks;
using Newsletter.Api.Contracts;
using Newsletter.Api.Database;
using static Newsletter.Api.Features.Articles.UpdateArticle;

namespace Newsletter.Api.Features.Articles
{
    public class UpdateArticle
    {
        //public record Command(Guid Id, string Title, string Content, List<string> Tags)
        //    :IRequest<Result>;

        public class Command : IRequest<Result>
        {
            public Guid Id { get; set; }
            public string Title { get; set; } = string.Empty;

            public string Content { get; set; } = string.Empty;

            public List<string> Tags { get; set; } = new();
        }
        public class Handler(ApplicationDbContext DbContext) : IRequestHandler<Command, Result>
        {
            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                //var articleResponse = await DbContext
                //    .Articles
                //    .AsNoTracking()
                //    .Where(article=>article.Id == request.Id)
                //    .FirstOrDefaultAsync(cancellationToken);
                var articleResponse = await DbContext.Articles.FindAsync(request.Id);
                if (articleResponse == null)
                {
                    return Result.Failure(new Error(
                          "GetArticle.Null",
                    "The article with the specified ID was not found"));
                }

                articleResponse.Title = request.Title;
                articleResponse.Content = request.Content;
                articleResponse.Tags = request.Tags;

                await DbContext.SaveChangesAsync(cancellationToken);
                return Result.Success(articleResponse);
            }
        }

    }

    public static class UpdateRequestEndpoint
    {
        public static void AddRoutes(this IEndpointRouteBuilder app)
        {
            app.MapPut("/api/articles/{id}", async (Guid id, UpdateArticleRequest request,
                ISender sender) =>
            {

                if (id != request.Id)
                {
                    return Results.BadRequest($"Article with Id {request.Id} not found.");
                }
                var command = new UpdateArticle.Command()
                {
                    Id = request.Id,
                    Title = request.Title,
                    Content = request.Content,
                    Tags = request.Tags
                };
                var result = await sender.Send(command);
                return result.IsSuccess ?
                 Results.NoContent() : Results.BadRequest(result.Error);
            });
        }
    }
}
