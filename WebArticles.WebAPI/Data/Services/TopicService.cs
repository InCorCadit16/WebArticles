using AutoMapper;
using DataModel.Data.Entities;
using System.Threading.Tasks;
using WebArticles.WebAPI.Data.Dtos;
using WebArticles.WebAPI.Data.Repositories.Implementations;
using WebArticles.WebAPI.Infrastructure.Exceptions;

namespace WebArticles.WebAPI.Data.Services
{
    public class TopicService
    {
        private readonly TopicRepository _repository;
        private readonly IMapper _mapper;

        public TopicService(TopicRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TopicDto[]> GetAll()
        {
            return _mapper.Map<TopicDto[]>((await _repository.GetAll()).ToArray());
        }

        public async Task<TopicDto> GetTopicByName(string name)
        {
            return _mapper.Map<TopicDto>(await _repository.GetTopicByName(name));
        }

        public async Task<TopicDto> CreateTopic(string topicName)
        {
            if (topicName.Contains(","))
            {
                throw new FormInvalidException("", "Topic name cannot contain ','");
            }

            var topic = new Topic { TopicName = topicName };

            topic = await _repository.Insert(topic);
            return _mapper.Map<TopicDto>(topic);
        }

        public async Task DeleteTopic(long id)
        {
            await _repository.Delete(id);
        }

        public async Task<TopicDto> UpdateTopic(TopicDto topicDto)
        {
            if (topicDto.TopicName.Contains(","))
            {
                throw new FormInvalidException("", "Topic name cannot contain ','");
            }

            var topic = await _repository.GetById(topicDto.Id);
            topic = _mapper.Map(topicDto, topic);
            await _repository.Update(topic);

            return topicDto;
        }
    }
}
