using Appstagram.Base.Models.Entities;

namespace Aps.Apps.CueTheCurves.Api.Models.Entities
{
    public class UserBrands : EntityDef
    {
        public int UserId { get; set; }
        public Users User { get; set; }
        public int BrandId { get; set; }
        public Brands Brand { get; set; }
    }
}
