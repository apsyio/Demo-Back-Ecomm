using Appstagram.Base.Models.Entities;

namespace Aps.Apps.CueTheCurves.Api.Models.Entities
{
    public class StyleBrands : EntityDef
    {
        public int StyleId { get; set; }
        public Styles Style { get; set; }
        public int BrandId { get; set; }
        public Brands Brand { get; set; }
    }
}
