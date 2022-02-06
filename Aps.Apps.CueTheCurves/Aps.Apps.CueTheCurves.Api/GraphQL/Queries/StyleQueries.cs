using Appstagram.Base.Enums;
using Appstagram.Base.Generics.Responses;
using Aps.Apps.CueTheCurves.Api.Models.Dtos;
using Aps.Apps.CueTheCurves.Api.Models.Entities;
using Aps.Apps.CueTheCurves.Api.Services.Contracts;
using HotChocolate;
using HotChocolate.Types;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Aps.Apps.CueTheCurves.Api.GraphQL.Queries
{
    [ExtendObjectType("Query")]
    public class StyleQueries
    {
        [GraphQLName("styles_getStyles")]
        public ListResponseBase<Styles> GetStyles(ClaimsPrincipal claims, [Service] IStyleService styleService)
        {
            if (!claims.Identity.IsAuthenticated) return ListResponseBase<Styles>.Failure(ResponseStatus.AUTHENTICATION_FAILED);
            var user = JsonConvert.DeserializeObject<Users>(claims.FindFirstValue("user"));
            if (user == null) return ListResponseBase<Styles>.Failure(ResponseStatus.USER_NOT_FOUND);

            return styleService.GetStyles();
        }

        [GraphQLName("styles_getStyle")]
        public ResponseBase<StyleDto> GetStyle(ClaimsPrincipal claims, [Service] IStyleService styleService, int styleId)
        {
            if (!claims.Identity.IsAuthenticated) return ResponseBase<StyleDto>.Failure(ResponseStatus.AUTHENTICATION_FAILED);
            var user = JsonConvert.DeserializeObject<Users>(claims.FindFirstValue("user"));
            if (user == null) return ResponseBase<StyleDto>.Failure(ResponseStatus.USER_NOT_FOUND);

            return styleService.GetStyle(user, styleId);
        }

    }
}
