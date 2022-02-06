using Appstagram.Base.Attributes;
using Appstagram.Social.Models.Dtos;
using Aps.Apps.CueTheCurves.Api.Models.Entities;
using Aps.Apps.CueTheCurves.Api.Models.Enums;
using HotChocolate;

namespace Aps.Apps.CueTheCurves.Api.Models.Dtos
{
    public class PostDto : PostDtoLikeDef<PostLikes>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Photo { get; set; }
        public Brands Brand { get; set; }
        public Styles Style { get; set; }
        public PostTypes PostType { get; set; }
        public double SizeTag { get; set; }
        public int PosterId { get; set; }
        public Users Poster { get; set; }
    }
}
