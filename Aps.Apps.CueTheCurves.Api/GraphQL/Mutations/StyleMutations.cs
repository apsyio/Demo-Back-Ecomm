using Appstagram.Base.Enums;
using Appstagram.Base.Generics.Responses;
using Aps.Apps.CueTheCurves.Api.Models.Entities;
using Aps.Apps.CueTheCurves.Api.Services.Contracts;
using HotChocolate;
using HotChocolate.Types;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Aps.Apps.CueTheCurves.Api.GraphQL.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class StyleMutations
    {
        [GraphQLName("style_likeStyle")]
        public ResponseBase LikeStyle(ClaimsPrincipal claims, [Service] IStyleService styleService, int styleId, bool liked = true)
        {
            if (!claims.Identity.IsAuthenticated) return ResponseBase.Failure(ResponseStatus.AUTHENTICATION_FAILED);
            var user = JsonConvert.DeserializeObject<Users>(claims.FindFirstValue("user"));
            if (user == null) return ResponseBase.Failure(ResponseStatus.USER_NOT_FOUND);

            return styleService.LikeStyle(user,styleId, liked);
        }
    }
}
