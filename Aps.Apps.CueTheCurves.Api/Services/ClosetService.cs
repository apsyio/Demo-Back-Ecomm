using Appstagram.Base.Generics.Responses;
using Appstagram.Base.Services;
using Aps.Apps.CueTheCurves.Api.Models.Entities;
using Aps.Apps.CueTheCurves.Api.Models.Inputs;
using Aps.Apps.CueTheCurves.Api.Repositories.Contracts;
using Aps.Apps.CueTheCurves.Api.Services.Contracts;

namespace Aps.Apps.CueTheCurves.Api.Services
{
    public class ClosetService : ServiceBase<Closets, ClosetInput>, IClosetService
    {
        public ClosetService(IClosetRepository closetRepository)
            :base(closetRepository)
        {

        }

        public ResponseBase<Closets> CreateCloset(ClosetInput input)
        {
            return Add(input);
        }
    }
}
