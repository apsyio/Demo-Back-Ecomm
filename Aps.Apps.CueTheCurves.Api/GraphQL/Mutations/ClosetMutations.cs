using Appstagram.Base.Enums;
using Appstagram.Base.Generics.Responses;
using Aps.Apps.CueTheCurves.Api.Models.Entities;
using Aps.Apps.CueTheCurves.Api.Models.Inputs;
using Aps.Apps.CueTheCurves.Api.Services.Contracts;
using HotChocolate;
using HotChocolate.Types;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Aps.Apps.CueTheCurves.Api.GraphQL.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class ClosetMutations
    {
        [GraphQLName("closet_createCloset")]
        public ResponseBase<Closets> CreateCloset(ClaimsPrincipal claims, [Service] IClosetService closetService, ClosetInput input)
        {
            if (!claims.Identity.IsAuthenticated) return ResponseBase<Closets>.Failure(ResponseStatus.AUTHENTICATION_FAILED);
            var user = JsonConvert.DeserializeObject<Users>(claims.FindFirstValue("user"));
            if (user == null) return ResponseBase<Closets>.Failure(ResponseStatus.USER_NOT_FOUND);

            input.UserId = user.Id;
            return closetService.CreateCloset(input);
        }
    }
}
