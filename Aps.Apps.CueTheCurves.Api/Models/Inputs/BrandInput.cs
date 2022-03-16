using Appstagram.Base.Models.Inputs;
using System.Collections.Generic;

namespace Aps.Apps.CueTheCurves.Api.Models.Inputs
{
    public class BrandInput : InputDef
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Thumbnail { get; set; }
        public string SizeOffered { get; set; }
        public Dictionary<string, List<string>> Photos { get; set; } = new();
        public List<int> Styles { get; set; } = new();
        public string BrandUrl { get; set; }

        public List<int> Sizes { get; set; } = new();
    }
}
