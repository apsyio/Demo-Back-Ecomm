using Appstagram.Base.Generics.Responses;
using Appstagram.Base.Models.Inputs;
using Appstagram.Base.Services.Contracts;
using Aps.Apps.CueTheCurves.Api.Models.Dtos;
using Aps.Apps.CueTheCurves.Api.Models.Entities;

namespace Aps.Apps.CueTheCurves.Api.Services.Contracts
{
    public interface IStyleService : IServiceBase<Styles, InputDef>
    {
        ListResponseBase<Styles> GetStyles(Users user = null);
        ResponseBase<StyleDto> GetStyle(Users user, int styleId);
        ResponseBase LikeStyle(Users user, int styleId, bool liked);
        ResponseBase ActiveStyle(int styleId);
        ResponseBase DeActiveStyle(int styleId);
        ResponseBase<Styles> UpdateStyle(Styles styles);
    }
}
