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
    [ExtendObjectType(typeof(Query))]
    public class UserQueries
    {
        [GraphQLName("user_login")]
        public ResponseBase<Users> Login(ClaimsPrincipal claims)
        {
            if (!claims.Identity.IsAuthenticated) return ResponseBase<Users>.Failure(ResponseStatus.AUTHENTICATION_FAILED);
            var user = JsonConvert.DeserializeObject<Users>(claims.FindFirstValue("user"));
            if (user == null) return ResponseBase<Users>.Failure(ResponseStatus.USER_NOT_FOUND);
            
            return ResponseBase<Users>.Success(user);
        }

        [GraphQLName("user_getStyles")]
        public ListResponseBase<Styles> GetStyles(ClaimsPrincipal claims, [Service] IStyleService styleService)
        {
            if (!claims.Identity.IsAuthenticated) return ListResponseBase<Styles>.Failure(ResponseStatus.AUTHENTICATION_FAILED);
            var user = JsonConvert.DeserializeObject<Users>(claims.FindFirstValue("user"));
            if (user == null) return ListResponseBase<Styles>.Failure(ResponseStatus.USER_NOT_FOUND);

            return styleService.GetStyles(user);
        }

        [GraphQLName("user_getSelectedInspos")]
        public ListResponseBase<Users> GetSelectedInspos(ClaimsPrincipal claims, [Service] IUserService userService)
        {
            if (!claims.Identity.IsAuthenticated) return ListResponseBase<Users>.Failure(ResponseStatus.AUTHENTICATION_FAILED);
            var user = JsonConvert.DeserializeObject<Users>(claims.FindFirstValue("user"));
            if (user == null) return ListResponseBase<Users>.Failure(ResponseStatus.USER_NOT_FOUND);

            return userService.GetSelectedInspos(user);
        }

        [GraphQLName("user_getBrands")]
        public ListResponseBase<Brands> GetUserBrands(ClaimsPrincipal claims, [Service] IBrandService brandService)
        {
            if (!claims.Identity.IsAuthenticated) return ListResponseBase<Brands>.Failure(ResponseStatus.AUTHENTICATION_FAILED);
            var user = JsonConvert.DeserializeObject<Users>(claims.FindFirstValue("user"));
            if (user == null) return ListResponseBase<Brands>.Failure(ResponseStatus.USER_NOT_FOUND);

            return brandService.GetBrands(user);
        }

        [GraphQLName("user_getInspos")]
        public ListResponseBase<Users> GetInspos(ClaimsPrincipal claims, [Service] IUserService userService, bool isCommon = false, bool isRandom = true)
        {
            if (!claims.Identity.IsAuthenticated) return ListResponseBase<Users>.Failure(ResponseStatus.AUTHENTICATION_FAILED);
            var user = JsonConvert.DeserializeObject<Users>(claims.FindFirstValue("user"));
            if (user == null) return ListResponseBase<Users>.Failure(ResponseStatus.USER_NOT_FOUND);

            return userService.GetInspos(user, isCommon, isRandom);
        }

        [GraphQLName("user_getInspo")]
        public ResponseBase<UserDto> GetInspo(ClaimsPrincipal claims, [Service] IUserService userService, int inspoId)
        {
            if (!claims.Identity.IsAuthenticated) return ResponseBase<UserDto>.Failure(ResponseStatus.AUTHENTICATION_FAILED);
            var user = JsonConvert.DeserializeObject<Users>(claims.FindFirstValue("user"));
            if (user == null) return ResponseBase<UserDto>.Failure(ResponseStatus.USER_NOT_FOUND);

            return userService.GetInspo(inspoId);
        }
    }
}
