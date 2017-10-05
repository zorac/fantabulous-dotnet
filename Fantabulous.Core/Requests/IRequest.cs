namespace Fantabulous.Core.Requests
{
    /// <summary>
    /// A request.
    /// </summary>
    public interface IRequest
    {
        /// <summary>
        /// A request-authentication token.
        /// </summary>
        string Token { get; }
    }
}
