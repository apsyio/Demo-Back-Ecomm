using System.Collections.Generic;

namespace Aps.Apps.CueTheCurves.Api.Models.Inputs
{
    public class StyleInput
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Thumbnail { get; set; }
        public Dictionary<string, List<string>> Photos { get; set; }
    }
}
