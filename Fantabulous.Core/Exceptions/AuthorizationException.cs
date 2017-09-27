using System;

namespace Fantabulous.Core.Exceptions
{
    public class AuthorizationException : Exception
    {
        public AuthorizationException(string message) : base(message)
        {
        }
    }
}
