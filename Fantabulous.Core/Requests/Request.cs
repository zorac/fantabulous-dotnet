namespace Fantabulous.Core.Requests
{
    /// <summary>
    /// A request.
    /// </summary>
    /// <inheritDoc/>
    public abstract class Request : IRequest
    {
        public string Token { get; set; }
    }
}
