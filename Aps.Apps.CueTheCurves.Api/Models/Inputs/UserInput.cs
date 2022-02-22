using Appstagram.Base.Models.Inputs;
using Aps.Apps.CueTheCurves.Api.Models.Enums;
using System.Collections.Generic;

namespace Aps.Apps.CueTheCurves.Api.Models.Inputs
{
    public class UserInput : InputDef
    {
        public int? Id { get; set; }
        public string FullName { get; set; }
        public AccountTypes? AccountType { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Bio { get; set; }

        public List<UserSocialInput> Socials { get; set; }
    }
}
