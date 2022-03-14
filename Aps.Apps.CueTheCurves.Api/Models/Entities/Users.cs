using Appstagram.Base.Models.Entities;
using Aps.Apps.CueTheCurves.Api.Models.Enums;
using HotChocolate;
using System.Collections.Generic;

namespace Aps.Apps.CueTheCurves.Api.Models.Entities
{
    public class Users : UserDef
    {
        public string FullName { get; set; }
        public AccountTypes AccountType { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string AvatarUrl { get; set; }
        public string Bio { get; set; }
        [GraphQLIgnore]
        public bool IsSelected { get; set; }
        [GraphQLIgnore]
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<PostLikes> PostLikes { get; set; }
        public ICollection<UserSocials> Socials { get; set; }
        public ICollection<Closets> Closets { get; set; }
        public ICollection<UserBrands> UserBrands { get; set; }
        public ICollection<BrandLikes> BrandLikes { get; set; }
        public ICollection<UserStyles> UserStyles { get; set; }
        public ICollection<StyleLikes> StyleLikes { get; set; }
    }
}
