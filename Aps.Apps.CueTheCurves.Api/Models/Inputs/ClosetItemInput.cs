using Appstagram.Base.Models.Inputs;

namespace Aps.Apps.CueTheCurves.Api.Models.Inputs
{
    public class ClosetItemInput : InputDef
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public long XCoordinate { get; set; }
        public long YCoordinate { get; set; }
    }
}
