using Appstagram.Base.Models.Dtos;
using Aps.Apps.CueTheCurves.Api.Models.Entities;
using Aps.Apps.CueTheCurves.Api.Models.Enums;
using System.Collections.Generic;

namespace Aps.Apps.CueTheCurves.Api.Models.Dtos
{
    public class UserDto : DtoDef
    {
        public string FullName { get; set; }
        public AccountTypes AccountType { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Bio { get; set; }
        public string AvatarUrl { get; set; }

        public List<UserSocials> Socials { get; set; }
        public List<Brands> Brands { get; set; }
        public List<Closets> Closets { get; set; }
    }
}
