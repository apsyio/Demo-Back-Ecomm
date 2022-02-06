using Appstagram.Base.Enums;
using Appstagram.Base.Generics.Responses;
using Aps.Apps.CueTheCurves.Api.Models.Entities;
using Aps.Apps.CueTheCurves.Api.Models.Inputs;
using Aps.Apps.CueTheCurves.Api.Services.Contracts;
using HotChocolate;
using HotChocolate.Types;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Claims;

namespace Aps.Apps.CueTheCurves.Api.GraphQL.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class UserMutations
    {
        [GraphQLName("user_signUp")]
        public ResponseBase<Users> Signup(ClaimsPrincipal claims, [Service] IUserService userService)
        {
            if (!claims.Identity.IsAuthenticated) return ResponseBase<Users>.Failure(ResponseStatus.AUTHENTICATION_FAILED);
            if(JsonConvert.DeserializeObject<Users>(claims.FindFirstValue("user")) != null)
            {
                return ResponseBase<Users>.Failure(ResponseStatus.ALREADY_EXIST);
            }

            string email = JsonConvert.DeserializeObject<dynamic>(claims.FindFirstValue("firebase")).identities.email[0];
            return userService.Add(new Users { Email = email, ExternalId = claims.FindFirstValue("user_id") });
        }

        [GraphQLName("user_setStyles")]
        public ResponseBase SetStyles(ClaimsPrincipal claims, [Service] IUserService userService, List<int> styleIds)
        {
            if (!claims.Identity.IsAuthenticated) return ResponseBase.Failure(ResponseStatus.AUTHENTICATION_FAILED);
            var user = JsonConvert.DeserializeObject<Users>(claims.FindFirstValue("user"));
            if (user == null) return ResponseBase.Failure(ResponseStatus.USER_NOT_FOUND);

            return userService.SetStyles(styleIds, user);
        }

        [GraphQLName("user_setBrands")]
        public ResponseBase SetBrands(ClaimsPrincipal claims, [Service] IUserService userService, List<int> brandIds)
        {
            if (!claims.Identity.IsAuthenticated) return ResponseBase.Failure(ResponseStatus.AUTHENTICATION_FAILED);
            var user = JsonConvert.DeserializeObject<Users>(claims.FindFirstValue("user"));
            if (user == null) return ResponseBase.Failure(ResponseStatus.USER_NOT_FOUND);

            return userService.SetBrands(brandIds, user);
        }

        [GraphQLName("user_updateUser")]
        public ResponseBase<Users> SetBrands(ClaimsPrincipal claims, [Service] IUserService userService, UserInput input)
        {
            if (!claims.Identity.IsAuthenticated) return ResponseBase<Users>.Failure(ResponseStatus.AUTHENTICATION_FAILED);
            var user = JsonConvert.DeserializeObject<Users>(claims.FindFirstValue("user"));
            if (user == null) return ResponseBase<Users>.Failure(ResponseStatus.USER_NOT_FOUND);
            input.Id = user.Id;
            return userService.Update(input);
        }
    }
}
