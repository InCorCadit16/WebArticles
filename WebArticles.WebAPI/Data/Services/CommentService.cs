using AutoMapper;
using WebArticles.DataModel.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data.Repositories.Interfaces;
using WebAPI.Infrastructure;
using WebArticles.WebAPI.Data.Dtos;
using WebArticles.WebAPI.Data.Repositories.Implementations;
using WebArticles.WebAPI.Infrastructure.Models;

namespace WebArticles.WebAPI.Data.Services
{
    public class CommentService
    {
        private readonly CommentRepository _repository;
        
        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper;

        public CommentService(CommentRepository repository, UserRepository userRepository, IMapper mapper)
        {
            this._repository = repository;
            this._userRepository = userRepository;
            this._mapper = mapper;
        }

        public async Task<PaginatorAnswer<CommentDto>> GetArticleCommentsPage(long articleId, PaginatorQuery paginatorQuery)
        {
            return await _repository.GetArticlesCommentsPage<CommentDto>(articleId, paginatorQuery, c => c.Reviewer, c => c.Reviewer.User, c => c.UserCommentMarks);
        }

        public async Task<PaginatorAnswer<CommentDto>> GetUserCommentsPage(long userId, PaginatorQuery paginatorQuery)
        {
            return await _repository.GetUserCommentsPage<CommentDto>(userId, paginatorQuery, c => c.Reviewer, c => c.Reviewer.User, c => c.UserCommentMarks);
        }

        public async Task UpdateComment(CommentUpdateDto commentUpdate)
        {
            var comment = await _repository.GetById(commentUpdate.Id);

            comment.Content = commentUpdate.NewContent;
            comment.LastEditDate = commentUpdate.LastEditDate;
            await _repository.Update(comment);
        }

        public async Task DeleteComment(long id)
        {
            await _repository.Delete(id);
        }

        public async Task<long> CreateComment(CommentCreateDto commentCreate)
        {
            var user = await _userRepository.GetById(commentCreate.UserId, u => u.Reviewer);
            commentCreate.ReviewerId = user.Reviewer.Id;

            var comment = _mapper.Map<Comment>(commentCreate);

            await _repository.Insert(comment);
            return comment.Id;
        }

        public async Task<int> GetUserCommentMark(long userId, long commentId)
        {
            var comment = await _repository.GetById(commentId, c => c.UserCommentMarks);
            var userCommentMark = comment.UserCommentMarks.FirstOrDefault(uam => uam.UserId == userId);

            if (userCommentMark != null)
                return userCommentMark.Mark ? 1 : -1;

            return 0;
        }

        public async Task<int> UpdateRating(long userId, long commentId, int mark)
        {
            var comment = await _repository.GetById(commentId, c => c.UserCommentMarks);
            var userCommentMark = comment.UserCommentMarks.FirstOrDefault(uam => uam.UserId == userId);

            if (userCommentMark == null)
                comment.UserCommentMarks.Add(new UserCommentMark { CommentId = commentId, UserId = userId, Mark = (mark == 1) });
            else if (mark == 0)
                comment.UserCommentMarks.Remove(userCommentMark);
            else
                userCommentMark.Mark = (mark == 1);

            await _repository.SaveAllChanges();
            return comment.Rating;
        }
    }
}
