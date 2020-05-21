using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebArticles.WebAPI.Infrastructure.Models
{
    public class PaginatorAnswer<T>
    {
        public int Total { get; set; }

        public T[] Items { get; set; }
    }
}
