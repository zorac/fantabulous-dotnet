using Fantabulous.Core.Entities;

namespace Fantabulous.Core.Models
{
    /// <summary>
    /// A user and their default psued.
    /// </summary>
    public class UserAndPseud
    {
        /// <summary>
        /// A user.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// The user's default pseud.
        /// </summary>
        public Pseud Pseud { get; set; }
    }
}
