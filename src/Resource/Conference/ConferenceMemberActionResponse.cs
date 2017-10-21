using System.Collections.Generic;

namespace Plivo.Resource.Conference
{
    public class ConferenceMemberActionResponse : DeleteResponse<Conference>
    {
        /// <summary>
        /// Gets or sets the member identifier.
        /// </summary>
        /// <value>The member identifier.</value>
        public List<string> MemberId { get; set; }
    }
}
