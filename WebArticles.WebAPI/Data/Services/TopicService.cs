using AutoMapper;
using DataModel.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data.Repositories;

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

    }
}
