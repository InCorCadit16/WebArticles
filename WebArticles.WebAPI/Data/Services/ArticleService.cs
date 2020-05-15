using AutoMapper;
using DataModel.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data.Repositories;
using WebAPI.Infrastructure;
using WebArticles.WebAPI.Data.Dto;
using WebArticles.WebAPI.Data.Models;

namespace WebArticles.WebAPI.Data.Services
{
    
    public class ArticleService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public ArticleService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ArticleModel> GetArticleById(long id)
        {
            IQueryable<Article> query = _repository.GetAll<Article>();

            query = query.Include(a => a.Topic)
                            .Include(a => a.Writer)
                            .ThenInclude(w => w.User);

            return _mapper.Map<ArticleModel>(await query.FirstOrDefaultAsync(a => a.Id == id));
        }


        public async Task<PaginatorAnswer<ArticlePreview>> GetPage(PaginatorQuery queryDto)
        {
            IQueryable<Article> query = _repository.GetAll<Article>();

            query = query.Include(a => a.Topic)
                         .Include(a => a.Writer)
                            .ThenInclude(w => w.User);

            // search string
            if (!string.IsNullOrEmpty(queryDto.Search) && !string.IsNullOrWhiteSpace(queryDto.Search))
            {
                query = query.Where(a => a.Title.Contains(queryDto.Search));
            }

            // sorting
            bool desc = queryDto.SortDirection == "desc";
            switch (queryDto.SortBy)
            {
                case "publichDate": query = desc ? query.OrderByDescending(a => a.PublichDate) : query.OrderBy(a => a.PublichDate); break;
                case "rating": query = desc ? query.OrderByDescending(a => a.Rating) : query.OrderBy(a => a.Rating); break;
                case "title": query = desc ? query.OrderByDescending(a => a.Title) : query.OrderBy(a => a.Title); break;
                default: query = desc ? query.OrderByDescending(a => a.Id) : query.OrderBy(a => a.Id); break;
            }

            // filtering and returning results
            if (!queryDto.Filters.IsEmpty)
            {
               
                Article[] filtered = await query.FilterAsync(queryDto.Filters);
                return new PaginatorAnswer<ArticlePreview> { Total = filtered.Count(), Items = await filtered.AsQueryable().GetPage(queryDto.Page,10).MapWithAsync<ArticlePreview, Article>(_mapper) };
            }
            else
                return new PaginatorAnswer<ArticlePreview> { Total = await query.CountAsync(), Items = await query.GetPage(queryDto.Page, 10).MapWithAsync<ArticlePreview, Article>(_mapper) };
        }

        
        public async Task<PaginatorAnswer<ArticlePreview>> GetPageByUserId(long id, int page)
        {
            IQueryable<Article> query = _repository.GetAll<Article>().Include(a => a.Topic)
                                                                     .Include(a => a.Writer)
                                                                        .ThenInclude(w => w.User);

            query = query.Where(a => a.Writer.UserId == id);

            return new PaginatorAnswer<ArticlePreview> { Total = await query.CountAsync(), Items = await query.GetPage(page, 5).MapWithAsync<ArticlePreview, Article>(_mapper) };
        }

        public async Task<UpdateAnswer> UpdateArticle(ArticleModel model)
        {

            IQueryable<Article> query = _repository.GetAll<Article>();

            query = query.AsNoTracking()
                            .Include(a => a.Topic)
                            .Include(a => a.Writer)
                                .ThenInclude(w => w.User);

            var initialArticle = await query.FirstOrDefaultAsync(a => a.Id == model.Id);

            var updatedArticle = _mapper.Map(model, initialArticle);

            try
            {
                _repository.Update(updatedArticle);
                _repository.SaveChanges();

                return new UpdateAnswer { Succeeded = true };
            } catch (Exception e)
            {
                return new UpdateAnswer { Succeeded = false, Error = e.Message };
            }
        }

        public async Task<CreateAnswer> CreateArticle(ArticleCreate articleCreate)
        {
            try
            {
                var article = _mapper.Map<Article>(articleCreate);

                article.WriterId = (await _repository.GetAll<User>()
                                            .Include(u => u.Writer)
                                            .FirstOrDefaultAsync(u => u.Id == article.WriterId)).Writer.Id;

                _repository.Insert(article);
                _repository.SaveChanges();
                return new CreateAnswer { Succeeded = true, Id = article.Id };
            } catch (Exception e)
            {
                return new CreateAnswer { Succeeded = false, Error = e.Message };
            }
            
        }

        public async Task<int> UpdateRating(long id, int rating)
        {
            var article = await _repository.GetAll<Article>().FirstOrDefaultAsync(a => a.Id == id);

            article.Rating = rating;
            _repository.Update(article);
            _repository.SaveChanges();

            return article.Rating;
        }
        
        public async Task<UpdateAnswer> DeleteArticle(long id)
        {
            try
            {
                var article = await _repository.GetAll<Article>().FirstOrDefaultAsync(a => a.Id == id);

                _repository.Delete(article);
                _repository.SaveChanges();
                return new UpdateAnswer { Succeeded = true };
            } catch (Exception e)
            {
                return new UpdateAnswer { Succeeded = false, Error = "Faild to delete article" };
            }
        }
    }
}
