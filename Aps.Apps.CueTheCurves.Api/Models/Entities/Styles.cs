using Appstagram.Base.Models.Entities;
using System;
using System.Collections.Generic;

namespace Aps.Apps.CueTheCurves.Api.Models.Entities
{
    public class Styles : EntityDef
    {
        public string Name { get; set; }
        public string Thumbnail { get; set; }
        public Dictionary<string, List<string>> Photos { get; set; }
        public int LikesCount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<StyleBrands> StyleBrands { get; set; }
        public ICollection<Posts> Posts { get; set; }
        public ICollection<UserStyles> UserStyles { get; set; }
        public ICollection<StyleLikes> StyleLikes { get; set; }
    }
}
