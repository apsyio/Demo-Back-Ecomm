using Appstagram.Base.Models.Entities;
using System;
using System.Collections.Generic;

namespace Aps.Apps.CueTheCurves.Api.Models.Entities
{
    public class Closets : EntityDef
    {
        public int UserId { get; set; }
        public Users User { get; set; }
        public string OutfitName { get; set; }
        public string Photo { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<ClosetItems> ClosetItems { get; set; }
    }
}
