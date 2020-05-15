using AutoMapper;
using DataModel.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data.Repositories;
using WebArticles.WebAPI.Data.Dto;

namespace WebArticles.WebAPI.Data.Services
{
    public class TopicService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public TopicService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Topic[]> GetAll()
        {
            return await _repository.GetAll<Topic>().OrderBy(t => t.Id).ToArrayAsync();
        }

        public Topic GetTopicByName(string name)
        {
            return _repository.GetAll<Topic>().FirstOrDefault(t => t.TopicName == name);
        }

        public CreateAnswer CreateTopic(string topicName)
        {
            try
            {
                var topic = new Topic { TopicName = topicName };

                _repository.Insert(topic);
                _repository.SaveChanges();
                return new CreateAnswer { Succeeded = true, Id = topic.Id };
            } catch (Exception e)
            {
                return new CreateAnswer { Succeeded = false, Error = $"Failed to create topic \"{topicName}\"" };
            }
        }

        public async Task<UpdateAnswer> DeleteTopic(long id)
        {
            try
            {
                var topic = await _repository.GetAll<Topic>().FirstOrDefaultAsync(t => t.Id == id);

                _repository.Delete(topic);
                _repository.SaveChanges();
                return new UpdateAnswer { Succeeded = true };
            } catch (Exception e)
            {
                return new UpdateAnswer { Succeeded = false, Error = "Failed to delete topic" };
            }
        }

        public UpdateAnswer UpdateTopic(Topic topic)
        {
            try
            {
                _repository.Update(topic);
                _repository.SaveChanges();
                return new UpdateAnswer { Succeeded = true };
            } catch (Exception e)
            {
                return new UpdateAnswer { Succeeded = false, Error = $"Failed to update topic to \"{topic.TopicName}\"" };
            }
        }

    }
}
