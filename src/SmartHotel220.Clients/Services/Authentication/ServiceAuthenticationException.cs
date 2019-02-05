using System;

namespace SmartHotel220.Clients.Core.Services.Authentication
{
    /// <inheritdoc />
    /// <summary>
    /// Исключение при аутентификации через нашу службу аутентификации
    /// </summary>
    public class ServiceAuthenticationException : Exception
    {
        public string Content { get; }

        public ServiceAuthenticationException()
        {
        }

        public ServiceAuthenticationException(string content)
        {
            Content = content;
        }
    }
}