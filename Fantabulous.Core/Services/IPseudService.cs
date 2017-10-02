using System.Collections.Generic;
using System.Threading.Tasks;

using Fantabulous.Core.Entities;

namespace Fantabulous.Core.Services
{
    /// <summary>
    /// A service which provides pseudonym-related actions.
    /// </summary>
    public interface IPseudService
    {
        /// <summary>
        /// Fetch a pseud by their unique ID.
        /// </summary>
        /// <param name="id">
        /// A pseud ID
        /// </param>
        /// <returns>
        /// The pseud, or null if not found
        /// </returns>
        Task<Pseud> GetPseudAsync(long id);

        /// <summary>
        /// Fetch a pseud by their user ID and name.
        /// </summary>
        /// <param name="name">
        /// A user ID
        /// </param>
        /// <param name="name">
        /// A pseudonym
        /// </param>
        /// <returns>
        /// The pseud, or null if not found
        /// </returns>
        Task<Pseud> GetPseudAsync(long userId, string name);

        /// <summary>
        /// Fetch some pseuds by their IDs.
        /// </summary>
        /// <param name="ids">
        /// Some pseud IDs
        /// </param>
        /// <returns>
        /// The pseuds which were found, empty if none
        /// </returns>
        Task<IEnumerable<Pseud>> GetPseudsAsync(IEnumerable<long> ids);

        /// <summary>
        /// Fetch the JSON representation of a pseud by their unique ID.
        /// </summary>
        /// <param name="id">
        /// A pseud ID
        /// </param>
        /// <returns>
        /// JSON of the pseud, or null if not found
        /// </returns>
        Task<string> GetPseudJsonAsync(long id);

        /// <summary>
        /// Fetch the JSON representation of a pseud by their user ID and name.
        /// </summary>
        /// <param name="user ID">
        /// A user ID
        /// </param>
        /// <param name="name">
        /// A pseudonym
        /// </param>
        /// <returns>
        /// JSON of the pseud, or null if not found
        /// </returns>
        Task<string> GetPseudJsonAsync(long userId, string name);

        /// <summary>
        /// Fetch JSON representations of some pseuds by their unique IDs.
        /// </summary>
        /// <param name="ids">
        /// Some pseud IDs
        /// </param>
        /// <returns>
        /// JSONS of the pseuds which were found, empty if none
        /// </returns>
        Task<IEnumerable<string>> GetPseudsJsonAsync(IEnumerable<long> ids);
    }
}
