using Appstagram.Base.Generics.Responses;
using Appstagram.Base.Services.Contracts;
using Aps.Apps.CueTheCurves.Api.Models.Entities;
using Aps.Apps.CueTheCurves.Api.Models.Inputs;

namespace Aps.Apps.CueTheCurves.Api.Services.Contracts
{
    public interface IClosetService : IServiceBase<Closets,ClosetInput>
    {
        ResponseBase<Closets> CreateCloset(ClosetInput input);
    }
}
