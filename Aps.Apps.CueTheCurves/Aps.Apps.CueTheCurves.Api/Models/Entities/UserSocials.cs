using Appstagram.Base.Models.Entities;
using Aps.Apps.CueTheCurves.Api.Models.Enums;

namespace Aps.Apps.CueTheCurves.Api.Models.Entities
{
    public class UserSocials : EntityDef
    {
        public SocialNetworks SocialNetworks { get; set; }
        public string Address { get; set; }
        public int UserId { get; set; }
        public Users User { get; set; }
    }
}
