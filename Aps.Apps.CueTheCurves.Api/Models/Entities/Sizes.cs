using Appstagram.Base.Models.Entities;
using System.Collections.Generic;

namespace Aps.Apps.CueTheCurves.Api.Models.Entities
{
    public class Sizes : EntityDef
    {
        public string Size { get; set; }

        public ICollection<BrandSizes> BrandSizes { get; set; }
        public ICollection<Posts> Posts { get; set; }
    }
}
