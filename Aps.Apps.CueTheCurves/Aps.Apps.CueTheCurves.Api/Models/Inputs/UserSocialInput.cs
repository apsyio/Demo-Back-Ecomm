using Appstagram.Base.Models.Inputs;
using Aps.Apps.CueTheCurves.Api.Models.Enums;

namespace Aps.Apps.CueTheCurves.Api.Models.Inputs
{
    public class UserSocialInput : InputDef
    {
        public SocialNetworks SocialNetworks { get; set; }
        public string Address { get; set; }
    }
}
