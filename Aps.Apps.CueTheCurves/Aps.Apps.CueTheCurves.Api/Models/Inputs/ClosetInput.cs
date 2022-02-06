using Appstagram.Base.Models.Inputs;
using HotChocolate;
using System.Collections.Generic;

namespace Aps.Apps.CueTheCurves.Api.Models.Inputs
{
    public class ClosetInput : InputDef
    {
        [GraphQLIgnore]
        public int UserId { get; set; }
        public string OutfitName { get; set; }
        public string Photo { get; set; }

        public List<ClosetItemInput> ClosetItems { get; set; }
    }
}
