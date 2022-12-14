using Appstagram.Social.Models.Entities;
using Aps.Apps.CueTheCurves.Api.Models.Enums;
using System.Collections.Generic;

namespace Aps.Apps.CueTheCurves.Api.Models.Entities
{
    public class Posts : PostDefLike<Users, PostLikes>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Photo { get; set; }
        public int? BrandId { get; set; }
        public Brands Brand { get; set; }
        public int? StyleId { get; set; }
        public Styles Style { get; set; }
        public PostTypes PostType { get; set; }
        public int? SizeId { get; set; }
        public Sizes Size { get; set; }

        public ICollection<PostLikes> PostLikes { get; set; }
    }
}
