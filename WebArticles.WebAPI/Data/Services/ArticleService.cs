using AutoMapper;
using DataModel.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebArticles.WebAPI.Data.Dtos;
using WebArticles.WebAPI.Data.Repositories.Implementations;
using WebArticles.WebAPI.Infrastructure.Models;

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
            var article = await _repository.GetById(id, a => a.Topic, a => a.Writer, a => a.Writer.User);

            return _mapper.Map<ArticleDto>(article);
        }

        public async Task<PaginatorAnswer<ArticlePreviewDto>> GetArticlesPage(PaginatorQuery paginatorQuery)
        {
            return await _repository.GetPage<ArticlePreviewDto>(paginatorQuery, a => a.Topic, a => a.Writer, a => a.Writer.User);
        }

        
        public async Task<PaginatorAnswer<ArticlePreviewDto>> GetUserArticlesPage(long userId, PaginatorQuery paginatorQuery)
        {
            return await _repository.GetUserArticlesPage<ArticlePreviewDto>(userId, paginatorQuery, a => a.Topic, a => a.Writer, a => a.Writer.User);
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

        public async Task<int> UpdateRating(long id, int rating)
        {
            var article = await _repository.GetById(id);

            article.Rating = rating;
            await _repository.Update(article);

            return article.Rating;
        }
        
        public async Task DeleteArticle(long id)
        {
            await _repository.Delete(id);
        }
    }
}
