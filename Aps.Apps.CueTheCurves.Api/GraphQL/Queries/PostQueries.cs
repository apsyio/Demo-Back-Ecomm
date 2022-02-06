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
    public class PostQueries
    {
        [GraphQLName("post_getUserPosts")]
        public ListResponseBase<PostDto> GetUserPosts(ClaimsPrincipal claims, [Service] IPostService postService)
        {
            if (!claims.Identity.IsAuthenticated) return ListResponseBase<PostDto>.Failure(ResponseStatus.AUTHENTICATION_FAILED);
            var user = JsonConvert.DeserializeObject<Users>(claims.FindFirstValue("user"));
            if (user == null) return ListResponseBase<PostDto>.Failure(ResponseStatus.USER_NOT_FOUND);

            return postService.GetUserPosts<PostDto>(user);
        }

        [GraphQLName("post_getStylePosts")]
        public ListResponseBase<PostDto> GetStylePosts(ClaimsPrincipal claims, [Service] IPostService postService, int styleId)
        {
            if (!claims.Identity.IsAuthenticated) return ListResponseBase<PostDto>.Failure(ResponseStatus.AUTHENTICATION_FAILED);
            var user = JsonConvert.DeserializeObject<Users>(claims.FindFirstValue("user"));
            if (user == null) return ListResponseBase<PostDto>.Failure(ResponseStatus.USER_NOT_FOUND);

            return postService.GetStylePosts(user, styleId);
        }

        [GraphQLName("post_getBrandPosts")]
        public ListResponseBase<PostDto> GetBrandPosts(ClaimsPrincipal claims, [Service] IPostService postService, int brandId)
        {
            if (!claims.Identity.IsAuthenticated) return ListResponseBase<PostDto>.Failure(ResponseStatus.AUTHENTICATION_FAILED);
            var user = JsonConvert.DeserializeObject<Users>(claims.FindFirstValue("user"));
            if (user == null) return ListResponseBase<PostDto>.Failure(ResponseStatus.USER_NOT_FOUND);

            return postService.GetBrandPosts(user, brandId);
        }
    }
}
