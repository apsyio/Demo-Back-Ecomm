using Appstagram.Base.Models.Dtos;
using Aps.Apps.CueTheCurves.Api.Models.Entities;
using System;
using System.Collections.Generic;

namespace Aps.Apps.CueTheCurves.Api.Models.Dtos
{
    public class StyleDto : DtoDef
    {
        public string Name { get; set; }
        public string Thumbnail { get; set; }
        public Dictionary<string, List<string>> Photos { get; set; }
        public int LikesCount { get; set; }
        public bool Liked { get; set; }
        public List<Users> Inspos { get; set; } = new List<Users>();
        public List<Brands> Brands { get; set; } = new List<Brands>();
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
