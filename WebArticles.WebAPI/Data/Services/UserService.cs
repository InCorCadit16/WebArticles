using AutoMapper;
using DataModel.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data.Repositories.Interfaces;
using WebAPI.Infrastructure;
using WebArticles.WebAPI.Data.Dtos;
using WebArticles.WebAPI.Data.Repositories.Implementations;
using WebArticles.WebAPI.Infrastructure.Exceptions;
using WebArticles.WebAPI.Infrastructure.Models;

namespace WebArticles.WebAPI.Data.Services
{
    public class UserService
    {
        private readonly UserRepository _repository;
        private readonly ArticleRepository _articleRepository;
        private readonly IRepository<Reviewer> _reviewerRepository;
        private readonly IRepository<Writer> _writerRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserService(UserRepository repository,
                            ArticleRepository articleRepository,
                            IRepository<Reviewer> reviewerRepository,
                            IRepository<Writer> writerRepository,
                            IMapper mapper,
                            UserManager<User> userManager)
        {
            this._repository = repository;
            this._writerRepository = writerRepository;
            this._reviewerRepository = reviewerRepository;
            this._articleRepository = articleRepository;
            this._mapper = mapper;
            this._userManager = userManager;
        }


        public async Task<User> GetUserByUserName(string userName)
        {
            return await _repository.GetUserByUserName(userName);
        }
        public async Task<User> GetUserById(long id)
        {
            return await _repository.GetUserWithAllProperties(id);
        }

        public async Task<UserDto> GetUserModelById(long id)
        {
            return _mapper.Map<UserDto>(await GetUserById(id));
        }

        public async Task CreateWriterAndReviewer(User user)
        {
            var reviewer = new Reviewer() { UserId = user.Id, User = user };
            var writer = new Writer() { UserId = user.Id, User = user };

            user.Reviewer = reviewer;
            user.Writer = writer;

            await _reviewerRepository.Insert(reviewer);
            await _writerRepository.Insert(writer);

            await _repository.Update(user);
        }

        public async Task<User> GetUserArticlesId(long articleId)
        {
            var article = await _articleRepository.GetById(articleId, a => a.Writer, article => article.Writer.User);

            return article.Writer.User;
        }

        public async Task<string> GetProfilePickLink(long id)
        {
                var user = await _repository.GetById(id);

                return user.ProfilePickLink;
        }

        public async Task UpdateUser(UserUpdateDto userUpdateDto)
        {
            var user = await GetUserById(userUpdateDto.Id);

            if (userUpdateDto.Email != user.Email)
            {
                if (_repository.GetUserByEmail(userUpdateDto.Email) != null)
                {
                    throw new FormInvalidException("", $"Email {userUpdateDto.Email} is already taken");
                }
            }
            
            user = _mapper.Map(userUpdateDto, user);

            await _repository.Update(user);
        }

        public async Task DeleteUser(long id)
        {
            var user = await _repository.GetById(id, u => u.Reviewer, u => u.Writer);

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                await _reviewerRepository.Delete(user.Reviewer.Id);
                await _writerRepository.Delete(user.Writer.Id);
            }
        }

        public async Task<PaginatorAnswer<UserRowDto>> GetPage(PaginatorQuery paginatorQuery)
        {
            return await _repository.GetPage<UserRowDto>(paginatorQuery, u => u.Reviewer, u => u.Reviewer.Comments, u => u.Writer, u => u.Writer.Articles);
        }
    }
}
