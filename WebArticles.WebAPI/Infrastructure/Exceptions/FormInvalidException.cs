using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebArticles.WebAPI.Infrastructure.Exceptions
{
    public class FormInvalidException : Exception
    {
        public string PublicMessage { get; }

        public int ReturnStatusCode { get; }
        public FormInvalidException(string message, string publicMessage, int returnStatusCode = StatusCodes.Status400BadRequest) : base(message)
        {
            PublicMessage = publicMessage;
            ReturnStatusCode = returnStatusCode;
        }
    }
}
