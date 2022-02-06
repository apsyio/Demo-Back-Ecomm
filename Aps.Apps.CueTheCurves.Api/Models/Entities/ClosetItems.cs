using Appstagram.Base.Models.Entities;

namespace Aps.Apps.CueTheCurves.Api.Models.Entities
{
    public class ClosetItems : EntityDef
    {
        public int ClosetId { get; set; }
        public Closets Closet { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public long XCoordinate { get; set; }
        public long YCoordinate { get; set; }
    }
}
