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
    public class PostMutations
    {
        [GraphQLName("post_createPost")]
        public ResponseBase<Posts> CreatePost(ClaimsPrincipal claims, [Service] IPostService postService, PostInput postInput)
        {
            if (!claims.Identity.IsAuthenticated) return ResponseBase<Posts>.Failure(ResponseStatus.AUTHENTICATION_FAILED);
            var user = JsonConvert.DeserializeObject<Users>(claims.FindFirstValue("user"));
            if (user == null) return ResponseBase<Posts>.Failure(ResponseStatus.USER_NOT_FOUND);

            if(postInput.StyleId == null && postInput.BrandId == null)
            {
                return ResponseBase<Posts>.Failure(ResponseStatus.NOT_ENOUGH_DATA);
            }

            postInput.PosterId = user.Id;
            return postService.Add(postInput);
        }

        [GraphQLName("post_likePost")]
        public ResponseBase LikePost(ClaimsPrincipal claims, [Service] IPostService postService, int postId, bool liked = true)
        {
            if (!claims.Identity.IsAuthenticated) return ResponseBase.Failure(ResponseStatus.AUTHENTICATION_FAILED);
            var user = JsonConvert.DeserializeObject<Users>(claims.FindFirstValue("user"));
            if (user == null) return ResponseBase.Failure(ResponseStatus.USER_NOT_FOUND);

            return postService.LikePost(user.Id, postId, liked);
        }
    }
}
