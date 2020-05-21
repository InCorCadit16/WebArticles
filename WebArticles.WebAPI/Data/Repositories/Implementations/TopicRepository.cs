using AutoMapper;
using DataModel.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebAPI;

namespace WebArticles.WebAPI.Data.Repositories.Implementations
{
    public class TopicRepository : EntityRepository<Topic>
    {

        public TopicRepository(ArticleDbContext context, IMapper mapper): base (context, mapper)
        {

        }

        public async Task<Topic> GetTopicByName(string name)
        {
            return await _context.Topics.FirstOrDefaultAsync(t => t.TopicName == name);
        }
    }
}
