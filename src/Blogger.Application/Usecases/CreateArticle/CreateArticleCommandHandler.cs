﻿namespace Blogger.Application.Usecases.CreateArticle;

public class CreateArticleCommandHandler(IArticleRepository articleRepository) : IRequestHandler<CreateArticleCommand, CreateArticleCommandResponse>
{
    public async Task<CreateArticleCommandResponse> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        var articleId = ArticleId.CreateUniqueId(request.Title);
        if (await articleRepository.HasIdAsync(articleId, cancellationToken))
        {
            throw new ArticleAlreadyExistsException(articleId.ToString());
        }

        var article = Article.CreateArticle(request.Title, request.Body, request.Summary);
        article.AddTags(request.Tags);
         
        await articleRepository.CreateAsync(article, cancellationToken);
        await articleRepository.SaveChangesAsync(cancellationToken);
    
        return new CreateArticleCommandResponse(article.Id);
    }
}
