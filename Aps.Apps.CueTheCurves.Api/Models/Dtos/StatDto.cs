using Appstagram.Base.Models.Dtos;

namespace Aps.Apps.CueTheCurves.Api.Models.Dtos
{
    public class StatDto : DtoDef
    {
        public int BrandsCount { get; set; }
        public int StylesCount { get; set; }
        public int UsersCount { get; set; }
    }
}
