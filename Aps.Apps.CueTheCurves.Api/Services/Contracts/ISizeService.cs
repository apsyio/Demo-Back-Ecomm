using Appstagram.Base.Generics.Responses;
using Appstagram.Base.Services.Contracts;
using Aps.Apps.CueTheCurves.Api.Models.Entities;
using Aps.Apps.CueTheCurves.Api.Models.Inputs;

namespace Aps.Apps.CueTheCurves.Api.Services.Contracts
{
    public interface ISizeService : IServiceBase<Sizes, SizeInput>
    {
        ResponseBase DeactiveSize(int sizeId);
        ResponseBase ActivateSize(int sizeId);
        ListResponseBase<Sizes> GetSizes();
    }
}
