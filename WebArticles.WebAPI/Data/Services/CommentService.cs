using AutoMapper;
using DataModel.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data.Repositories;
using WebAPI.Infrastructure;
using WebArticles.WebAPI.Data.Dto;
using WebArticles.WebAPI.Data.Models;

namespace WebArticles.WebAPI.Data.Services
{
    public class CommentService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public CommentService(IRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public async Task<PaginatorAnswer<CommentModel>> GetPage(long articleId, PaginatorQuery paginatorQuery)
        {
            IQueryable<Comment> query = _repository.GetAll<Comment>()
                                                        .Include(c => c.Article)
                                                        .Include(c => c.Reviewer)
                                                            .ThenInclude(r => r.User);

            query = query.Where(c => c.ArticleId == articleId);

            // sorting
            bool desc = paginatorQuery.SortDirection == "desc";
            switch (paginatorQuery.SortBy)
            {
                case "publichDate": query = desc ? query.OrderByDescending(a => a.PublichDate) : query.OrderBy(a => a.PublichDate); break;
                case "rating": query = desc ? query.OrderByDescending(a => a.Rating) : query.OrderBy(a => a.Rating); break;
                default: query = desc ? query.OrderByDescending(a => a.Id) : query.OrderBy(a => a.Id); break;
            }

            return new PaginatorAnswer<CommentModel> { Total = await query.CountAsync(), Items = await query.GetPage(paginatorQuery.Page, paginatorQuery.PageSize).MapWithAsync<CommentModel, Comment>(_mapper) };
        }

        public async Task<PaginatorAnswer<CommentModel>> GetPageByUserId(long userId, PaginatorQuery paginatorQuery)
        {
            IQueryable<Comment> query = _repository.GetAll<Comment>()
                                                        .Include(c => c.Article)
                                                        .Include(c => c.Reviewer)
                                                            .ThenInclude(r => r.User);

            query = query.Where(c => c.Reviewer.UserId == userId);

            return new PaginatorAnswer<CommentModel> { Total = await query.CountAsync(), Items = await query.GetPage(paginatorQuery.Page, paginatorQuery.PageSize).MapWithAsync<CommentModel, Comment>(_mapper) };
        }

        public async Task<UpdateAnswer> UpdateComment(CommentUpdate commentUpdate)
        {
            try
            {
                var comment = await _repository.GetAll<Comment>().FirstOrDefaultAsync(c => c.Id == commentUpdate.Id);

                comment.Content = commentUpdate.NewContent;
                _repository.Update(comment);
                _repository.SaveChanges();

                return new UpdateAnswer { Succeeded = true };
            } catch (Exception e)
            {
                return new UpdateAnswer { Succeeded = false, Error = "Comment not found" };
            }
        }

        public async Task<UpdateAnswer> DeleteComment(long id)
        {
            try
            {
                var comment = await _repository.GetAll<Comment>().FirstOrDefaultAsync(c => c.Id == id);

                _repository.Delete(comment);
                _repository.SaveChanges();

                return new UpdateAnswer { Succeeded = true };
            }
            catch (Exception e)
            {
                return new UpdateAnswer { Succeeded = false, Error = "Comment not found" };
            }
        }

        public async Task<CreateAnswer> CreateComment(CommentCreate commentCreate)
        {
            try
            {
                long reviewerId = (await _repository.GetAll<Reviewer>().FirstOrDefaultAsync(r => r.UserId == commentCreate.UserId)).Id;
                commentCreate.ReviewerId = reviewerId;

                var comment = _mapper.Map<Comment>(commentCreate);

                _repository.Insert(comment);
                _repository.SaveChanges();

                return new CreateAnswer { Succeeded = true, Id = comment.Id };
            }
            catch (Exception e)
            {
                return new CreateAnswer { Succeeded = false, Error = "Failed to create a comment" };
            }
        }

        public async Task<int> UpdateRating(long id, int rating)
        {
            var comment = await _repository.GetAll<Comment>().FirstOrDefaultAsync(c => c.Id == id);

            comment.Rating = rating;
            _repository.Update(comment);
            _repository.SaveChanges();

            return comment.Rating;
        }
    }
}
