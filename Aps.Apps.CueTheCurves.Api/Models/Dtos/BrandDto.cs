using Appstagram.Base.Models.Dtos;
using Aps.Apps.CueTheCurves.Api.Models.Entities;
using System;
using System.Collections.Generic;

namespace Aps.Apps.CueTheCurves.Api.Models.Dtos
{
    public class BrandDto : DtoDef
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Thumbnail { get; set; }
        public string SizeOffered { get; set; }
        public int LikesCount { get; set; }
        public Dictionary<string, List<string>> Photos { get; set; }
        public List<Styles> Styles { get; set; }
        public List<Users> Inspos { get; set; }
        public bool Liked { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted {get; set;}

    }
}
