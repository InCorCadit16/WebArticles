﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebArticles.WebAPI.Data.Dto
{
    public class CommentUpdate
    {
        public long Id { get; set; }

        public string NewContent { get; set; }
    }
}
