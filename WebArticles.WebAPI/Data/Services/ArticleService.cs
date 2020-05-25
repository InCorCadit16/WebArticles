using AutoMapper;
using WebArticles.DataModel.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebArticles.WebAPI.Data.Dtos;
using WebArticles.WebAPI.Data.Repositories.Implementations;
using WebArticles.WebAPI.Infrastructure.Models;
using System.Linq;

namespace WebArticles.WebAPI.Data.Services
{
    
    public class ArticleService
    {
        private readonly ArticleRepository _repository;
        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper;

        public ArticleService(ArticleRepository repository, UserRepository userRepository, IMapper mapper)
        {
            _repository = repository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ArticleDto> GetArticleById(long id)
        {
            var article = await _repository.GetById(id, a => a.Topic, a => a.Writer, a => a.Writer.User, a => a.UserArticleMarks);

            return _mapper.Map<ArticleDto>(article);
        }

        public async Task<PaginatorAnswer<ArticlePreviewDto>> GetArticlesPage(PaginatorQuery paginatorQuery)
        {
            return await _repository.GetPage<ArticlePreviewDto>(paginatorQuery, a => a.Topic, a => a.Writer, a => a.Writer.User, a => a.UserArticleMarks);
        }

        
        public async Task<PaginatorAnswer<ArticlePreviewDto>> GetUserArticlesPage(long userId, PaginatorQuery paginatorQuery)
        {
            return await _repository.GetUserArticlesPage<ArticlePreviewDto>(userId, paginatorQuery, a => a.Topic, a => a.Writer, a => a.Writer.User, a => a.UserArticleMarks);
        }

        public async Task UpdateArticle(ArticleUpdateDto updateDto)
        {
            var initialArticle = await _repository.GetById(updateDto.Id, a => a.Topic, a => a.Writer, a => a.Writer.User);
            var updatedArticle = _mapper.Map(updateDto, initialArticle);

            await _repository.Update(updatedArticle);
        }

        public async Task<long> CreateArticle(ArticleCreateDto articleCreate)
        {
            var article = _mapper.Map<Article>(articleCreate);

            var user = await _userRepository.GetById(articleCreate.UserId, u => u.Writer);
            article.WriterId =  user.Writer.Id;

            await _repository.Insert(article);
            return article.Id;
        }

        public async Task<int> GetUserArticleMark(long userId, long articleId)
        {
            var article = await _repository.GetById(articleId, a => a.UserArticleMarks);
            var userArticleMark = article.UserArticleMarks.FirstOrDefault(uam => uam.UserId == userId);

            if (userArticleMark != null)
                return userArticleMark.Mark ? 1 : -1;

            return 0;
        }

        public async Task<int> UpdateRating(long userId, long articleId, int mark)
        {
            var article = await _repository.GetById(articleId, c => c.UserArticleMarks);
            var userCommentMark = article.UserArticleMarks.FirstOrDefault(uam => uam.UserId == userId);

            if (userCommentMark == null)
                article.UserArticleMarks.Add(new UserArticleMark { ArticleId = articleId, UserId = userId, Mark = (mark == 1) });
            else if (mark == 0)
                article.UserArticleMarks.Remove(userCommentMark);
            else
                userCommentMark.Mark = (mark == 1);

            await _repository.SaveAllChanges();
            return article.Rating;
        }

        public async Task DeleteArticle(long id)
        {
            await _repository.Delete(id);
        }
    }
}
