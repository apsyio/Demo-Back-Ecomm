using Appstagram.Base.Models.Entities;

namespace Aps.Apps.CueTheCurves.Api.Models.Entities
{
    public class BrandSizes : EntityDef
    {
        public int BrandId { get; set; }
        public Brands Brand { get; set; }
        public int SizeId { get; set; }
        public Sizes Size { get; set; }
    }
}
