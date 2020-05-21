using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebArticles.WebAPI.Infrastructure.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public string PublicMessage { get; }
        public int ReturnStatusCode { get; }
        public EntityNotFoundException(string message, string publicMessage, int returnStatusCode = StatusCodes.Status404NotFound) : base(message)
        {
            PublicMessage = publicMessage;
            ReturnStatusCode = returnStatusCode;
        }
    }
}
