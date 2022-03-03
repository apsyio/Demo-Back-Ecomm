using Appstagram.Base.Models.Entities;

namespace Aps.Apps.CueTheCurves.Api.Models.Entities
{
    public class ClosetItems : EntityDef
    {
        public int ClosetId { get; set; }
        public Closets Closet { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string XCoordinate { get; set; }
        public string YCoordinate { get; set; }
    }
}
