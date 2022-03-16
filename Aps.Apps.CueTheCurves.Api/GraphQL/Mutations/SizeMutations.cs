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
    public class SizeMutations
    {
        [GraphQLName("size_createSize")]
        public ResponseBase<Sizes> CreateSize(ClaimsPrincipal claims, [Service] ISizeService sizeService, SizeInput sizeInput)
        {
            if (!claims.Identity.IsAuthenticated) return ResponseBase<Sizes>.Failure(ResponseStatus.AUTHENTICATION_FAILED);
            var user = JsonConvert.DeserializeObject<Users>(claims.FindFirstValue("user"));
            if (user == null) return ResponseBase<Sizes>.Failure(ResponseStatus.USER_NOT_FOUND);

            if (string.IsNullOrEmpty(sizeInput.Size))
            {
                return ResponseBase<Sizes>.Failure(ResponseStatus.NOT_ENOUGH_DATA);
            }

            if (!user.IsAdmin)
            {
                return ResponseBase<Sizes>.Failure(ResponseStatus.NOT_ALLOWED);
            }

            return sizeService.Add(sizeInput.Adapt<Sizes>());
        }

        [GraphQLName("size_activateSize")]
        public ResponseBase ActivateSize(ClaimsPrincipal claims, [Service] ISizeService sizeService, int sizeId)
        {
            if (!claims.Identity.IsAuthenticated) return ResponseBase.Failure(ResponseStatus.AUTHENTICATION_FAILED);
            var user = JsonConvert.DeserializeObject<Users>(claims.FindFirstValue("user"));
            if (user == null) return ResponseBase.Failure(ResponseStatus.USER_NOT_FOUND);

            if (!user.IsAdmin)
            {
                return ResponseBase.Failure(ResponseStatus.NOT_ALLOWED);
            }

            return sizeService.ActivateSize(sizeId);
        }

        [GraphQLName("size_deactivateSize")]
        public ResponseBase DeactivateSize(ClaimsPrincipal claims, [Service] ISizeService sizeService, int sizeId)
        {
            if (!claims.Identity.IsAuthenticated) return ResponseBase.Failure(ResponseStatus.AUTHENTICATION_FAILED);
            var user = JsonConvert.DeserializeObject<Users>(claims.FindFirstValue("user"));
            if (user == null) return ResponseBase.Failure(ResponseStatus.USER_NOT_FOUND);

            if (!user.IsAdmin)
            {
                return ResponseBase.Failure(ResponseStatus.NOT_ALLOWED);
            }

            return sizeService.DeactiveSize(sizeId);
        }
    }
}
