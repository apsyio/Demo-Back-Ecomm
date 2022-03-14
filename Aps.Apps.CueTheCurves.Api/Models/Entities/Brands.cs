using Appstagram.Base.Attributes;
using Appstagram.Base.Models.Entities;
using HotChocolate;
using System;
using System.Collections.Generic;

namespace Aps.Apps.CueTheCurves.Api.Models.Entities
{
    public class Brands : EntityDef
    {
        public string Name { get; set; }
        public string Thumbnail { get; set; }
        public string SizeOffered { get; set; }
        public int LikesCount { get; set; }
        public Dictionary<string, List<string>> Photos { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<UserBrands> UserBrands { get; set; }
        public ICollection<BrandLikes> BrandLikes { get; set; }
        public ICollection<Posts> Posts { get; set; }
        public ICollection<StyleBrands> StyleBrands { get; set; }
    }
}
