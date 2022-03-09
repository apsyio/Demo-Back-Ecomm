using Appstagram.Social.Models.Inputs;
using Aps.Apps.CueTheCurves.Api.Models.Enums;

namespace Aps.Apps.CueTheCurves.Api.Models.Inputs
{
    public class PostInput : PostInputDef
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Photo { get; set; }
        public int? BrandId { get; set; }
        public int? StyleId { get; set; }
        public PostTypes PostType { get; set; }
        public string SizeOffered { get; set; }
    }
}
