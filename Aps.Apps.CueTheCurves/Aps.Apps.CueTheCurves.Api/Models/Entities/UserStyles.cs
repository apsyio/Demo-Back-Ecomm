using Appstagram.Base.Models.Entities;

namespace Aps.Apps.CueTheCurves.Api.Models.Entities
{
    public class UserStyles : EntityDef
    {
        public int UserId { get; set; }
        public Users User { get; set; }
        public int StyleId { get; set; }
        public Styles Style { get; set; }
    }
}
