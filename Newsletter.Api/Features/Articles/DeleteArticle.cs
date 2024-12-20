using MediatR;
using Microsoft.EntityFrameworkCore;
using Newsletter.Api.BuildingBlocks;
using Newsletter.Api.Database;

namespace Newsletter.Api.Features.Articles
{
    public class DeleteArticle
    {
        public class DeleteCommand : IRequest<Result<Guid>>
        {
            public Guid Id { get; set; }
        }

        public class DeleteHandler(ApplicationDbContext DbContext) : IRequestHandler<DeleteCommand, Result<Guid>>
        {

            public async Task<Result<Guid>> Handle(DeleteCommand request, CancellationToken cancellationToken)
            {
                var articleResponse = await DbContext
                    .Articles
                    .FirstOrDefaultAsync(article => article.Id == request.Id,cancellationToken);

                if (articleResponse is null)
                {
                    return Result.Failure<Guid>(new Error("GetArticle.Null",
                        "The article with the specified ID was not found"));
                }
                DbContext.Articles.Remove(articleResponse);
                await DbContext.SaveChangesAsync(cancellationToken);
                return articleResponse.Id;
            }
        }
    }

    public static class DeleteArticleEndpoint
    {
        public static void AddRoutes(this IEndpointRouteBuilder app)
        {
            app.MapDelete("api/articles/{id}", async (Guid id,ISender sender) =>
            {
                var deleteCommand = new DeleteArticle.DeleteCommand() { Id = id };
                var result = await sender.Send(deleteCommand);
                if (result.IsFailure)
                {
                    return Results.BadRequest(result.Error);
                }
                return Results.Ok(result.Value);
            });
        }
    }
}
