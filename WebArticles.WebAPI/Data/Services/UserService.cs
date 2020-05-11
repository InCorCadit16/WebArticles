using AutoMapper;
using DataModel.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data.Repositories;
using WebArticles.WebAPI.Data.Dto;
using WebArticles.WebAPI.Data.Models;

namespace WebArticles.WebAPI.Data.Services
{
    public class UserService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserService(IRepository repository, IMapper mapper, UserManager<User> userManager)
        {
            this._repository = repository;
            this._mapper = mapper;
            this._userManager = userManager;
        }


        public async Task<User> GetUserByUserName(string userName)
        {
            return await _repository.GetAll<User>().FirstAsync(u => u.UserName == userName);
        }
        public async Task<User> GetUserById(long id)
        {
            var query = _repository.GetAll<User>();


            query = query.Include(u => u.Writer)
                            .ThenInclude(w => w.TopicsLink)
                            .ThenInclude(wt => wt.Topic)
                          .Include(u => u.Writer)
                            .ThenInclude(w => w.Articles);

            query = query.Include(u => u.Reviewer)
                            .ThenInclude(r => r.TopicsLink)
                            .ThenInclude(rt => rt.Topic)
                          .Include(u => u.Reviewer)
                            .ThenInclude(w => w.Comments);

            return await query.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<UserModel> GetUserModelById(long id)
        {
            return _mapper.Map<UserModel>(await GetUserById(id));
        }

        public void CreateWriterAndReviewer(User user)
        {
            var reviewer = new Reviewer() { UserId = user.Id, User = user };
            var writer = new Writer() { UserId = user.Id, User = user };

            user.Reviewer = reviewer;
            user.Writer = writer;

            _repository.Insert(reviewer);
            _repository.Insert(writer);
            _repository.SaveChanges();
            _repository.Update(user);
            _repository.SaveChanges();
        }

        public async Task<User> GetUserIdByArticleId(long articleId)
        {
            var query = _repository.GetAll<User>();

            query = query.Include(u => u.Writer)
                            .ThenInclude(w => w.Articles);

            return await query.FirstOrDefaultAsync(u => u.Writer.Articles.Any(a => a.Id == articleId));
        }

        public async Task<string> GetProfilePickLink(long id)
        {
            try
            {
                var query = _repository.GetAll<User>();

                return (await query.FirstOrDefaultAsync(u => u.Id == id)).ProfilePickLink;
            } catch(NullReferenceException e)
            {
                return null;
            }
        }

        public async Task<UpdateAnswer> UpdateUser(UserModel userModel)
        {
            try
            {
                var initialUser = await GetUserById(userModel.Id);
                var updatedUser = _mapper.Map(userModel, initialUser);
                var result = await _userManager.UpdateAsync(updatedUser);

                if (result.Succeeded)
                    return new UpdateAnswer { Succeeded = true };
                else
                    return new UpdateAnswer { Succeeded = false, Error = result.Errors.Select(e => e.Description).Aggregate((e, i) => i += e + "\n") };
            }
            catch (DbUpdateException e)
            {
                return new UpdateAnswer { Succeeded = false, Error = $"Email \'{userModel.Email}\' is already taken" };
            }
            catch (Exception e)
            {
                return new UpdateAnswer { Succeeded = false, Error = "An internal server error occured" };
            }
        }
    }
}
