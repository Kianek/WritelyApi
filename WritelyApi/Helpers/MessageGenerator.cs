using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace WritelyApi.Helpers
{
    public static class MessageGenerator
    {
        public static object Generate(string message) => new { Message = message };

        public static object GenerateErrors(IdentityResult result)
        {
            return new { Errors = result.Errors.Select(err => err.Description) };
        }
    }
}