using System.Threading.Tasks;

using Fantabulous.Core.Entities;
using Fantabulous.Core.Exceptions;
using Fantabulous.Core.Models;
using Fantabulous.Core.Requests;

namespace Fantabulous.Core.Services
{
    /// <summary>
    /// A service which provides authentication-related actions.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Log in a user.
        /// </summary>
        /// <param name="request">
        /// A login request
        /// </param>
        /// <returns>
        /// The login response
        /// </returns>
        /// <exception cref="AuthenticationException">
        /// Thrown if the username or password was incorrect
        /// </exception>
        Task<UserAndPseud> LoginAsync(LoginRequest request);

        /// <summary>
        /// Creare a new user.
        /// </summary>
        /// <param name="request">
        /// A user creation request
        /// </param>
        /// <returns>
        /// A login response
        /// </returns>
        /// <exception cref="AuthenticationException">
        /// Thrown if the user could not be created
        /// </exception>
        Task<UserAndPseud> CreateUserAsync(CreateUserRequest request);

        /// <summary>
        /// Change a user's password
        /// </summary>
        /// <param name="request">
        /// A password change request
        /// </param>
        /// <exception cref="AuthenticationException">
        /// Thrown if the password change failed
        /// </exception>
        Task ChangePasswordAsync(ChangePasswordRequest request);
    }
}
