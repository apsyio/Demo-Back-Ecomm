using Appstagram.Base.Enums;
using Appstagram.Base.Generics.Responses;
using Aps.Apps.CueTheCurves.Api.Models.Entities;
using Aps.Apps.CueTheCurves.Api.Models.Inputs;
using Aps.Apps.CueTheCurves.Api.Services.Contracts;
using HotChocolate;
using HotChocolate.Types;
using Mapster;
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

        [GraphQLName("style_activeStyle")]
        public ResponseBase ActiveStyle(ClaimsPrincipal claims, [Service] IStyleService styleService, int styleId)
        {
            if (!claims.Identity.IsAuthenticated) return ResponseBase.Failure(ResponseStatus.AUTHENTICATION_FAILED);
            var user = JsonConvert.DeserializeObject<Users>(claims.FindFirstValue("user"));
            if (user == null) return ResponseBase.Failure(ResponseStatus.USER_NOT_FOUND);
            if (!user.IsAdmin) return ResponseBase.Failure(ResponseStatus.NOT_ALLOWED);
            return styleService.ActiveStyle(styleId);
        }

        [GraphQLName("style_deActiveStyle")]
        public ResponseBase DeActiveStyle(ClaimsPrincipal claims, [Service] IStyleService styleService, int styleId)
        {
            if (!claims.Identity.IsAuthenticated) return ResponseBase.Failure(ResponseStatus.AUTHENTICATION_FAILED);
            var user = JsonConvert.DeserializeObject<Users>(claims.FindFirstValue("user"));
            if (user == null) return ResponseBase.Failure(ResponseStatus.USER_NOT_FOUND);
            if (!user.IsAdmin) return ResponseBase.Failure(ResponseStatus.NOT_ALLOWED);
            return styleService.DeActiveStyle(styleId);
        }

        [GraphQLName("style_createStyle")]
        public ResponseBase<Styles> CreateStyle(ClaimsPrincipal claims, [Service] IStyleService styleService, StyleInput input)
        {
            if (!claims.Identity.IsAuthenticated) return ResponseBase<Styles>.Failure(ResponseStatus.AUTHENTICATION_FAILED);
            var user = JsonConvert.DeserializeObject<Users>(claims.FindFirstValue("user"));
            if (user == null) return ResponseBase<Styles>.Failure(ResponseStatus.USER_NOT_FOUND);
            if (!user.IsAdmin) return ResponseBase<Styles>.Failure(ResponseStatus.NOT_ALLOWED);
            return styleService.Add(input.Adapt<Styles>());
        }

        [GraphQLName("style_updateStyle")]
        public ResponseBase<Styles> UpdateStyle(ClaimsPrincipal claims, [Service] IStyleService styleService, StyleInput input)
        {
            if (!claims.Identity.IsAuthenticated) return ResponseBase<Styles>.Failure(ResponseStatus.AUTHENTICATION_FAILED);
            var user = JsonConvert.DeserializeObject<Users>(claims.FindFirstValue("user"));
            if (user == null) return ResponseBase<Styles>.Failure(ResponseStatus.USER_NOT_FOUND);
            if (!user.IsAdmin) return ResponseBase<Styles>.Failure(ResponseStatus.NOT_ALLOWED);
            if (input.Id is null) return ResponseBase<Styles>.Failure(ResponseStatus.NOT_ENOUGH_DATA);

            return styleService.UpdateStyle(input.Adapt<Styles>());
        }
    }
}
